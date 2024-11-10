using System.Collections.Generic;
using UnityEngine.Audio;
using Zombieland.GameScene0.EnvironmentModule;
using Zombieland.GameScene0.RootModule;

namespace Zombieland.GameScene0.GlobalSoundModule
{
    public interface IGlobalSoundController
    {
        IRootController RootController { get; }
        AudioMixer MainAudioMixer { get; }
        List<AudioSourceObject> AudioSourceObjects { get; }
    }
}