using System;
using System.Diagnostics;
using System.IO;

namespace AdBAR
{
    public class ActivityTracker
    {
        private readonly string _programName;
        private StreamWriter _tracklog;

        public ActivityTracker(string programName)
        {
            _programName = programName;
        }

        public static string GetOutgoingLocation(string dir, String SaveLocationName = "", bool prefixSaveLocation = true)
        {
            var p = @"C:\Program Files\newlocaldirect.com\sync\Outgoing";

            if (Directory.Exists(@"C:\Program Files (x86)\newlocaldirect.com"))
                p = @"C:\Program Files (x86)\newlocaldirect.com\sync\Outgoing";

            try
            {
                foreach (string d in Directory.GetDirectories(p))
                    return prefixSaveLocation ? Path.Combine(d, SaveLocationName, dir) : Path.Combine(d, dir);
            }
            catch
            {
            }

            // Should not reach this
            return dir;
        }

        private void CreateOrOpenTrackLog()
        {
            try
            {
                if (_tracklog != null)
                    return;
            }
            catch
            {
            }

            var d = GetOutgoingLocation("user_data\\" + _programName);
            if (!Directory.Exists(d))
                Directory.CreateDirectory(d);

            _tracklog = new StreamWriter(Path.Combine(d, Process.GetCurrentProcess().ProcessName + "__" + DateTime.Now.ToString("MMddyyyy") + ".log"), true);
        }

        public void Close()
        {
            try
            {
                _tracklog.Close();
                _tracklog.Dispose();
                _tracklog = null;
            }
            catch
            {
            }
        }

        public void WriteEvent(Activities activity, String data = "")
        {
            /*if (activity == Activities.ApplicationSync)
                return;*/

            CreateOrOpenTrackLog();
            /*var closeLog = true;*/

            _tracklog.WriteLine("user=\"{0}\" time=\"{3}\" activitytype=\"{1}\" activitydata=\"{2}\"",
                Environment.UserName, activity, data, DateTime.Now);

            //if(closeLog)
                Close();
        }
    }

    public enum Activities
    {
        ApplicationOpen, ApplicationClose, ApplicationError, ApplicationOpenLink,
        ApplicationSwitchTab, BrowserSwitch,
        ApplicationSync
    }
}
