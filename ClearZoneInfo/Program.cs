using System.Diagnostics;
using System.IO;

namespace ClearZoneInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            string streamsFile = "streams.exe";
            string appFolder = string.Format(@"{0}\newlocaldirect.com\app", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            string salesDepotFolder = string.Format(@"{0}\newlocaldirect.com\Sales Depot", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            string applicationsFolder = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\applications", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
            string updateFolder = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\update", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));

            try
            {
                if (File.Exists(streamsFile))
                { 
                    Process streamsProcess = new Process();
                    streamsProcess.StartInfo.CreateNoWindow = true;
                    streamsProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    streamsProcess.StartInfo.FileName = Path.Combine(System.Windows.Forms.Application.StartupPath,streamsFile);

                    streamsProcess.StartInfo.Arguments = "/accepteula -s -d " + "\"" + appFolder + "\"";
                    streamsProcess.Start();

                    streamsProcess.StartInfo.Arguments = "/accepteula -s -d " + "\"" + salesDepotFolder + "\"";
                    streamsProcess.Start();

                    streamsProcess.StartInfo.Arguments = "/accepteula -s -d " + "\"" + applicationsFolder + "\"";
                    streamsProcess.Start();

                    streamsProcess.StartInfo.Arguments = "/accepteula -s -d " + "\"" + updateFolder + "\"";
                    streamsProcess.Start();
                }
            }
            catch
            { 
            }
        }
    }
}
