#if PLY_GAME

using plyCommon;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.plyGame
{
    [System.Serializable]
    public partial class plyGameAttributeModifierModel
    {
        public UniqueID id;
        public Color color = Color.white;
        public bool addToBonus = false; // Is it a consumable value or a bonus value? A consuamble restores a value, when the max is reached it's clamped. Bonus will increase it's max.
        public int toAdd = 0;
    }
}

#endif