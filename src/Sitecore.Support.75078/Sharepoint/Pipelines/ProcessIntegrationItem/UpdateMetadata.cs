namespace Sitecore.Support.Sharepoint.Pipelines.ProcessIntegrationItem
{
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Sharepoint.Pipelines.ProcessIntegrationItem;
    using System;
    using System.Drawing;
    using System.IO;

    public class UpdateMetadata
    {
        public virtual void Process(ProcessIntegrationItemArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            if (args.IntegrationItem != null)
            {
                MediaItem item = new MediaItem(args.IntegrationItem);
                if (item != null)
                {
                    Stream mediaStream = item.GetMediaStream();
                    if (mediaStream != null)
                    {
                        Image image = Image.FromStream(mediaStream);
                        if ((image != null) && ((string.IsNullOrEmpty(item.InnerItem.Fields["Width"].Value) || string.IsNullOrEmpty(item.InnerItem.Fields["Height"].Value)) || string.IsNullOrEmpty(item.InnerItem.Fields["Dimensions"].Value)))
                        {
                            using (new EditContext(item.InnerItem, false, false))
                            {
                                item.InnerItem.Fields["Width"].SetValue(image.Width.ToString(), true);
                                item.InnerItem.Fields["Height"].SetValue(image.Height.ToString(), true);
                                item.InnerItem.Fields["Dimensions"].SetValue(string.Format("{0} x {1}", image.Width, image.Height), true);
                            }
                        }
                    }
                }
            }
        }
    }
}

