using System.Collections.Generic;
using UnityEngine;


namespace Zombieland.GameScene0.NPCModule.NPCVFXModule
{
    public class VFXCreator
    {
        private INPCVFXController _nPCVFXController;
        private Dictionary<string, GameObject> _vfxs;

        public VFXCreator(INPCVFXController nPCVFXController)
        {
            _nPCVFXController = nPCVFXController;
            _vfxs = new Dictionary<string, GameObject>();
        }

        public GameObject CtreateVFX(string nameVFX, Vector3 spawnPosition, Quaternion spawnRotation)
        {
            if (!_vfxs.ContainsKey(nameVFX))
            {
                GameObject vfx = Resources.Load<GameObject>(nameVFX);
                _vfxs.Add(nameVFX, vfx);
            }

            return GameObject.Instantiate(_vfxs[nameVFX], spawnPosition, spawnRotation);
        }
    }
}