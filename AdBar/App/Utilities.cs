using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace AdBAR
{
    internal static class Utilities
    {
        /// <summary>
        /// Opens a file and reads all the text
        /// </summary>
        /// <param name="textFile">File to open</param>
        /// <returns>Title, empty if failed</returns>
        public static string GetTextFromFile(string textFile)
        {
            string t = "";

            if (File.Exists(textFile))
            {
                try
                {
                    t = File.ReadAllText(textFile);
                }
                catch
                {
                }
            }

            return t;
        }

        /// <summary>
        /// Returns the value of the first matching group
        /// </summary>
        /// <param name="expression">Regular expression</param>
        /// <param name="input">Input text</param>
        /// <returns>Matching text of first group</returns>
        public static String GetValueRegex(String expression, String input)
        {
            try
            {
                return new Regex(expression, RegexOptions.IgnoreCase).Match(input).Groups[1].Value;
            }
            catch
            {
            }
            return "";
        }

        internal static List<string> GetValuesRegex(string expression, string input)
        {
            var l = new List<string>();
            try
            {
                l.AddRange(from Match m in new Regex(expression, RegexOptions.IgnoreCase).Matches(input) select m.Groups[1].Value);
            }
            catch
            {
            }
            return l;
        }

        public static IEnumerable<ButtonItem> GetButtonItems(SubItemsCollection s)
        {
            var items = new List<ButtonItem>();
            foreach (var i in s)
            {
                var btn = i as ButtonItem;

                if (btn != null && btn.Tag != null)
                {
                    items.Add(btn);
                }
                else
                {
                    var sub = i as BaseItem;

                    if (sub != null)
                        if (sub.SubItems.Count > 0)
                            items.AddRange(GetButtonItems(sub.SubItems));

                }
            }
            return items;
        }

        public static void CreateRibbonBarFromTemplate(IEnumerable items, ref RibbonBar r, ref SuperTabItem tab)
        {
            foreach (BaseItem c in items)
                r.Items.Add((BaseItem) c.Clone());

            tab.AttachedControl.Controls.Add(r);
        }

        public static void CreateButtonOrGallery(IList<TabGroupItem> g, ref RibbonBar r, ref SuperTabItem tab, int horizontalPadding=0, int verticalPadding=0, bool checkUser=false)
        {
            if (g.Count == 0)
                return;

            var owner = GetOwner(tab.AttachedControl);

            if (g.Count == 1)
            {
                r.Items.Add(CreateButtonItem(g[0], owner, checkUser));
                tab.AttachedControl.Controls.Add(r);
            }
            else
            {
                var bar = new GalleryContainer {EnableGalleryPopup = false};
                var resize = true;

                foreach (var i in g)
                {
                    var b = CreateButtonItem(i, owner, checkUser);
                    bar.SubItems.Add(b);

                    if (!resize) continue;

                    bar.RecalcSize();
                    bar.DefaultSize = new Size(b.Image.Size.Width + horizontalPadding,
                                               b.Image.Size.Height + verticalPadding);
                    resize = false;
                }

                r.Items.Add(bar);
                tab.AttachedControl.Controls.Add(r);
            }
        }

        private static IWin32Window GetOwner(Control c)
        {
            return c.Parent == null ? c : GetOwner(c.Parent);
        }

        public static ButtonItem CreateButtonItem(TabGroupItem item, IWin32Window owner, bool checkUser)
        {
            var i = item;
            var btn = new ButtonItem
                          {
                              Image = i.Image,
                              Style = eDotNetBarStyle.Metro,
                              ThemeAware = false,
                              Enabled = checkUser==false || i.IsCurrentUserAllowed(),
                              ImagePosition = eImagePosition.Top
                          };

            if (!String.IsNullOrEmpty(i.Tooltip))
                btn.Tooltip = i.Tooltip;

            btn.Click += (o, args) => OpenLink(i, owner);
            return btn;
        }

        public static void OpenLink(TabGroupItem item, IWin32Window owner)
        {
            if(!String.IsNullOrEmpty(FormBar._currentBrowser))
                item.SuggestedBrowser = FormBar._browsersPaths[FormBar._currentBrowser];

            var isAllowed = item.IsCurrentUserAllowed();

            if (isAllowed && !String.IsNullOrEmpty(item.Password))
            {
                var r = new FormPassword(item.Password, item.Title).ShowDialog(owner);

                if (r == DialogResult.Cancel)
                    return;

                isAllowed = r == DialogResult.OK;
            }

            if(!isAllowed)
            {
                MessageBox.Show(owner, "You are not authorized to access this feature", "Not authorized",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            item.Open();
        }

        internal static List<WatchedProcess> ParseWatchedProcessesFile(string path)
        {
            var list = new List<WatchedProcess>();
            if (File.Exists(path))
            {
                const string _type = "type", _name = "name";
                var r = new Regex(@"<Application[\s]*(?<"+_type+">.*?)>(?<"+_name+">.*?)</Application>",
                    RegexOptions.IgnoreCase | RegexOptions.Multiline);

                foreach (Match m in r.Matches(File.ReadAllText(path)))
                {
                    var wp = new WatchedProcess(m.Groups[_name].Value);
                    var type = m.Groups[_type].Value.ToLower();

                    if(!String.IsNullOrEmpty(type))
                    {
                        if(type.StartsWith(_type+"="))
                        {
                            switch(type.Split(new []{'='},2)[1].Replace("\"",""))
                            {
                                case "1":
                                case "active":
                                    wp.Behaviour = WatchedProcessBehaviour.HideIfIsActive;
                                    break;

                                case "2":
                                case "maximized":
                                    wp.Behaviour = WatchedProcessBehaviour.HideIfIsActiveAndMaximized;
                                    break;

                                case "3":
                                case "notontop":
                                    wp.Behaviour = WatchedProcessBehaviour.SetNotOnTopIfIsActive;
                                    break;

                                case "4":
                                case "running":
                                    wp.Behaviour = WatchedProcessBehaviour.HideIfProcessIsRunning;
                                    break;
                            }
                        }
                    }

                    list.Add(wp);
                }
            }
            return list;
        }

        public static List<WatchedProcess> FilterWatchedProcessesList(List<WatchedProcess> originalList, List<WatchedProcessBehaviour> include)
        {
            return originalList.Where(i => include.Any(b => i.Behaviour == b)).ToList();
        }
    }

    internal enum WatchedProcessBehaviour
    {
        HideIfIsActive,
        HideIfIsActiveAndMaximized,
        SetNotOnTopIfIsActive,
        HideIfProcessIsRunning
    }
}