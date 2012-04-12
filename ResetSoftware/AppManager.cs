using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Reset
{
    public class AppManager
    {
        private static AppManager _instance = new AppManager();

        public string HelpLinksPath { get; set; }

        public static AppManager Instance
        {
            get 
            {
                return _instance;
            }
        }

        private AppManager()
        {
            this.HelpLinksPath = string.Format(@"{0}\newlocaldirect.com\app\HelpUrls\SetupHelp.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
        }

        public void RunForm()
        {
            Application.Run(new FormMain());
        }

        public void ShowWarning(string text)
        {
            MessageBox.Show(text, "Minibar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public DialogResult ShowWarningQuestion(string text)
        {
            return MessageBox.Show(text, "Minibar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
        }

        public void ShowInformation(string text)
        {
            MessageBox.Show(text, "Minibar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
