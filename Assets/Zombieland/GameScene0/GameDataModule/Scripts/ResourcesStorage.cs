using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Zombieland.GameScene0.GameDataModule
{
    public class ResourcesStorage : IStorage
    {
        public void SaveDada<T>(string name, T data)
        {
#if UNITY_EDITOR
            var fileName = name + ".txt";
            var filePath = Path.Combine(Application.dataPath, "Zombieland/GameScene0/GameDataModule/Resources", fileName);
            Debug.Log($"<color=blue>Save data to filepath: {filePath}</color>");
            var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.Auto};
            var json = JsonConvert.SerializeObject(data, settings);
            File.WriteAllText(filePath, json);
#endif
        }

        public T GetData<T>(string name)
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