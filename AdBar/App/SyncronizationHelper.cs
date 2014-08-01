using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace AdBAR
{
    class SyncronizationHelper
    {
        private readonly string _settingsFile, _settingsFileTemplate, _silentSyncronizationExe, _syncronizationExe;
        private bool _saveRequired;

        public SyncronizationHelper(string settingsFile, string settingsFileTemplate, string silentSyncronizationExe, string syncronizationExe)
        {
            _settingsFile = settingsFile;
            _silentSyncronizationExe = silentSyncronizationExe;
            _syncronizationExe = syncronizationExe;
            _settingsFileTemplate = Utilities.GetTextFromFile(settingsFileTemplate);
            _saveRequired = false;

            ParseSettings();
            DoSyncronization();
        }

        private void ParseSettings()
        {
            Next = DateTime.Now.AddDays(1);
            Last = DateTime.Now;
            Hourly = false;
            Enabled = true;

            if (File.Exists(_settingsFile))
            {
                var f = Utilities.GetTextFromFile(_settingsFile);

                try
                {
                    Next = DateTime.Parse(Utilities.GetValueRegex("<NextSync>(.*)</NextSync>", f));
                }
                catch
                {
                }

                try
                {
                    Last = DateTime.Parse(Utilities.GetValueRegex("<LastSync>(.*)</LastSync>", f));
                }
                catch
                {
                }

                Hourly = Utilities.GetValueRegex("<SyncHourly>(.*)</SyncHourly>", f)
                    .Equals("true", StringComparison.OrdinalIgnoreCase);
                Enabled = Utilities.GetValueRegex("<SyncEnabled>(.*)</SyncEnabled>", f)
                    .Equals("true", StringComparison.OrdinalIgnoreCase);
            }
            else
                _saveRequired = true;
        }

        internal bool DoSyncronization(bool force = false)
        {
            var sync = false;
            if(DateTime.Now.CompareTo(Next)>=0 || force)
            {
                // Syncronization
                try
                {
                    Process.Start(force?_syncronizationExe:_silentSyncronizationExe);
                    sync = true;
                }
                catch
                {
                }
                Last = DateTime.Now;

                if(!force)
                    Next = Hourly ? DateTime.Now.AddHours(1) : DateTime.Now.AddDays(1);

                _saveRequired = true;
            }
             

            if (_saveRequired)
            {
                SaveSettings();
                _saveRequired = false;
            }

            return sync;
        }

        internal void SaveSettings()
        {
            File.WriteAllText(_settingsFile, String.Format(_settingsFileTemplate, Last.HasValue ? Last.Value.AddSeconds(-Last.Value.Second).ToString():null, Next.AddSeconds(-Next.Second), Hourly, Enabled));
        }

        public DateTime Next { get; set; }

        public DateTime? Last { get; set; }

        public bool Hourly { get; set; }

        public bool Enabled { get; set; }
    }
}
