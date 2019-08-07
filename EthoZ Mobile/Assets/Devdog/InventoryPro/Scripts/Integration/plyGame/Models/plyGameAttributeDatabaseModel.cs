#if PLY_GAME

using plyCommon;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.plyGame
{
    [System.Serializable]
    public partial class plyGameAttributeDatabaseModel
    {
        [SerializeField]
        public UniqueID ID;
        public bool show;
        public string category;

        public plyGameAttributeDatabaseModel(UniqueID id, string category, bool show)
        {
            this.ID = id;
            this.category = category;
            this.show = show;
        }
    }
}

#endif