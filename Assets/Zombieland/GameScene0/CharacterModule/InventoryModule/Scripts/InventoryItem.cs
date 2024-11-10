namespace Zombieland.GameScene0.CharacterModule.InventoryModule
{
    public struct InventoryItem
    {
        public string Name;
        public int Count;

        public InventoryItem(string name, int count)
        {
            Name = name;
            Count = count;
        }
    }
}
