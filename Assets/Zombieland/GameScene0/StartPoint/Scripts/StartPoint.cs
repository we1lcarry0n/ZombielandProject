using UnityEngine;
using Zombieland.GameScene0.RootModule;

namespace Zombieland.GameScene0.StartPoint
{
    public class StartPoint : MonoBehaviour
    {
        private IController _rootController;
        
        void Start()
        {
            _rootController = new RootController(null, null);
            _rootController.Enable();
        }

        private void OnDestroy()
        {
            _rootController.Disable();
        }

         /*#region TEST
         private void CreateTestCharacterData()
         {
             var newCharData = new Zombieland.CharacterModule.CharacterDataModule.CharacterData();
             newCharData.MaxAcceleration = 0.1f;
             newCharData.MaxSpeed = 5f;
             this.GameDataController.SaveDada("TestCharacterData", newCharData);
         }
        
         private void TestLoadingOfCharacterData()
         {
             var charData = this.GameDataController.GetData<Zombieland.CharacterModule.CharacterDataModule.CharacterData>("TestCharacterData");
             Debug.Log($"<color=blue>Acceleration of character = {charData.MaxAcceleration}</color>");
         }
         #endregion*/
    }
}
