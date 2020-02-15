using System;
using System.Xml;
using Asa.Business.Media.Configuration;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Output;
using Asa.Common.Core.Objects.RemoteStorage;
using Asa.Media.Controls.BusinessClasses.Output.SlideOutputConfiguration;

namespace Asa.Media.Controls.BusinessClasses.Output
{
    public class OutputManager
    {
        public OutputColorList ScheduleColors { get; private set; }
        public OutputColorList SnapshotColors { get; private set; }
        public OutputColorList OptionsColors { get; private set; }
        public OutputColorList CalendarColors { get; private set; }

        public ScheduleSlideOutputConfiguration ProgramScheduleOutputConfiguration { get; private set; }
        public ScheduleSlideOutputConfiguration SnapshotOutputConfiguration { get; private set; }
        public ScheduleSlideOutputConfiguration OptionsOutputConfiguration { get; private set; }
        public DigitalSlideOutputConfiguration DigitalSlideOutputConfiguration { get; private set; }

        public event EventHandler<EventArgs> ColorCollectionChanged;
        public event EventHandler<EventArgs> SelectedColorChanged;

        public StorageDirectory ContractTemplateFolder { get; private set; }

        public void Init()
        {
            ScheduleColors = new OutputColorList();
            SnapshotColors = new OutputColorList();
            OptionsColors = new OutputColorList();
            CalendarColors = new OutputColorList();

            LoadSlideOutputConfiguration();

            UpdateColors();

            UpdateContractTemplates();
        }

        public void UpdateColors()
        {
            var colorListFile = new StorageFile(Common.Core.Configuration.ResourceManager.Instance.ScheduleSlideTemplatesFolder.RelativePathParts
                .Merge("colors.txt"));
            if (!colorListFile.ExistsLocal())
                AsyncHelper.RunSync(async () => await colorListFile.Download());

            ScheduleColors.Load(colorListFile);
            SnapshotColors.Load(colorListFile);
            OptionsColors.Load(colorListFile);

            CalendarColors.Load(
                new StorageDirectory(Common.Core.Configuration.ResourceManager.Instance.CalendarSlideTemplatesFolder.RelativePathParts
                    .Merge(new[]
                    {
                        "broadcast_cal",
                        "broadcast_images",
                    })));
            ColorCollectionChanged?.Invoke(this, EventArgs.Empty);
        }

        private void LoadSlideOutputConfiguration()
        {
            ProgramScheduleOutputConfiguration = new ScheduleSlideOutputConfiguration();
            SnapshotOutputConfiguration = new ScheduleSlideOutputConfiguration();
            OptionsOutputConfiguration = new ScheduleSlideOutputConfiguration();
            DigitalSlideOutputConfiguration = new DigitalSlideOutputConfiguration();

            if (ResourceManager.Instance.SlideOutputConfigFile.ExistsLocal())
            {
                var document = new XmlDocument();
                document.Load(ResourceManager.Instance.SlideOutputConfigFile.LocalPath);

                ProgramScheduleOutputConfiguration.EnablePrograms =
                    Boolean.Parse(document.SelectSingleNode(@"//Config/SelectedSlides/Schedule/Programs")?.InnerText ?? "false");
                ProgramScheduleOutputConfiguration.EnableProgramsDigital =
                    Boolean.Parse(document.SelectSingleNode(@"//Config/SelectedSlides/Schedule/ProgramsDigital")?.InnerText ?? "false");
                ProgramScheduleOutputConfiguration.EnableSummary =
                    Boolean.Parse(document.SelectSingleNode(@"//Config/SelectedSlides/Schedule/Summary")?.InnerText ?? "false");

                SnapshotOutputConfiguration.EnablePrograms =
                    Boolean.Parse(document.SelectSingleNode(@"//Config/SelectedSlides/Snapshot/Programs")?.InnerText ?? "false");
                SnapshotOutputConfiguration.EnableProgramsDigital =
                    Boolean.Parse(document.SelectSingleNode(@"//Config/SelectedSlides/Snapshot/ProgramsDigital")?.InnerText ?? "false");
                SnapshotOutputConfiguration.EnableSummary =
                    Boolean.Parse(document.SelectSingleNode(@"//Config/SelectedSlides/Snapshot/Summary")?.InnerText ?? "false");

                OptionsOutputConfiguration.EnablePrograms =
                    Boolean.Parse(document.SelectSingleNode(@"//Config/SelectedSlides/Options/Programs")?.InnerText ?? "false");
                OptionsOutputConfiguration.EnableProgramsDigital =
                    Boolean.Parse(document.SelectSingleNode(@"//Config/SelectedSlides/Options/ProgramsDigital")?.InnerText ?? "false");
                OptionsOutputConfiguration.EnableSummary =
                    Boolean.Parse(document.SelectSingleNode(@"//Config/SelectedSlides/Options/Summary")?.InnerText ?? "false");

                DigitalSlideOutputConfiguration.EnableProducts =
                    Boolean.Parse(document.SelectSingleNode(@"//Config/SelectedSlides/Digital/Products")?.InnerText ?? "false");
                DigitalSlideOutputConfiguration.EnableStrategies =
                    Boolean.Parse(document.SelectSingleNode(@"//Config/SelectedSlides/Digital/Strategies")?.InnerText ?? "false");
                DigitalSlideOutputConfiguration.EnablePlanners =
                    Boolean.Parse(document.SelectSingleNode(@"//Config/SelectedSlides/Digital/Planners")?.InnerText ?? "false");
                DigitalSlideOutputConfiguration.EnablePackages =
                    Boolean.Parse(document.SelectSingleNode(@"//Config/SelectedSlides/Digital/Package")?.InnerText ?? "false");
                DigitalSlideOutputConfiguration.EnableWrapUp =
                    Boolean.Parse(document.SelectSingleNode(@"//Config/SelectedSlides/Digital/WrapUp")?.InnerText ?? "false");
                DigitalSlideOutputConfiguration.EnableAlaCart =
                    Boolean.Parse(document.SelectSingleNode(@"//Config/SelectedSlides/Digital/AlaCart")?.InnerText ?? "false");
            }
        }

