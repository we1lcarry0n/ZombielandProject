using Newtonsoft.Json;
using UnityEngine;

namespace Zombieland.GameScene0.GameDataModule
{
    public class PlayerPrefsStorage : IStorage
    {
        public void SaveDada<T>(string name, T data)
        {
            var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.Auto};
            var json = JsonConvert.SerializeObject(data, settings);
            PlayerPrefs.SetString(name, json);
            PlayerPrefs.Save();
        }

        public T GetData<T>(string name)
        {
            if (!PlayerPrefs.HasKey(name))
            {
                SaveDada(name, GetDataFromResources<T>(name));
            }
            var json = PlayerPrefs.GetString(name);
            var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.Auto};
            var data = JsonConvert.DeserializeObject<T>(json, settings);
            return data;
        }
        
        private T GetDataFromResources<T>(string name)
        {
            TextAsset textAsset = Resources.Load<TextAsset>(name);
            if (textAsset == null)
            {
                Debug.LogError("Cannot find file at " + name);
                return default;
            }
            var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.Auto};
            var data = JsonConvert.DeserializeObject<T>(textAsset.text, settings);
            return data;
        }
    }
}