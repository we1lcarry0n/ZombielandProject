using System.Collections.Generic;
using Zombieland.GameScene0.NPCModule;
using Zombieland.GameScene0.RootModule;

namespace Zombieland.GameScene0.NPCManagerModule
{
    public class NPCManagerController : Controller, INPCManagerController
    {
        public IRootController RootController { get; private set; }
        public List<INPCController> ActiveNpcControllers { get; private set; }


        public NPCManagerController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            RootController = parentController as IRootController;
            ActiveNpcControllers = new List<INPCController>();
        }

        public void AddNPCToActive(INPCController npcController)
        {
            ActiveNpcControllers.Add(npcController);
        }

        public void RemoveNPCFromActive(INPCController npcController)
        {
            ActiveNpcControllers.Remove(npcController);
        }

        protected override void CreateHelpersScripts()
        {
            // This controller doesn’t have any helpers scripts at the moment.
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            var npcSpawnDataList = RootController.GameDataController.GetData<List<NPCSpawnData>>("NPCSpawnData");
            foreach (var npcSpawnData in npcSpawnDataList)
            {
                INPCController npcController = new NPCController(this, null, npcSpawnData);
                subsystemsControllers.Add((IController)npcController);
                AddNPCToActive(npcController);
            }
        }
    }
}