using System.Collections.Generic;
using Zombieland.GameScene0.NPCModule;
using Zombieland.GameScene0.RootModule;

namespace Zombieland.GameScene0.NPCManagerModule
{
    public interface INPCManagerController
    {
        IRootController RootController { get; }
        List<INPCController> ActiveNpcControllers { get; }

        void AddNPCToActive(INPCController npcController);
        void RemoveNPCFromActive(INPCController npcController);
    }
}