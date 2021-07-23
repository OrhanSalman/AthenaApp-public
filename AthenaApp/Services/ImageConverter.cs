using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AthenaApp.Services
{
    class ImageConverter
    {
        public async Task<ImageSource> NewImage(byte[] imageArr)
        {
            return ImageSource.FromStream(() => new MemoryStream(imageArr));

        }
    }
}
