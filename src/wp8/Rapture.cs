
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Xna.Framework.Media;

namespace WPCordovaClassLib.Cordova.Commands
{
    public class Rapture : BaseCommand
    {
        public void captureScreen(string strArgs)
        {
            var fileName = String.Format("AppScreenShot{0:}.jpg", DateTime.Now.Ticks);

            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {

                WriteableBitmap bmpCurrentScreenImage = new WriteableBitmap((int)Application.Current.RootVisual.RenderSize.Width,
                                                                            (int)Application.Current.RootVisual.RenderSize.Height);
                bmpCurrentScreenImage.Render(Application.Current.RootVisual, new MatrixTransform());
                bmpCurrentScreenImage.Invalidate();
                SaveToMediaLibrary(bmpCurrentScreenImage, fileName, 100);

                DispatchCommandResult(new PluginResult(PluginResult.Status.OK, "All is good!"));
            });
        }

        private void SaveToMediaLibrary(WriteableBitmap bitmap, string name, int quality)
        {
            using (var stream = new MemoryStream())
            {
                // Save the picture to the Windows Phone media library.
                bitmap.SaveJpeg(stream, bitmap.PixelWidth, bitmap.PixelHeight, 0, quality);
                stream.Seek(0, SeekOrigin.Begin);
                new MediaLibrary().SavePicture(name, stream);
            }
        }
    }

}