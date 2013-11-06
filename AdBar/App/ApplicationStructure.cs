using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AdBAR
{
    class ApplicationStructure
    {
        public ApplicationStructure(string tabsDefinitionFile)
        {
            var f = Utilities.GetTextFromFile(tabsDefinitionFile);
            Tabs = new List<TabDetails>();

            foreach (Match m in Regex.Matches(f, "<Tab>(.+?)</Tab>", RegexOptions.IgnoreCase | RegexOptions.Singleline))
            {
                // Tab info
                var d = m.Groups[1].Value;
                var tab = new TabDetails(Utilities.GetValueRegex("<Id>(.*)</Id>", d),
                                         Utilities.GetValueRegex("<Name>(.*)</Name>", d),
                                         Utilities.GetValueRegex("<Enabled>(.*)</Enabled>", d) != "false",
                                        Utilities.GetValueRegex("<Visible>(.*)</Visible>", d) != "false");

                // Load tab groups
                tab.Groups = new List<TabGroup>();

                if(Directory.Exists(tab.Id))
                    foreach (var p in Directory.GetDirectories(tab.Id))
                        tab.Groups.Add(new TabGroup(p));

                Tabs.Add(tab);
            }
        }

        public List<WatchedProcess> WatchedProcesses { get; set; }
        public List<TabDetails> Tabs { get; set; }
    }

    internal class WatchedProcess
    {
        public string Name { get; private set; }
        public WatchedProcessBehaviour Behaviour { get; set; }

        public WatchedProcess(string name)
        {
            Name = name.ToLower();
            Behaviour = WatchedProcessBehaviour.HideIfIsActive;
        }
    }

    internal class TabGroup
    {
        public TabGroup(string path)
        {
            Type = TabGroupType.CustomControls;
            Items = new List<TabGroupItem>();

            foreach(var f in Directory.GetFiles(path, "*.xml"))
            {
                if (Path.GetFileNameWithoutExtension(f).ToLower().Contains("settings"))
                {
                    // Fill name and other data
                    var tx = Utilities.GetTextFromFile(f);
					Config = tx;

                    switch (Utilities.GetValueRegex("<content>(.*)</content>", tx))
                    {
                        case "shortbutton":
                            Type = TabGroupType.ShortButton;
                            break;

                        case "longbutton":
                            Type = TabGroupType.LongButton;
                            break;

                        case "browsertoggle":
                            Type = TabGroupType.BrowserPanel;
                            break;
                    }

                    Name = Utilities.GetValueRegex("<groupname>(.*)</groupname>", tx);
                }
                else
                {
                    // Button definition
                    var tx = Utilities.GetTextFromFile(f);
                    var type = Utilities.GetValueRegex("<type>(.*)</type>", tx);
                    var t = new TabGroupItem(Utilities.GetValuesRegex("<path>(.*)</path>", tx),
                                              type == "url" ? LinkType.Url : (type == "exe" ? LinkType.Exe : LinkType.Any), f);

                    t.Tooltip = Utilities.GetValueRegex("<tooltip>(.*?)</tooltip>", tx);
                    t.Title = Utilities.GetValueRegex("<title>(.*?)</title>", tx);
                    t.Password = Utilities.GetValueRegex("<pincode>(.*?)</pincode>", tx);

                    if (tx.Contains("approveduser"))
                    {
                        t.AllowedUsers = Utilities.GetValuesRegex("<approveduser>(.*?)</approveduser>", tx);
                    }
                
                    Items.Add(t);
                }
            }
        }

        public TabGroupType Type;

        public string Name { get; set; }

		public string Config { get; set; }

        public List<TabGroupItem> Items { get; set; }

		public string GetCustomOption(string optionName)
		{
			return !String.IsNullOrEmpty(Config) ?
				Utilities.GetValueRegex(String.Format("<{0}>(.*)</{0}>", optionName), Config) :
				null;
		}
    }

    internal enum LinkType
    {
        Url, Exe,
        Any
    }

    internal class TabGroupItem
    {
        private readonly List<string> _paths;
        private readonly LinkType _type;
        private readonly string _imgPath;

        public TabGroupItem(List<String> paths, LinkType type, string imgPath)
        {
            _paths = paths;
            _type = type;
            SuggestedBrowser = String.Empty;
            AllowedUsers = new List<string>();

            _imgPath = Path.ChangeExtension(imgPath, "png");
        }

        public void Open()
        {
            switch (_type)
            {
                case LinkType.Url:
                    if (String.IsNullOrEmpty(SuggestedBrowser) && File.Exists(SuggestedBrowser))
                        Start(_paths[0],"",true);
                    else
                        Start(SuggestedBrowser, _paths[0]);
                    break;

                case LinkType.Exe:
                case LinkType.Any:
                    Cursor.Current = Cursors.WaitCursor;
                    const string programFiles = "\\program files", programFiles86 = programFiles+" (x86)";
                    foreach (var path in _paths)
                    {
                        var t = path.ToLower();

                        if (File.Exists(t) || _type == LinkType.Any) // Do not test program files thing with this LinkType
                        {
                            Start(path, "", _type == LinkType.Any);
                            break;
                        }

                        // Should make this more pretty, last minute hack
                        // Find program files, hardcoded ugly version. 
                        // Better with: http://stackoverflow.com/questions/194157/c-sharp-how-to-get-program-files-x86-on-windows-vista-64-bit/194191#194191
                        t = path.Contains(programFiles86)
                                ? path.Replace(programFiles86, programFiles)
                                : path.Replace(programFiles, programFiles86);

                        if (File.Exists(t))
                        {
                            Start(path);
                            break;
                        }
                    }
                    Cursor.Current = Cursors.Arrow;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void Start(string path, string args = "", bool useShellExecute = false)
        {
            try
            {
                Process.Start(new ProcessStartInfo(path, args) { UseShellExecute = useShellExecute });
            }
            catch
            {
            }
        }

        public Image Image 
        {
            get { return File.Exists(_imgPath) ? Image.FromFile(_imgPath) : null; }
        }

        public string Tooltip { get; set; }
        public string SuggestedBrowser { get; set; }
        public List<string> AllowedUsers { get; set; }
        public string Password { get; set; }
        public string Title { get; set; }

        internal bool IsCurrentUserAllowed()
        {
            return AllowedUsers.Count <= 0 || AllowedUsers.Any(user => user.Equals(Environment.UserName, StringComparison.OrdinalIgnoreCase));
        }
    }

    internal enum TabGroupType
    {
        ShortButton, LongButton, CustomControls,BrowserPanel
    }

    internal class TabDetails
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public bool Visible { get; set; }

        public TabDetails(string id, string name, bool enabled, bool visible)
        {
            Id = id;
            Name = name;
            Enabled = enabled;
            Visible = visible;
        }

        public List<TabGroup> Groups { get; set; }
    }
}
