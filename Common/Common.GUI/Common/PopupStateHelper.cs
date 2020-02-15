using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Objects.RemoteStorage;
using DevExpress.XtraEditors.Repository;

namespace Asa.Common.GUI.Common
{
    public class PopupStateHelper
    {
        private const string StorageName = "PopupSize.xml";
        private readonly RepositoryItemPopupContainerEdit _repositoryItem;
        private readonly StorageFile _stateStorageFile;

        private PopupStateHelper(RepositoryItemPopupContainerEdit targetRepositoryItem, StorageDirectory storagePath, string filePrefix)
        {
            _repositoryItem = targetRepositoryItem;
            _stateStorageFile = new StorageFile(storagePath.RelativePathParts.Merge(String.Format("{0}-{1}", filePrefix, StorageName)));
            _repositoryItem.Closed += (o, e) => SaveState();

            LoadState();
        }

        public static PopupStateHelper Init(RepositoryItemPopupContainerEdit targetRepositoryItem, StorageDirectory storagePath, string filePrefix)
        {
            return new PopupStateHelper(targetRepositoryItem, storagePath, filePrefix);
        }

        private void LoadState()
        {
            int? width = null, height = null;
            if (_stateStorageFile.ExistsLocal())
            {
                var document = new XmlDocument();
                document.Load(_stateStorageFile.LocalPath);

                var node = document.SelectSingleNode(@"/Size/Width");
                if (node != null)
                {
                    int temp;
                    if (Int32.TryParse(node.InnerText, out temp))
                        width = temp;
                }
                node = document.SelectSingleNode(@"/Size/Height");
                if (node != null)
                {
                    int temp;
                    if (Int32.TryParse(node.InnerText, out temp))
                        height = temp;
                }
            }
            if (width.HasValue && height.HasValue)
                _repositoryItem.PopupFormSize = new Size(width.Value, height.Value);
        }

        private void SaveState()
        {
            var width = _repositoryItem.PopupControl.Width;
            var height = _repositoryItem.PopupControl.Height;

            _repositoryItem.PopupFormSize = new Size(width, height);

            var xml = new StringBuilder();
            xml.AppendLine(@"<Size>");
            xml.AppendLine(@"<Width>" + width + @"</Width>");
            xml.AppendLine(@"<Height>" + height + @"</Height>");
            xml.AppendLine(@"</Size>");
            using (var sw = new StreamWriter(_stateStorageFile.LocalPath, false))
            {
                sw.Write(xml);
                sw.Flush();
            }
        }
    }
}
