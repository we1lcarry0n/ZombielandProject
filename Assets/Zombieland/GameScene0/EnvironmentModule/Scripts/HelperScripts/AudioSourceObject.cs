using System;
using UnityEngine;

namespace Zombieland.GameScene0.EnvironmentModule
{
    [Serializable]
    public class AudioSourceObject
    {
        public GameObject AudioSourceObjectInScene;
        public PlayMode PlayMode;
        public float IntervalFixed;
        public float MinIntervalRandom;
        public float MaxIntervalRandom;
    }
}