using System;
using System.Diagnostics;
using System.IO;
using Xamarin.Forms;

namespace XamarinBusinessCentral.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;

        string no;
        public string No
        {
            get => no;
            set => SetProperty(ref no, value);
        }

        private string description;
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        private string price;
        public string Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }

        string picture;
        public string Picture
        {
            get
            {
                return picture;
            }

            set
            {
                picture = value;

                if (picture != "NoPicture.png")
                {
                    ImageBinary = Convert.FromBase64String(picture);

                    PictureSource = ImageSource.FromStream(() => new MemoryStream(ImageBinary));
                }
                else
                {
                    PictureSource = "NoPicture.png";
                }

            }
        }

        private byte[] ImageBinary;

        private ImageSource _PictureSource;
        public ImageSource PictureSource
        {
            get => _PictureSource;
            set => SetProperty(ref _PictureSource, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                Shared.Models.Item item = await DataStore.GetItemAsync(itemId);
                No = item.No;
                Description = item.Description;
                Price = item.UnitPriceTxt;
                Picture = item.Picture;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
