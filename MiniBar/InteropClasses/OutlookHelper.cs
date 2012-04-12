using System;
using System.IO;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace MiniBar.InteropClasses
{
    public class OutlookHelper
    {
        private static OutlookHelper instance = new OutlookHelper();

        private OutlookHelper()
        {
        }

        public static OutlookHelper Instance
        {
            get
            {
                return instance;
            }
        }

        private Outlook.Application outlookObject;

        public bool Open()
        {
            try
            {
                outlookObject =
                    System.Runtime.InteropServices.Marshal.GetActiveObject("Outlook.Application") as Outlook.Application;
            }
            catch
            {
                outlookObject = null;
            }
            if (outlookObject == null)
            {
                try
                {
                    outlookObject = new Outlook.Application();
                    Outlook.MAPIFolder folder = (outlookObject.GetNamespace("MAPI")).GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderInbox);
                    folder.Display();
                    outlookObject.Explorers.Add(folder, Microsoft.Office.Interop.Outlook.OlFolderDisplayMode.olFolderDisplayNormal);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        public void Close()
        {
            outlookObject = null;
        }

        public void CreateMessage(string attachment)
        {
            try
            {
                Outlook.MailItem mi = (Outlook.MailItem)outlookObject.CreateItem(Outlook.OlItemType.olMailItem);
                mi.Attachments.Add(attachment, Outlook.OlAttachmentType.olByValue, 1, "Attachment");
                mi.Display(new object());
                outlookObject = null;
            }
            catch (Exception e)
            {
                AppManager.Instance.ShowWarning(e.Message);
            }
        }

        public void CreateMessage(FileInfo[] attachment)
        {
            try
            {
                Outlook.MailItem mi = (Outlook.MailItem)outlookObject.CreateItem(Outlook.OlItemType.olMailItem);
                foreach (FileInfo file in attachment)
                    mi.Attachments.Add(file.FullName, Outlook.OlAttachmentType.olByValue, 1, "Attachment");
                mi.Display(new object());
                outlookObject = null;
            }
            catch (Exception e)
            {
                AppManager.Instance.ShowWarning(e.Message);
            }
        }

        public void CreateMessageToBilly()
        {
            Outlook.MailItem mi = (Outlook.MailItem)outlookObject.CreateItem(Outlook.OlItemType.olMailItem);
            mi.Recipients.Add("billy@newlocaldirect.com");
            mi.Subject = "";
            mi.Display(new object());
            outlookObject = null;

        }
    }
}

