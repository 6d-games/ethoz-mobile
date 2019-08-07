#if PLY_GAME

using Devdog.InventoryPro.Integration.plyGame;

namespace Devdog.InventoryPro
{
    public partial class ItemManager
    {
        public plyGameAttributeDatabaseModel[] plyAttributes
        {
            get { return database.plyAttributes; }
            set { database.plyAttributes = value; }
        }
    }
}


#endif