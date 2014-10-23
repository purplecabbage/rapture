

using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Info;
using System.IO.IsolatedStorage;
using System.Windows.Resources;
using System.IO;
using System.Diagnostics;
using System.Windows.Media.Imaging;
using Microsoft.Xna.Framework.Media;

namespace WPCordovaClassLib.Cordova.Commands
{
    public class Rapture : BaseCommand
    {
        public void captureScreen(string strArgs)
        {
            var fileName = String.Format("WmDev_{0:}.jpg", DateTime.Now.Ticks);

            WriteableBitmap bmpCurrentScreenImage = new WriteableBitmap((int)Application.Current.RootVisual.RenderSize.Width,
                                                                        (int)Application.Current.RootVisual.RenderSize.Height);
            bmpCurrentScreenImage.Render(Application.Current.RootVisual, new MatrixTransform());
            bmpCurrentScreenImage.Invalidate();
            SaveToMediaLibrary(bmpCurrentScreenImage, fileName, 100);
            
            //MessageBox.Show("Captured image " + fileName + " Saved Sucessfully", "WmDev Capture Screen", MessageBoxButton.OK);

            //currentFileName = fileName;
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