        public void UpdateContractTemplates()
        {
            ContractTemplateFolder = new StorageDirectory(Common.Core.Configuration.ResourceManager.Instance.ScheduleSlideTemplatesFolder.RelativePathParts
                .Merge(new[]
                {
                    SlideSettingsManager.Instance.SlideSettings.SlideFolder.ToLower(),
                    String.Format("{0} Slides",MediaMetaData.Instance.DataTypeString),
                    "legal"
                }));

            AsyncHelper.RunSync(async () =>
            {
                var remoteFolderExists = await ContractTemplateFolder.Exists(true, true);
                if (remoteFolderExists && !ContractTemplateFolder.ExistsLocal())
                    await ContractTemplateFolder.Allocate(false);
                else if (!remoteFolderExists)
                    Utilities.DeleteFolder(ContractTemplateFolder.LocalPath);
            });
        }

        public void RaiseSelectedColorChanged()
        {
            SelectedColorChanged?.Invoke(this, EventArgs.Empty);
        }

        private string GetScheduleTemplateFile(string[] fileName)
        {
            var file = new StorageFile(Common.Core.Configuration.ResourceManager.Instance.ScheduleSlideTemplatesFolder.RelativePathParts
                .Merge(new[]
                    {
                        SlideSettingsManager.Instance.SlideSettings.SlideFolder.ToLower(),
                        String.Format("{0} Slides",MediaMetaData.Instance.DataTypeString)
                    })
                .Merge(fileName));

            AsyncHelper.RunSync(async () => await file.Download());

            return file.LocalPath;
        }

        public string GetMediaOneSheetFile(
            string color,
            bool includeDigital,
            int programsPerSlide,
            int spotsPerSlide)
        {
            return GetScheduleTemplateFile(new[]
            {
                "tables",
                color,
                includeDigital ? "3_broadcast_and_digital" : "1_broadcast_only",
                String.Format("{0}_programs",programsPerSlide),
                String.Format("{0}-{1}.pptx",programsPerSlide,spotsPerSlide)
            });
        }

        public string GetDigitalOneSheetFile(
            string color,
            int recordsPerSlide)
        {
            return GetScheduleTemplateFile(new[]
            {
                "tables",
                color,
                "2_digital_only",
                String.Format("{0}_products", recordsPerSlide),
                String.Format("{0}-0.pptx", recordsPerSlide)
            });
        }

        public string GetDigitalStrategyFile(
            int recordsPerSlide)
        {
            return GetScheduleTemplateFile(new[]
            {
                "digital_strategies",
                String.Format("digital_strategies_{0}.pptx", recordsPerSlide)
            });
        }

        public string GetSnapshotItemFile(
            string color,
            bool showLogo,
            int programsPerSlide,
            string slideSuffix
            )
        {
            return GetScheduleTemplateFile(new[]
            {
                "snapshot",
                color,
                showLogo ? "logo" : "no_logo",
                String.Format("1s{0}r",programsPerSlide),
                String.Format("{0}rows_{1}.pptx",programsPerSlide,slideSuffix)
            });
        }

        public string GetSnapshotSummaryFile(string color, int columnsCount)
        {
            return GetScheduleTemplateFile(new[]
            {
                "snapshot",
                color,
                "summary",
                String.Format("snapshot_summary_{0}.pptx",columnsCount)
            });
        }

        public string GetOptionsItemFile(
            string color,
            bool showLogo
            )
        {
            return GetScheduleTemplateFile(new[]
                {
                    "options",
                    color,
                    String.Format("options{0}.pptx",showLogo ? "_logo" : String.Empty)
                });
        }

        public string GetOptionsSummaryFile(string color, int columnsCount)
        {
            return GetScheduleTemplateFile(new[]
            {
                "options",
                color,
                "summary",
                String.Format("options_summary_{0}.pptx",columnsCount)
            });
        }

        public string GetOptionsColumnsWidthFile()
        {
            return GetScheduleTemplateFile(new[]
            {
                "options",
                "table_column_widths.txt"
            });
        }

        private string GetCalendarTemplateFile(string[] fileName)
        {
            var file = new StorageFile(Common.Core.Configuration.ResourceManager.Instance.CalendarSlideTemplatesFolder.RelativePathParts
                .Merge(new[]
                    {
                        "broadcast_cal"
                    })
                .Merge(fileName));

            return file.LocalPath;
        }

        public string GetCalendarFile(bool showLogo, int daysCount)
        {
            return GetCalendarTemplateFile(new[]
            {
                "broadcast_slides",
                String.Format("Broadcast_{0}_{1}_{2}.pptx",
                    showLogo ? "logo" : "no_logo",
                    daysCount,
                    SlideSettingsManager.Instance.SlideSettings.SlideFolder.Replace("Slides", ""))
            });
        }

        public string GetCalendarBackgroundFile(string color, DateTime calendarMonthDate, bool showBigDates)
        {
            return GetCalendarTemplateFile(new[]
            {
                "broadcast_images",
                color,
                calendarMonthDate.ToString("yyyy"),
                String.Format("{0}{1}.png", calendarMonthDate.ToString("MMM").ToLower(), (showBigDates ? "1" : "2"))
            });
        }
    }
}