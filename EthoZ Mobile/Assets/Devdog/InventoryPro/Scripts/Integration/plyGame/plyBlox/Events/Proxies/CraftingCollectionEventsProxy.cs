#if PLY_GAME

using UnityEngine;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{

    /// <summary>
    /// Relays all events to plyGame's plyBox
    /// </summary>
    public partial class CraftingCollectionEventsProxy : MonoBehaviour
    {
        private CraftingEventHandler eventHandler { get; set; }
        private CraftingWindowLayoutUI layout { get; set; }
        private CraftingWindowStandardUI standard { get; set; }


        // <inheritdoc />
        public void Start()
        {
            eventHandler = GetComponent<CraftingEventHandler>();
            layout = InventoryManager.instance.craftingLayout;
            standard = InventoryManager.instance.craftingStandard;

            if (layout != null)
            {
                layout.OnCraftSuccess += OnCraftSuccess;
                layout.OnCraftFailed += OnCraftFailed;
                layout.OnCraftProgress += OnCraftProgress;
                layout.OnCraftCancelled += OnCraftCancelled;
            }

            if (standard != null)
            {
                standard.OnCraftSuccess += OnCraftSuccess;
                standard.OnCraftFailed += OnCraftFailed;
                standard.OnCraftProgress += OnCraftProgress;
                standard.OnCraftCancelled += OnCraftCancelled;
            }
        }

        public void OnDestroy()
        {
            if (layout != null)
            {
                layout.OnCraftSuccess -= OnCraftSuccess;
                layout.OnCraftFailed -= OnCraftFailed;
                layout.OnCraftProgress -= OnCraftProgress;
                layout.OnCraftCancelled -= OnCraftCancelled;
            }

            if (standard != null)
            {
                standard.OnCraftSuccess -= OnCraftSuccess;
                standard.OnCraftFailed -= OnCraftFailed;
                standard.OnCraftProgress -= OnCraftProgress;
                standard.OnCraftCancelled -= OnCraftCancelled;
            }
        }


        public void OnCraftSuccess(CraftingProgressContainer.CraftInfo craftInfo)
        {
            if (eventHandler != null)
                eventHandler.OnCraftSuccess(craftInfo);
        }

        public void OnCraftFailed(CraftingProgressContainer.CraftInfo craftInfo)
        {
            if (eventHandler != null)
                eventHandler.OnCraftFailed(craftInfo);
        }

        public void OnCraftProgress(CraftingProgressContainer.CraftInfo craftInfo, float progress)
        {
            if (eventHandler != null)
                eventHandler.OnCraftProgress(craftInfo, progress);
        }

        public void OnCraftCancelled(CraftingProgressContainer.CraftInfo craftInfo, float progress)
        {
            if (eventHandler != null)
                eventHandler.OnCraftCanceled(craftInfo, progress);
        }
    }
}

#endif