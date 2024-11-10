using Newtonsoft.Json;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.CharacterDataModule;

namespace Zombieland.GameScene0.CharacterModule.SpawnDeathRespawnModule
{
    public class TestConvertToJSON : MonoBehaviour
    {
        /*
        void Start()
        {
            Serialize();


        }
        void Serialize()
        {

            // Создаем экземпляр CommandA
            SpawnInDefaultPosition spawnInDefaultPosition = new SpawnInDefaultPosition
            {
                DefaultSpawnPosition = new Vector3(1,1,1)
            };
            // Создаем экземпляр CommandHolder, сериализуем его в JSON и десериализуем обратно
            CharacterData characterData = new CharacterData {SpawnMethod = spawnInDefaultPosition};
            var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.Auto};
            string jsonHolder = JsonConvert.SerializeObject(characterData, settings);
            Debug.Log(jsonHolder);
            Deserialize(jsonHolder);
        }

        private void Deserialize(string json)
        {
            var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.Auto};
            CharacterData characterData = JsonConvert.DeserializeObject<CharacterData>(json, settings);

            // Выполняем команду из десериализованного CommandHolder
            characterData.SpawnMethod.Execute();
        }*/

        private void Start()
        {
            Serialize();
        }

        private void Serialize()
        {
            SpawnData spawnData = new SpawnData();
            spawnData.Level1DefaultPosition = new System.Numerics.Vector3(1f, 1f, 1f);
            spawnData.SpawnRadius = 5f;
            spawnData.SpawnType = SpawnType.InPoint;

            CharacterData characterData = new CharacterData { SpawnData = spawnData };
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
            string jsonHolder = JsonConvert.SerializeObject(characterData, settings);
            Debug.Log(jsonHolder);
            Deserialize(jsonHolder);
        }

        private void Deserialize(string json)
        {
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
            CharacterData characterData = JsonConvert.DeserializeObject<CharacterData>(json, settings);

            Debug.Log(characterData.SpawnData.Level1DefaultPosition);
            Debug.Log(characterData.SpawnData.SpawnRadius);
            Debug.Log(characterData.SpawnData.SpawnType);
        }
    }
}