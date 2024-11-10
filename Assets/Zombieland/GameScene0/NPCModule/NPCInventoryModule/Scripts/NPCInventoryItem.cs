namespace Zombieland.GameScene0.NPCModule.NPCInventoryModule
{
    public struct NPCInventoryItem
    {
        public string Name;
        public int Count;

        public NPCInventoryItem(string name, int count)
        {
            Name = name;
            Count = count;
        }
    }
}
