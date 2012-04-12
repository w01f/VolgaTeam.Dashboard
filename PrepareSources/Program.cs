using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClearZoneInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo rootFolder = new DirectoryInfo(System.Windows.Forms.Application.StartupPath);
            if (rootFolder.Exists)
            {
                DeleteBinariesFromFolder(rootFolder);
                System.Windows.Forms.MessageBox.Show("Ready");
            }
        }

        private static void DeleteBinariesFromFolder(DirectoryInfo folder)
        {
            foreach (DirectoryInfo subFolder in folder.GetDirectories())
            {
                if (subFolder.Name.ToLower().Equals("bin") || subFolder.Name.ToLower().Equals("obj"))
                {
                    try
                    {
                        subFolder.Delete(true);
                    }
                    catch 
                    { 
                    }
                }
                else
                    DeleteBinariesFromFolder(subFolder);
            }
        }
    }
}
