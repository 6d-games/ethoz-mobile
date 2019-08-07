#if PLY_GAME

using UnityEngine;

namespace Devdog.InventoryPro.Integration.plyGame
{
    [AddComponentMenu(InventoryPro.AddComponentMenuPath + "Integration/plyGame/Windows/plyCharacter")]
    public class plyCharacterUI : CharacterUI
    {
        protected override void Start()
        {
            base.Start();
        }

        protected override void UseDefaultDataProviders()
        {
            base.UseDefaultDataProviders();
        }
    }
}

#endif