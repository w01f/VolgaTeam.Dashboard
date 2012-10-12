using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace RadioScheduleBuilder.BusinessClasses
{
    public class ThemeManager
    {
        private static ThemeManager _instance = new ThemeManager();

        public List<Theme> Themes { get; private set; }

        public static ThemeManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private ThemeManager()
        {
            this.Themes = new List<Theme>();
            Load();
        }

        private void Load()
        {
            this.Themes.Clear();
            string themesRootPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\ScheduleBuilders\{1}\!SlideThemes", new object[] { System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles), ConfigurationClasses.SettingsManager.Instance.SlideFolder });
            if (Directory.Exists(themesRootPath))
                foreach (string themeFile in Directory.GetFiles(themesRootPath, "*.pot"))
                    this.Themes.Add(new Theme(themeFile));
        }
    }

    public class Theme
    {
        public string FilePath { get; private set; }
        public Image Image { get; private set; }
        public Image Thumbnail { get; private set; }

        public string Name
        {
            get
            {
                return Path.GetFileNameWithoutExtension(this.FilePath);
            }
        }

        public Theme(string filePath)
        {
            this.FilePath = filePath;
            string imagePath = Path.ChangeExtension(this.FilePath, "png");
            if (File.Exists(imagePath))
            {
                this.Image = new Bitmap(imagePath);
                this.Thumbnail = this.Image.GetThumbnailImage(this.Image.Width / 3, this.Image.Height / 3, null, IntPtr.Zero);
            }
        }
    }
}
