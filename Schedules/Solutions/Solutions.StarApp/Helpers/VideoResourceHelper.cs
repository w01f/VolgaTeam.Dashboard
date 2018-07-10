using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Asa.Business.Common.Entities.NonPersistent.ScheduleResources;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Objects.VideoInfo;

namespace Asa.Solutions.StarApp.Helpers
{
	static class VideoResourceHelper
	{
		private static readonly string UtilitiesPath;

		static VideoResourceHelper()
		{
			UtilitiesPath = Path.Combine(GlobalSettings.ApplicationRootPath, "assets", "ffmpeg");
		}

		public static VideoResourceItem AddVideoResource(string filePath, BaseScheduleResourceContainer resourceContainer)
		{
			var resourceItem = resourceContainer.AddResource<VideoResourceItem>();

			if (!Directory.Exists(resourceItem.ResourceFolderPath))
				Directory.CreateDirectory(resourceItem.ResourceFolderPath);

			var sourceFilePath = Path.Combine(resourceItem.ResourceFolderPath,
				String.Format("{0}{1}", VideoResourceItem.VideoSourceFileName, Path.GetExtension(filePath)));

			File.Copy(filePath, sourceFilePath, true);

			var dataFilePath = Path.Combine(resourceItem.ResourceFolderPath, VideoResourceItem.VideoSourceDataFileName);
			ExtractVideoInfo(sourceFilePath, dataFilePath);

			var videoData = FFMpegData.LoadFromFile(dataFilePath);
			GenerateThumbnails(sourceFilePath, resourceItem.ResourceFolderPath, VideoResourceItem.VideoThumbnailFilePrefixName, videoData);

			return resourceItem;
		}

		private static void ExtractVideoInfo(
			string sourceFilePath,
			string destinationFilePath)
		{
			var allowToExit = false;
			var analizerPath = Path.Combine(UtilitiesPath, "ffprobe.exe");
			if (!File.Exists(sourceFilePath) || !File.Exists(analizerPath)) return;
			var videoAnalyzer = new Process
			{
				StartInfo = new ProcessStartInfo(analizerPath, String.Format("-v error -print_format json -show_format -show_streams -select_streams v:0 \"{0}\"", sourceFilePath))
				{
					UseShellExecute = false,
					RedirectStandardError = true,
					RedirectStandardOutput = true,
					CreateNoWindow = true
				},
				EnableRaisingEvents = true
			};
			videoAnalyzer.Exited += (analyzerSender, analyzerE) =>
			{
				var analyzeOutput = videoAnalyzer.StandardOutput.ReadToEnd();
				analyzeOutput += videoAnalyzer.StandardError.ReadToEnd();
				File.WriteAllText(destinationFilePath, analyzeOutput);
				allowToExit = true;
			};
			videoAnalyzer.Start();
			while (!allowToExit)
			{
				Thread.Sleep(2000);
			}
		}

		private static void GenerateThumbnails(
			string sourceFilePath,
			string destinationPath,
			string thumbnailFileNamePrefix,
			FFMpegData ffMpegData)
		{
			var allowToExit = false;
			var converterPath = Path.Combine(UtilitiesPath, "ffmpeg.exe");
			var destinationOutputFilePath = Path.Combine(destinationPath, String.Format("{0}%d.png", thumbnailFileNamePrefix));
			if (!File.Exists(sourceFilePath) || !File.Exists(converterPath)) return;

			int startDelay;
			int endDelay;
			string imageExtractOption = "-vf \"{0},scale=-1:'min(ih,360)'\"";

			if (ffMpegData.Duration < 1)
			{
				startDelay = 5;
				endDelay = 5;
				imageExtractOption = String.Format(imageExtractOption, "fps=1") + " -vframes 1";
			}
			else if (ffMpegData.Duration < 10)
			{
				startDelay = 1;
				endDelay = 0;
				imageExtractOption = String.Format(imageExtractOption, "fps=1");
			}
			else if (ffMpegData.Duration < 30)
			{
				startDelay = 5;
				endDelay = 2;
				imageExtractOption = String.Format(imageExtractOption, "fps=1");
			}
			else if (ffMpegData.Duration < 180)
			{
				startDelay = 5;
				endDelay = 5;
				imageExtractOption = String.Format(imageExtractOption, "fps=1/2");
			}
			else if (ffMpegData.Duration < 300)
			{
				startDelay = 5;
				endDelay = 5;
				imageExtractOption = String.Format(imageExtractOption, "fps=1/6");
			}
			else
			{
				startDelay = 7;
				endDelay = 7;
				imageExtractOption = String.Format(imageExtractOption, "fps=1/30");
			}

			var targetDuration = ffMpegData.Duration - startDelay - endDelay;
			var videoConverter = new Process
			{
				StartInfo = new ProcessStartInfo(
					converterPath,
					String.Format("-i \"{0}\" -ss {2} -t {3} {4} \"{1}\"",
						sourceFilePath,
						destinationOutputFilePath,
						startDelay,
						targetDuration,
						imageExtractOption
						)
					)
				{
					UseShellExecute = false,
					RedirectStandardError = false,
					WindowStyle = ProcessWindowStyle.Hidden,
					RedirectStandardOutput = false,
					CreateNoWindow = true,
				},
				EnableRaisingEvents = true
			};
			videoConverter.Exited += (converterSender, converterE) =>
			{
				allowToExit = true;
			};
			videoConverter.Start();
			while (!allowToExit)
			{
				Thread.Sleep(2000);
			}
		}
	}
}
