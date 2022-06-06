using Newtonsoft.Json;
using Shared.Helper;
using Shared.Models;
using Shared.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XamarinBusinessCentral.Services
{
    public class DataStore : IDataStore<Item>
    {
        List<Item> items;

        public DataStore()
        {
            items = new List<Item>();
        }

        public async Task<bool> AddItemAsync(Item Item)
        {
            items.Add(Item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item Item)
        {
            //var oldItem = items.Where((Item arg) => arg.Id == Item.Id).FirstOrDefault();
            //items.Remove(oldItem);
            //items.Add(Item);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.No == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            var productResponse = await GetProductsAsync();

            if (productResponse.Message == null)
            {
                return await Task.FromResult(items);
            }
        
            Ouput Ouput = JsonConvert.DeserializeObject<Ouput>(productResponse.Message.ToString());
            if (Ouput != null)
            {
                items = JsonConvert.DeserializeObject<List<Item>>(Ouput.value);
            }

            return await Task.FromResult(items);
        }

        public async Task<Response<object>> GetProductsAsync()
        {
            RequestBridge Result = new RequestBridge(Constants.URLAzure);
            return await Result.GetResponseAsync();
        }
    }
}