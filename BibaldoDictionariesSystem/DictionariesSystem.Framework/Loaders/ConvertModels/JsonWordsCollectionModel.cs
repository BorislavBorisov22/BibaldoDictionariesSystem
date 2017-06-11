using Newtonsoft.Json;
using System.Collections.Generic;

namespace DictionariesSystem.Framework.Loaders.ConvertModels
{
    public class JsonWordsCollectionModel
    {
        [JsonProperty("words")]
        public IEnumerable<JsonWordModel> Words { get; set; }
    }
}
