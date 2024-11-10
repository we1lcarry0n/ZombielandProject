using System;
using System.Collections.Generic;


namespace Zombieland.GameScene0.EnvironmentModule
{
    public interface IEnvironmentController
    {
        event Action<List<AudioSourceObject>> OnLoadedSoundGameobjects;

        string CurrentLevelName { get; }
    }
}