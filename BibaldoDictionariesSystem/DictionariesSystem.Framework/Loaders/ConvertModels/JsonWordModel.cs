using Newtonsoft.Json;

namespace DictionariesSystem.Framework.Loaders.ConvertModels
{
    public class JsonWordModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("speechPart")]
        public string SpeechPart { get; set; }
    }
}
