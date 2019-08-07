#if UMA

using System.Collections;
using UMA;

namespace Devdog.InventoryPro.Integration.UMA
{

    public class UMAInventoryPlayer : InventoryPlayer
    {


        private UMAData umaData { get; set; }

        protected IEnumerator Start()
        {
            yield return null; // Wait for UMA

            umaData = GetComponent<UMAData>();
            umaData.OnCharacterUpdated += UMACharacterUpdated;
        }
        
        private void UMACharacterCreated(UMAData umaData)
        {

        }

        private void UMACharacterUpdated(UMAData data)
        {
            InventoryUtility.SetLayerRecursive(gameObject, InventorySettingsManager.instance.settings.localPlayerLayer);

        }
    }
}

#endif