using Shared.Helper;
using System;
using System.IO;
using Xamarin.Forms;

namespace Shared.Models
{
    public class Item : BaseViewModel
    {
        public string No { get; set; }
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitCost { get; set; }
        public string? UnitOfMeasure { get; set; }

        public string Picture { get; set; }

        private byte[] ImageBinary;

        private ImageSource _PictureSource;
        public ImageSource PictureSource 
        {
            get
            {
                if (Picture == "NoPicture.png")
                {
                    return Picture;
                }

                ImageBinary = Convert.FromBase64String(Picture);

                return _PictureSource = ImageSource.FromStream(() => new MemoryStream(ImageBinary));
            }
            
            set => SetProperty(ref _PictureSource, value);
        }

        string _UnitPriceTxt;
        public string UnitPriceTxt
        {
            get
            {
                return _UnitPriceTxt = $"{UnitPrice.ToString("C")}";  ;
            }

            set => SetProperty(ref _UnitPriceTxt, value);
        }

    }
}

