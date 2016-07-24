using System;

namespace Asa.Common.Core.Objects.RemoteStorage
{
	public class FileProcessingProgressEventArgs : EventArgs
	{
		public string FileName { get; private set; }
		public decimal TotalSize { get; private set; }
		public decimal DownloadedSize { get; private set; }

		public int ProgressPercent => (Int32)((DownloadedSize / TotalSize) * 100);

		public FileProcessingProgressEventArgs(string fileName, decimal totalSize, decimal downloadedSize)
		{
			FileName = fileName;
			TotalSize = totalSize;
			DownloadedSize = downloadedSize;
		}
	}
}
