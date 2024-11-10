using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

namespace Zombieland.GameScene0.UIModule.UIMainModule
{
    public class InitializerInputPrefab
    {
        public Input Input { get; private set; }

        private const string INPUT_MOBILE_PREFAB_NAME = "MainMobileUICanvas";
        private const string INPUT_PC_PREFAB_NAME = "MainPCUICanvas";

        private GameObject _inputSystemGameobject;

        public void Init()
        {
            GameObject eventSystem = new GameObject();
            eventSystem.name = "EventSystem";
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<InputSystemUIInputModule>();

#if UNITY_STANDALONE// || UNITY_EDITOR
            GameObject prefab = Resources.Load<GameObject>(INPUT_PC_PREFAB_NAME);
            _inputSystemGameobject = GameObject.Instantiate(prefab);
#else
            GameObject prefab = Resources.Load<GameObject>(INPUT_MOBILE_PREFAB_NAME);
            _inputSystemGameobject = GameObject.Instantiate(prefab);
#endif
            Input = _inputSystemGameobject.GetComponentInChildren<Input>();
        }

        public GameObject GetInputSystemGameobject()
        {
            return _inputSystemGameobject;
        }
    }
}