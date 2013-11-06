using AdBAR.App;
using AdBAR.Properties;
using DevComponents.DotNetBar;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace AdBAR
{
    public partial class FormBar : Form
    {
        // Reference: http://www.pinvoke.net/default.aspx/user32.getforegroundwindow
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        // Reference: http://www.pinvoke.net/default.aspx/user32/GetWindowPlacement.html
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowPlacement(IntPtr hWnd, ref WindowPlacement lpwndpl);

        // Reference: http://www.pinvoke.net/default.aspx/user32.getwindowthreadprocessid
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll")]
        static extern bool UnhookWinEvent(IntPtr hWinEventHook);

        [DllImport("user32.dll")]
        static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);
        delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

        private IntPtr _hook;
        private WinEventDelegate _wd;
        private UInt16 _currentIteration;
        private const UInt16 Delay = 100, Iterations = 10; // With every event, the application will iterate with a specified delay checking for changes
        private readonly Dictionary<int, IntPtr> _processesAndMaximizedWindows = new Dictionary<int, IntPtr>();
        private int _lastYVisible, _lastY=-1, _uncollapsedHeight = 200, _maxShortButtons = 4,_collapsedHeight = 24, _multiHorizontalPadding = 40, _multiVerticalPadding = 40;
        private readonly ApplicationStructure _structure;
        internal static Dictionary<string, string> _browsersPaths;
        internal static string _currentBrowser;
        private List<RibbonBar> _browsersPanels;
        private List<WatchedProcess> _watchedProcesses, _hideFromProcesses;
        private static BarStatus newStatus, lastStatus;
        private bool _disableNonAvailableButtons;

        public FormBar()
        {
            InitializeComponent();

            _structure = new ApplicationStructure("tab_names.xml");
            lastStatus = BarStatus.NotOnTop;
            newStatus = BarStatus.Hidden;

            CheckBrowsers();
            LoadSettings();
            CreateInterface();
        }

        #region "Windows Management"

        /// <summary>
        /// This optimizes the paint routines over this window, but generates some glitches with some controls
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                var os = Environment.OSVersion;
                if ((os.Platform == PlatformID.Win32NT) && (os.Version.Major >= 6))
                    cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED 
                return cp;
            }
        }

        public void CollapseWindow()
        {
            superTabControlMain.SelectedTab = null;
            ChangeWindowHeight(_collapsedHeight);
        }

        private void ChangeWindowHeight(int i)
        {
            Height = i;
            _lastYVisible--;
            AdjustPosition(true);
        }

        public void UncollapseWindow()
        {
            ChangeWindowHeight(_uncollapsedHeight);
        }

        public void AdjustPosition(bool forced = false)
        {
            var t = new TaskBarUtilities.Taskbar();

            if (t.VisibleBounds.Y == _lastYVisible)
                return;

            if (forced || t.VisibleBounds.Y == t.Bounds.Y || t.VisibleBounds.Y < t.Bounds.Y + t.Size.Height)
            {
                _lastYVisible = t.VisibleBounds.Y;
                _lastY = _lastYVisible > 1 ? Math.Max(_lastYVisible, t.Bounds.Y) : _lastYVisible;

                if (_lastY != 0)
                    Top = _lastY - Height;
                else
                    Top = t.VisibleBounds.Y == 0 ? t.Bounds.Bottom : t.VisibleBounds.Bottom - Height;

                Left = (Screen.PrimaryScreen.Bounds.Width / 2) - (Width / 2);
            }
        }

        void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            if (backgroundWorkerChecker.IsBusy)
                _currentIteration = 0;
            else
                backgroundWorkerChecker.RunWorkerAsync();
        }

        /// <summary>
        /// Checks if the current window referenced by a handle  is maximized
        /// </summary>
        /// <param name="p">Process you want to get information</param>
        /// <returns>True if the window is maximized</returns>
        private bool IsMaximized(Process p)
        {
            IntPtr handle;
            if (_processesAndMaximizedWindows.ContainsKey(p.Id))
            {
                handle = _processesAndMaximizedWindows[p.Id];
            }
            else
            {
                _processesAndMaximizedWindows.Add(p.Id, p.MainWindowHandle);
                handle = p.MainWindowHandle;
            }

            var placement = new WindowPlacement();
            placement.length = Marshal.SizeOf(placement);

            if (GetWindowPlacement(handle, ref placement))
            {
                if (placement.showCmd == 3) // WM_MAXIMIZED
                {
                    _processesAndMaximizedWindows[p.Id] = handle;
                    return true;
                }
            }
            else
                if (_processesAndMaximizedWindows[p.Id] != p.MainWindowHandle)
                {
                    _processesAndMaximizedWindows[p.Id] = p.MainWindowHandle;
                    return IsMaximized(p);
                }
                else
                    return false;

            // Error retrieving the Window placement
            return false;
        }

        private void backgroundWorkerChecker_DoWork(object sender, DoWorkEventArgs e)
        {
            timerChecker.Stop();
            _currentIteration = 0;
            newStatus = BarStatus.OnTop;

            // Check by processes first
            if ((from p in Process.GetProcesses() from s in _hideFromProcesses where p.ProcessName.ToLower().Equals(s.Name) select p).Any())
            {
                newStatus = BarStatus.Hidden;
                backgroundWorkerChecker.ReportProgress(-1);
                return;
            }

            do
            {
                uint processId;
                var foregroundWindow = GetForegroundWindow();

                GetWindowThreadProcessId(foregroundWindow, out processId);
                var p = Process.GetProcessById((int)processId);
                var processName = p.ProcessName.ToLower();

                if (processName == "idle") continue; // Ignore Idle process
                newStatus = BarStatus.OnTop;

                foreach (var s in _watchedProcesses)
                {
                    try
                    {
                        if (!processName.Equals(s.Name)) continue;

                        switch (s.Behaviour)
                        {
                            case WatchedProcessBehaviour.HideIfIsActive:
                                newStatus = BarStatus.Hidden;
                                break;

                            case WatchedProcessBehaviour.HideIfIsActiveAndMaximized:
                                if (IsMaximized(p))
                                    newStatus = BarStatus.Hidden;
                                break;

                            case WatchedProcessBehaviour.SetNotOnTopIfIsActive:
                                newStatus = BarStatus.NotOnTop;
                                break;
                        }
                    }
                    catch
                    {
                        // Error retrieving the process instance (privileges?)
                    }
                }

                if (newStatus != lastStatus)
                {
                    backgroundWorkerChecker.ReportProgress(-1);
                    return;
                }
                Thread.Sleep(Delay);
            } while (_currentIteration++ < Iterations);
        }

        private void backgroundWorkerChecker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if(e.ProgressPercentage==-1)
                UpdateApplicationStatus();
        }

        private void UpdateApplicationStatus()
        {
            switch (newStatus)
            {
                case BarStatus.OnTop:
                    Opacity = 100;
                    TopMost = true;
                    Select();
                    break;

                case BarStatus.Hidden:
                    Opacity = 0;
                    break;

                case BarStatus.NotOnTop:
                    Opacity = 100;
                    TopMost = false;
                    SendToBack();
                    break;
            }

            lastStatus = newStatus;
        }
        #endregion

        /// <summary>
        /// Check the browser availability reported by Windows, prepares the references
        /// </summary>
        private void CheckBrowsers()
        {
            // Check browser availability
            _browsersPanels = new List<RibbonBar>();
            _browsersPaths = new Dictionary<String, String>();
            _currentBrowser = Settings.Default.SelectedBrowser;

            var browsers = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet");
            foreach (var br in Utilities.GetButtonItems(ribbonBarBrowsers.Items))
            {
                if (browsers == null)
                {
                    // No browsers in registry, using just the default one
                    _currentBrowser = String.Empty;
                    break;
                }

                foreach (var key in browsers.GetSubKeyNames())
                {
                    var tag = (String)br.Tag;
                    if (!key.ToLower().Contains(tag)) continue;

                    var browserPath = browsers.OpenSubKey(key).OpenSubKey(@"shell\open\command").GetValue(null) as string;
                    if (browserPath == null) continue;

                    var path = browserPath.Replace("\"", "");
                    if (File.Exists(path))
                    {
                        if (!_browsersPaths.ContainsKey(tag))
                        {
                            _browsersPaths.Add(tag, path);
                            br.Enabled = true;

                            if (tag == _currentBrowser || String.IsNullOrEmpty(_currentBrowser))
                            {
                                _currentBrowser = tag;
                                br.Checked = true;
                            }
                        }
                    }
                }
            }

            // Select first available browser if none was selected
            if (!String.IsNullOrEmpty(_currentBrowser)) return;

            foreach (var tag in _browsersPaths)
            {
                SwitchSelectedBrowser(tag.Key);
                break;
            }
        }

        private void FormBar_Load(object sender, EventArgs e)
        {
            _watchedProcesses = Utilities.FilterWatchedProcessesList(_structure.WatchedProcesses, new List<WatchedProcessBehaviour>(new [] { 
                WatchedProcessBehaviour.HideIfIsActive, WatchedProcessBehaviour.HideIfIsActiveAndMaximized, WatchedProcessBehaviour.SetNotOnTopIfIsActive}));
            _hideFromProcesses = Utilities.FilterWatchedProcessesList(_structure.WatchedProcesses, new List<WatchedProcessBehaviour>(new[] { WatchedProcessBehaviour.HideIfProcessIsRunning}));

            _wd = WinEventProc;
            _hook = SetWinEventHook(3, 23, IntPtr.Zero, _wd, 0, 0, 0); // first args is EVENT_SYSTEM_FOREGROUND, last argument is WINEVENT_OUTOFCONTEXT

            CollapseWindow();
            backgroundWorkerChecker.RunWorkerAsync();
            //UiCallbackTimer.DelayExecution(TimeSpan.FromMilliseconds(500), () => { Opacity = 100; CollapseWindow(); Opacity = 100; }); // Hide the redraw
        }

        /// <summary>
        /// Creates all the ribbon interface from the XML and folders
        /// </summary>
        private void CreateInterface()
        {
            // Create interface
            superTabControlMain.Tabs.Clear();
            foreach (var t in _structure.Tabs)
                if (t.Visible)
                {
                    var tab = superTabControlMain.CreateTab(t.Name);
                    int left = 0,w = (superTabControlMain.Width/_maxShortButtons) + 1,
                        w2 = w*2,h = Height - superTabControlMain.TabStrip.Height - 2;
                    tab.Enabled = t.Enabled;

                    foreach (var g in t.Groups)
                    {
                        var r = new RibbonBar
                        {
                            Name = g.Name,
                            Width = w, Height = h,
                            Left = left, Text = g.Name,
                            HorizontalItemAlignment = eHorizontalItemsAlignment.Center,
                            VerticalItemAlignment = eVerticalItemsAlignment.Middle,
                            AutoOverflowEnabled = false
                        };

                        switch (g.Type)
                        {
                            case TabGroupType.ShortButton:
                                Utilities.CreateButtonOrGallery(g.Items, ref r, ref tab, _multiHorizontalPadding, _multiVerticalPadding, _disableNonAvailableButtons);
                                left += w;
                                break;

                            case TabGroupType.BrowserPanel:
                                Utilities.CreateRibbonBarFromTemplate(ribbonBarBrowsers.Items, ref r, ref tab);
                                _browsersPanels.Add(r); // Reference to each browser panel 
                                left += w;
                                break;

                            case TabGroupType.LongButton:
                                r.Width = w2;
                                Utilities.CreateButtonOrGallery(g.Items, ref r, ref tab, _multiHorizontalPadding, _multiVerticalPadding, _disableNonAvailableButtons);
                                left += w2;
                                break;

							case TabGroupType.CustomControls:
								var pluginName = g.GetCustomOption("content").ToLower();
								if(String.IsNullOrEmpty(pluginName)) break;
								var plugin = PluginsManager.Instance.Controls.FirstOrDefault(c => c.ControlName.ToLower().Equals(pluginName));
								if (plugin == null) break;

								foreach (var ribbonBar in plugin.RibbonBars)
								{
									ribbonBar.Name = g.Name;
									ribbonBar.Height = h;
									ribbonBar.Width = w2;
									ribbonBar.Left = left;
									ribbonBar.Text = g.Name;
									ribbonBar.HorizontalItemAlignment = eHorizontalItemsAlignment.Center;
									ribbonBar.VerticalItemAlignment = eVerticalItemsAlignment.Middle;
									ribbonBar.AutoOverflowEnabled = false;
									tab.AttachedControl.Controls.Add(ribbonBar);
									left += ribbonBar.Width;
								}
								break;
                        }
                    }
                }
        }

        private void LoadSettings()
        {
            try
            {
                // Application list to hide from
                _structure.WatchedProcesses = Utilities.ParseWatchedProcessesFile("hide_list.xml");

                // Program configuration
                var f = Utilities.GetTextFromFile("config.xml");
                _uncollapsedHeight = int.Parse(Utilities.GetValueRegex("<height>(.*)</height>", f));
                _collapsedHeight = int.Parse(Utilities.GetValueRegex("<collapsedheight>(.*)</collapsedheight>", f));
                Width = int.Parse(Utilities.GetValueRegex("<width>(.*)</width>", f));
                _multiHorizontalPadding = int.Parse(Utilities.GetValueRegex("<multihorizontalpadding>(.*)</multihorizontalpadding>", f));
                _multiVerticalPadding = int.Parse(Utilities.GetValueRegex("<multiverticalpadding>(.*)</multiverticalpadding>", f));
                _disableNonAvailableButtons = Utilities.GetValueRegex("<grayoutnonapproveduser>(.*)</grayoutnonapproveduser>", f) != "false";

                var g = CreateGraphics();
                try
                {
                    if (g.DpiX > 96)
                    {
                        Width = int.Parse(Utilities.GetValueRegex("<width120dpi>(.*)</width120dpi>", f));
                        _uncollapsedHeight = int.Parse(Utilities.GetValueRegex("<height120dpi>(.*)</height120dpi>", f));
                        _collapsedHeight = int.Parse(Utilities.GetValueRegex("<collapsedheight120dpi>(.*)</collapsedheight120dpi>", f));
                    }
                }
                finally
                {
                    g.Dispose();
                }

                Height = _uncollapsedHeight;

                // Theme
                superTabControlMain.TabStyle = (eSuperTabStyle)(Enum.GetValues(typeof (eSuperTabStyle)).GetValue(Math.Min(5,Math.Max(0,int.Parse(Utilities.GetValueRegex("<theme>(.*)</theme>", f))))));
                styleManagerMain.ManagerStyle = (eStyle)(Enum.GetValues(typeof (eStyle)).GetValue(Math.Min(10,Math.Max(0,int.Parse(Utilities.GetValueRegex("<subtheme>(.*)</subtheme>", f))))));
                timerUpdate.Interval = int.Parse(Utilities.GetValueRegex("<checktime>(.*)</checktime>", f));
                timerChecker.Interval = int.Parse(Utilities.GetValueRegex("<inactivitychecktime>(.*)</inactivitychecktime>", f));
                _maxShortButtons = int.Parse(Utilities.GetValueRegex("<maxshortbuttons>(.*)</maxshortbuttons>", f));

                // File updates
                try
                {
                    var source = Path.GetFullPath(Utilities.GetValueRegex("<syncsource>(.*)</syncsource>", f));
                    var destination = Path.GetFullPath(Utilities.GetValueRegex("<synctarget>(.*)</synctarget>", f));

                    foreach(var filter in Utilities.GetValueRegex("<syncfilter>(.*)</syncfilter>", f).Split(new [] {'|'}))
                        foreach (var file in Directory.GetFiles(source, filter))
                        {
                            var target = Path.Combine(destination, file.Substring(source.Length+1));

                            if (!File.Exists(target) || File.GetLastWriteTime(target).CompareTo(File.GetLastWriteTime(file)) < 0)
                                try
                                {
                                    File.Copy(file, target, true);
                                }
                                catch
                                {
                                }
                        }
                }
                catch { }
            }
            catch { }
        }

        private void superTabControlMain_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {
            UncollapseWindow();
        }

        private void FormBar_Deactivate(object sender, EventArgs e)
        {
            CollapseWindow();
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            // Not optimal but it works
            AdjustPosition();
        }

        private void superTabControlMain_TabStripMouseMove(object sender, MouseEventArgs e)
        {
            timerUpdate.Stop();
            timerUpdate.Start();
        }

        private void buttonItemBrowserSwitch_Click(object sender, EventArgs e)
        {
            // Select the current clicked one
            SwitchSelectedBrowser(((ButtonItem)sender).Tag as String);
        }

        /// <summary>
        /// Switches the selected browser, unchecks all other ones in other browsers panels
        /// </summary>
        /// <param name="browser"></param>
        private void SwitchSelectedBrowser(string browser)
        {
            _currentBrowser = browser;

            // Other panels
            foreach (var ribbon in _browsersPanels)
            {
                foreach (var b in Utilities.GetButtonItems(ribbon.Items))
                {
                    if (b.Enabled)
                    {
                        var enabled = b.Tag as String == _currentBrowser;
                        b.Checked = enabled;
                    }
                }
            }
        }

        private void FormBar_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.SelectedBrowser = _currentBrowser;
            Settings.Default.Save();
        }

        private void timerChecker_Tick(object sender, EventArgs e)
        {
            if(!backgroundWorkerChecker.IsBusy)
                backgroundWorkerChecker.RunWorkerAsync();
        }

        private void backgroundWorkerChecker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(_hideFromProcesses.Count > 0)
                timerChecker.Start();
        }

        private void superTabControlMain_ControlBox_CloseBox_Click(object sender, EventArgs e)
        {
            UnhookWinEvent(_hook);
            Application.Exit();
        }
    }

    public enum BarStatus
    {
        OnTop,Hidden,NotOnTop
    }
}
