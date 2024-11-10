using Newtonsoft.Json;
using UnityEngine;

namespace Zombieland.GameScene0.ImpactModule
{
    public class Deserializator
    {
        public Impact DeserializeImpact(string ImpactName)
        {
            var textAsset = Resources.Load<TextAsset>(ImpactName);
            if (textAsset == null)
            {
                Debug.LogError("Cannot find file: " + ImpactName);
                return null;
            }
            var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.Auto};
            var impact = JsonConvert.DeserializeObject<Impact>(textAsset.text, settings);
            return impact;
        }
    }
}
