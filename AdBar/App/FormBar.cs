using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
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
        private int _lastYVisible, _uncollapsedHeight = 200, _maxShortButtons = 4,_collapsedHeight = 24, _multiHorizontalPadding = 40, _multiVerticalPadding = 40;
        private readonly ApplicationStructure _structure;
        internal static Dictionary<string, string> BrowsersPaths;
        internal static string CurrentBrowser;
        private List<RibbonBar> _browsersPanels;
        private List<WatchedProcess> _watchedProcesses, _hideFromProcesses;
        private static BarStatus _newStatus, _lastStatus;
        private bool _disableNonAvailableButtons;
        private SyncronizationHelper _sync;
        private bool _skipCollapse;
        private readonly float _virtualDpi; // TODO: Clean this
        private int _lastMonitorCount;
        private int _preferedMonitor;
        private readonly Settings _settings
            ;

        public FormBar()
        {
            InitializeComponent();

            _settings = new Settings();
            _structure = new ApplicationStructure("tab_names.xml");
            _lastStatus = BarStatus.NotOnTop;
            _newStatus = BarStatus.Hidden;
            
            CheckBrowsers();

            _virtualDpi = 1/(96/CreateGraphics().DpiX);
            LoadSettings();
            CreateInterface();
            LoadSecondMonitorSettings();
        }

        private void LoadSecondMonitorSettings()
        {
            _preferedMonitor = _settings.Default.PreferedMonitor;
            CheckMultipleMonitors(true);
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
            if (!_skipCollapse)
            {
                superTabControlMain.SelectedTab = null;
                ChangeWindowHeight(_collapsedHeight);
            }
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

        private void colorPickerDropDownInterface_SelectedColorChanged(object sender, EventArgs e)
        {
            ChangeAccentColor(colorPickerDropDownInterface.SelectedColor);
        }

        private void colorPickerDropDownInterface_ColorPreview(object sender, ColorPreviewEventArgs e)
        {
            ChangeAccentColor(e.Color, false);
        }

        private void colorPickerDropDownInterface_PopupClose(object sender, EventArgs e)
        {
            ChangeAccentColor(_settings.Default.AccentColor);
        }

        private void ChangeAccentColor(Color color, bool save = true)
        {
            styleManagerMain.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(
                    styleManagerMain.MetroColorParameters.CanvasColor, color);

            if (save)
            {
                _settings.Default.AccentColor = color;
                _settings.Save();
            }
        }
        public void AdjustPosition(bool forced = false)
        {
            var s = Screen.AllScreens.Count()-1 >= _preferedMonitor ? _preferedMonitor : 0;
            var screen = Screen.AllScreens[s];
            var t = new TaskBarUtilities.Taskbar(!Screen.PrimaryScreen.Equals(screen));

            var y = t.Handle == IntPtr.Zero ? screen.Bounds.Bottom - Height : t.Location.Y - Height;

            if (y == _lastYVisible && !forced)
                return;

            Top = y;

            Left = screen.Bounds.X + (screen.Bounds.Width / 2) - (Width / 2);

            _lastYVisible = y;
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
            _newStatus = BarStatus.OnTop;

            // Check by processes first
            if ((from p in Process.GetProcesses() from s in _hideFromProcesses where p.ProcessName.ToLower().Equals(s.Name) select p).Any())
            {
                _newStatus = BarStatus.Hidden;
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
                _newStatus = BarStatus.OnTop;

                foreach (var s in _watchedProcesses)
                {
                    try
                    {
                        if (s.Behaviour == WatchedProcessBehaviour.HideIfTitlebarMatches)
                        {
                            if (p.MainWindowTitle.Contains(s.Name))
                            {
                                _newStatus = BarStatus.Hidden;
                                break;
                            }
                        }

                        if (!processName.Equals(s.Name)) continue;

                        switch (s.Behaviour)
                        {
                            case WatchedProcessBehaviour.HideIfIsActive:
                                _newStatus = BarStatus.Hidden;
                                break;

                            case WatchedProcessBehaviour.HideIfIsActiveAndMaximized:
                                if (IsMaximized(p))
                                    _newStatus = BarStatus.Hidden;
                                break;

                            case WatchedProcessBehaviour.SetNotOnTopIfIsActive:
                                _newStatus = BarStatus.NotOnTop;
                                break;
                        }
                    }
                    catch
                    {
                        // Error retrieving the process instance (privileges?)
                    }
                }

                if (_newStatus != _lastStatus)
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
            switch (_newStatus)
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

            _lastStatus = _newStatus;
        }
        #endregion

        /// <summary>
        /// Check the browser availability reported by Windows, prepares the references
        /// </summary>
        private void CheckBrowsers()
        {
            try
            {
                // Check browser availability
                _browsersPanels = new List<RibbonBar>();
                BrowsersPaths = new Dictionary<String, String>();
                CurrentBrowser = _settings.Default.SelectedBrowser;

                var browsers = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet");
                foreach (var br in Utilities.GetItems<ButtonItem>(ribbonBarBrowsers.Items))
                {
                    if (browsers == null)
                    {
                        // No browsers in registry, using just the default one
                        CurrentBrowser = String.Empty;
                        break;
                    }

                    foreach (var key in browsers.GetSubKeyNames())
                    {
                        var tag = (String) br.Tag;
                        if (!key.ToLower().Contains(tag)) continue;

                        // Check if path exists
                        var browserKey = browsers.OpenSubKey(key).OpenSubKey(@"shell\open\command");


                        if (browserKey != null)
                        {
                            var browserPath = browserKey.GetValue(null) as string;
                            if (browserPath == null) continue;

                            var path = browserPath.Replace("\"", "");
                            if (File.Exists(path))
                            {
                                if (!BrowsersPaths.ContainsKey(tag))
                                {
                                    BrowsersPaths.Add(tag, path);
                                    br.Enabled = true;

                                    if (tag == CurrentBrowser || String.IsNullOrEmpty(CurrentBrowser))
                                    {
                                        CurrentBrowser = tag;
                                        br.Checked = true;
                                    }
                                }
                            }
                        }
                    }
                }

                // Select first available browser if none was selected
                if (!String.IsNullOrEmpty(CurrentBrowser)) return;

                foreach (var tag in BrowsersPaths)
                {
                    SwitchSelectedBrowser(tag.Key);
                    break;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("This is the detail: " + GetExceptionDetails(ex));
            }
        }

        public static string GetExceptionDetails(Exception exception)
        {
            var properties = exception.GetType()
                .GetProperties();
            var fields = properties
                .Select(property => new
                {
                    property.Name,
                    Value = property.GetValue(exception, null)
                })
                .Select(x => String.Format("{0} = {1}", x.Name, x.Value != null ? x.Value.ToString() : String.Empty));
            return String.Join("\n", fields);
        }

        private void FormBar_Load(object sender, EventArgs e)
        {
            _watchedProcesses = Utilities.FilterWatchedProcessesList(_structure.WatchedProcesses, new List<WatchedProcessBehaviour>(new [] { 
                WatchedProcessBehaviour.HideIfIsActive, WatchedProcessBehaviour.HideIfIsActiveAndMaximized, WatchedProcessBehaviour.SetNotOnTopIfIsActive, 
                WatchedProcessBehaviour.HideIfTitlebarMatches}));
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
                            Width = w, Height = (int)((1f/(0.9f+(0.1f*_virtualDpi)))*h),
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

                            case TabGroupType.SyncButton:
                                var btn = new ButtonItem {Image = Resources.sync};
                                btn.Tooltip = "Update adSALESapps files";
                                btn.Click += (o, args) => SyncronizeNow(false, true);
                                r.Items.Add(btn);
                                r.Text = g.Name;
                                tab.AttachedControl.Controls.Add(r);
                                left += w;
                                break;

                            case TabGroupType.BrowserPanel:
                                Utilities.CreateRibbonBarFromTemplate(ribbonBarBrowsers.Items, ref r, ref tab);
                                _browsersPanels.Add(r); // Reference to each browser panel 
                                left += w;
                                break;

                            case TabGroupType.SyncPanel:
                                if (!timerSyncronization.Enabled) // Only the first instance
                                {
                                    //Utilities.CreateRibbonBarFromTemplate(ribbonBarSync.Items, ref r, ref tab);
                                    ribbonBarSync.Text = g.Name;
                                    ribbonBarSync.Height = r.Height;
                                    ribbonBarSync.Width = w2;
                                    ribbonBarSync.Left = r.Left;
                                    ribbonBarSync.Top = r.Top;
                                    r.Dispose();

                                    tab.AttachedControl.Controls.Add(ribbonBarSync);
                                    //_syncPanel = r;
                                    SyncronizeNow(true);
                                }
                                left += w2;
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
									ribbonBar.Height = (int)((1f/(0.9f+(0.1f*_virtualDpi)))*h); // DPI Hack
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

        private void SyncronizeNow(Boolean fromTimer=false, Boolean forceSync=false)
        {
            if (_sync == null) return;

            if (fromTimer)
            {
                if (timerSyncronization.Interval == 1)
                    timerSyncronization.Interval = 1000*(60 - (DateTime.Now.Second)); // First syncronization
                else
                    timerSyncronization.Interval = 60*1000;
            }
            timerSyncronization.Start();

            if(_sync.DoSyncronization(forceSync))
                _structure.Tracker.WriteEvent(Activities.ApplicationSync);

            checkBoxItemSyncHourly.Checked = _sync.Hourly;
            checkBoxItemSyncEnable.Checked = _sync.Enabled;
            labelItemSyncLast.Text = _sync.Last == null ? "---" : _sync.Last.Value.ToString("MM/dd/yy h:mm tt");

            if (_sync.Enabled)
            {
                //checkBoxItemSyncEnable.Text = "Sync Enabled";
                checkBoxItemSyncHourly.Enabled = true;
                labelItemSyncNext.ForeColor = SystemColors.HotTrack;
                labelItemSyncNext.Font = new Font(Font.FontFamily, Font.SizeInPoints,FontStyle.Underline);
                labelItemSyncNext.Cursor = Cursors.Hand;
                labelItemSyncNext.Tooltip = "Change your Sync Schedule";
                labelItemSyncLabelNext.ForeColor = Color.Empty;
                labelItemSyncNext.Text = _sync.Next.ToString("MM/dd/yy h:mm tt");
                checkBoxItemSyncEnable.Tooltip = "AdSync is Scheduled to Run";
            }
            else
            {
                //checkBoxItemSyncEnable.Text = "Sync Disabled";
                checkBoxItemSyncHourly.Enabled = false;
                labelItemSyncNext.ForeColor = SystemColors.GrayText;
                labelItemSyncNext.Font = new Font(Font.FontFamily, Font.SizeInPoints,FontStyle.Regular);
                labelItemSyncNext.Cursor = Cursors.Arrow;
                labelItemSyncNext.Tooltip = null;
                labelItemSyncLabelNext.ForeColor = SystemColors.GrayText;
                labelItemSyncNext.Text = "---";
                checkBoxItemSyncEnable.Tooltip = "AdSync is Disabled";
                
            }

            //checkBoxItemSyncHourly.Text = "Sync " +(checkBoxItemSyncHourly.Checked?"Hourly":"Daily");
            checkBoxItemSyncHourly.Tooltip = checkBoxItemSyncHourly.Checked ? "AdSync runs once per hour" : "AdSync runs once each day";
            ribbonBarSync.RecalcLayout();
        }

        private void checkBoxItemSyncEnable_Click(object sender, EventArgs e)
        {
            if (_sync == null) return;
            _sync.Enabled = checkBoxItemSyncEnable.Checked;
            _sync.SaveSettings();
            SyncronizeNow();
        }

        private void checkBoxItemSyncHourly_Click(object sender, EventArgs e)
        {
            if (_sync == null) return;
            _sync.Hourly = checkBoxItemSyncHourly.Checked;
            _sync.Next = _sync.Hourly ? DateTime.Now.AddHours(1) : DateTime.Now.AddDays(1);
            _sync.SaveSettings();
            SyncronizeNow();
        }

        private void LoadSettings()
        {
            try
            {
                // Application list to hide from
                _structure.WatchedProcesses = Utilities.ParseWatchedProcessesFile("hide_list.xml");

                // Program configuration
                var f = Utilities.GetTextFromFile("config.xml");
                _uncollapsedHeight = (int)((0.5 + (0.5 * _virtualDpi)) * float.Parse(Utilities.GetValueRegex("<height>(.*)</height>", f)));
                _collapsedHeight = (int)(((0.25 + (0.75 * _virtualDpi))) * float.Parse(Utilities.GetValueRegex("<collapsedheight>(.*)</collapsedheight>", f)));
                Width = (int)((0.5+(0.5*_virtualDpi))*float.Parse(Utilities.GetValueRegex("<width>(.*)</width>", f)));
                _multiHorizontalPadding = int.Parse(Utilities.GetValueRegex("<multihorizontalpadding>(.*)</multihorizontalpadding>", f));
                _multiVerticalPadding = int.Parse(Utilities.GetValueRegex("<multiverticalpadding>(.*)</multiverticalpadding>", f));
                _disableNonAvailableButtons = Utilities.GetValueRegex("<grayoutnonapproveduser>(.*)</grayoutnonapproveduser>", f) != "false";
                Height = _uncollapsedHeight;

                // Theme
                superTabControlMain.TabStyle = (eSuperTabStyle)(Enum.GetValues(typeof (eSuperTabStyle)).GetValue(Math.Min(5,Math.Max(0,int.Parse(Utilities.GetValueRegex("<theme>(.*)</theme>", f))))));
                styleManagerMain.ManagerStyle = (eStyle)(Enum.GetValues(typeof (eStyle)).GetValue(Math.Min(10,Math.Max(0,int.Parse(Utilities.GetValueRegex("<subtheme>(.*)</subtheme>", f))))));

                if (_settings.Default.AccentColor == Color.Transparent)
                {
                    // Load default color from file
                    ChangeAccentColor(ParseColorOrDefault(Utilities.GetValueRegex("<accent>(.*)</accent>", f), Color.Chocolate), false);
                }
                else
                    ChangeAccentColor(_settings.Default.AccentColor, false);

                timerUpdate.Interval = int.Parse(Utilities.GetValueRegex("<checktime>(.*)</checktime>", f));
                timerChecker.Interval = int.Parse(Utilities.GetValueRegex("<inactivitychecktime>(.*)</inactivitychecktime>", f));
                _maxShortButtons = int.Parse(Utilities.GetValueRegex("<maxshortbuttons>(.*)</maxshortbuttons>", f));

                _sync = new SyncronizationHelper(Utilities.GetValueRegex("<silentsyncsettings>(.*)</silentsyncsettings>", f),
                    Utilities.GetValueRegex("<silentsynctemplate>(.*)</silentsynctemplate>", f),
                    Utilities.GetValueRegex("<silentsyncexe>(.*)</silentsyncexe>", f),
                    Utilities.GetValueRegex("<syncexe>(.*)</syncexe>", f));

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


        internal static Color ParseColorOrDefault(string colorName, Color defaultColor)
        {
            try
            {
                if (!String.IsNullOrEmpty(colorName))
                    return Color.FromName(colorName);
            }
            catch
            {
            }
            return defaultColor;
        }

        private void superTabControlMain_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {
            UncollapseWindow();
            _structure.Tracker.WriteEvent(Activities.ApplicationSwitchTab, e.OldValue == null ? e.NewValue.Text : e.OldValue.Text + "->" + e.NewValue.Text);
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
            _structure.Tracker.WriteEvent(Activities.BrowserSwitch, browser);
            CurrentBrowser = browser;

            // Other panels
            foreach (var ribbon in _browsersPanels)
            {
                foreach (var b in Utilities.GetItems<ButtonItem>(ribbon.Items))
                {
                    if (b.Enabled)
                    {
                        var enabled = b.Tag as String == CurrentBrowser;
                        b.Checked = enabled;
                    }
                }
            }

            SaveSettings();
        }

        private void FormBar_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSettings();

            _structure.Tracker.WriteEvent(Activities.ApplicationClose);
            Application.Exit();
        }

        private void SaveSettings()
        {
            _settings.Default.SelectedBrowser = CurrentBrowser;
            _settings.Default.PreferedMonitor = _preferedMonitor;

            _settings.Save();
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
            Close();
        }

        private void timerSyncronization_Tick(object sender, EventArgs e)
        {
            SyncronizeNow(true);
        }

        private void labelItemSyncNext_Click(object sender, EventArgs e)
        {
            if (_sync != null && _sync.Enabled)
            {
                var f = new FormDateSelector(_sync.Next) { TopMost = true };
                f.Location = Utilities.GetCenterLocation(f.Size, ((Control)((LabelItem) sender).ContainerControl));

                _skipCollapse = true;
                if (f.ShowDialog() == DialogResult.OK)
                {
                    _sync.Next = f.DateTime;
                    _sync.SaveSettings();
                    SyncronizeNow();
                }
                _skipCollapse = false;
            }
        }

        private void FormBar_Shown(object sender, EventArgs e)
        {
            _structure.Tracker.WriteEvent(Activities.ApplicationOpen);
        }

        private void timerMonitorWatcher_Tick(object sender, EventArgs e)
        {
            CheckMultipleMonitors();
        }

        private void CheckMultipleMonitors(bool force = false)
        {
            if (force || _lastMonitorCount != Screen.AllScreens.Count())
            {
                _lastMonitorCount = Screen.AllScreens.Count();
                

                // Create monitor buttons
                if (_lastMonitorCount > 1)
                {
                    buttonItemScreen1.Checked = _preferedMonitor == 0;
                    buttonItemScreen2.Visible = _lastMonitorCount > 1;
                    buttonItemScreen2.Checked = _preferedMonitor == 1;
                    buttonItemScreen3.Visible = _lastMonitorCount > 2;
                    buttonItemScreen3.Checked = _preferedMonitor == 2;
                    buttonItemScreen4.Visible = _lastMonitorCount > 3;
                    buttonItemScreen4.Checked = _preferedMonitor == 3;
                    buttonItemScreen5.Visible = _lastMonitorCount > 4;
                    buttonItemScreen5.Checked = _preferedMonitor == 4;
                    buttonItemScreen6.Visible = _lastMonitorCount > 5;
                    buttonItemScreen6.Checked = _preferedMonitor == 5;
                }
                else
                    itemContainerMonitors.Visible = false;


                //_preferedMonitor = (checkBoxItemMonitor.Checked && _lastMonitorCount > 1) ? 1 : 0;
                AdjustPosition(true);
            }
        }

        private void buttonItemScreen_Click(object sender, EventArgs e)
        {
            var monitor = int.Parse(((ButtonItem) sender).Text)-1;

            if (monitor != _preferedMonitor)
            {
                _preferedMonitor = monitor;
                CheckMultipleMonitors(true);
                SaveSettings();
            }
        }
    }

    [Serializable]
    public struct ColorEx
    {
        private Color m_color;

        public ColorEx(Color color)
        {
            m_color = color;
        }

        [XmlIgnore]
        public Color Color
        { get { return m_color; } set { m_color = value; } }

        [XmlAttribute]
        public string ColorHtml
        {
            get { return ColorTranslator.ToHtml(Color); }
            set { Color = ColorTranslator.FromHtml(value); }
        }

        public static implicit operator Color(ColorEx colorEx)
        {
            return colorEx.Color;
        }

        public static implicit operator ColorEx(Color color)
        {
            return new ColorEx(color);
        }
    }

    /// <summary>
    /// Quick and dirty replacement to avoid permissions problems with the settings
    /// </summary>
    public class Settings
    {
        private readonly string[] _location = { @"C:\Program Files (x86)\newlocaldirect.com", @"C:\Program Files\newlocaldirect.com" };
        private readonly string _settingsFile;
        public readonly SerializedSettings Default;
        private const string Filename = "adbar.xml", Path = @"xml\adbar_settings";

        internal void Save()
        {
            if (String.IsNullOrEmpty(_settingsFile)) return;

            try
            {
                using (var stream = File.CreateText(_settingsFile))
                {
                    var bf = new XmlSerializer(typeof(SerializedSettings));
                    bf.Serialize(stream, Default);
                }
            }
            catch
            {
            }
        }

        public class SerializedSettings
        {
            public string SelectedBrowser { get; set; }
            public int PreferedMonitor { get; set; }
            public ColorEx AccentColor { get; set; }
        }

        public Settings()
        {
            Default = new SerializedSettings();
            Reset();

            // Load file
            foreach (var l in _location)
            {
                if (!Directory.Exists(l)) continue;

                try
                {
                    var tmp = System.IO.Path.Combine(l, Path);
                    _settingsFile = System.IO.Path.Combine(tmp, Filename);

                    if (Directory.Exists(tmp))
                    {
                        // Load settings
                        if (File.Exists(_settingsFile))
                        {
                            try
                            {
                                using (var stream = File.OpenRead(_settingsFile))
                                {
                                    var bf = new XmlSerializer(typeof (SerializedSettings));
                                    Default = (SerializedSettings)bf.Deserialize(stream);
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                    else
                        Directory.CreateDirectory(tmp);
                }
                catch
                {
                    _settingsFile = String.Empty;
                }
                break;
            }

            Save();
        }

        internal void Reset()
        {
            Default.PreferedMonitor = 0;
            Default.AccentColor = Color.Transparent;
            Default.SelectedBrowser = null;
        }
    }

    public enum BarStatus
    {
        OnTop,Hidden,NotOnTop
    }
}
