#if UMA

using System.Collections.Generic;
using Devdog.General;
using Devdog.General.ThirdParty.UniLinq;
using UMA;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.UMA
{
    public class UMAEquippableInventoryItem : EquippableInventoryItem
    {
        [System.Serializable]
        public class UMAEquipSlotData 
        {
            [Tooltip("The UMA slot name of the character you want to equip it to. \nExample: MaleTorso")]
            [Required]
            public SlotDataAsset umaEquipSlot;

            public bool umaOverrideColor = false;
            public Color umaOverlayColor = Color.white;

            [Tooltip("The overlay object to equip. Use the slot to define it's equip location.")]
            [Required]
            public OverlayDataAsset umaOverlayDataAsset;

            public SlotDataAsset umaSlotDataAsset;
            public SlotDataAsset umaReplaceSlot;

            public SlotData umaReplacedSlot;
            public SlotData umaPrevReplacedSlot;
        }

        private UMAData _umaData;
        protected UMAData umaData
        {
            get
            {
                if (_umaData == null)
                {
                    _umaData = PlayerManager.instance.currentPlayer.GetComponent<UMAData>();
                }

                return _umaData;
            }
        }

        
        public UMAEquipSlotData[] equipSlotsData = new UMAEquipSlotData[0];


        private OverlayLibraryBase _overlayLibrary;
        protected OverlayLibraryBase overlayLibrary
        {
            get
            {
                if (_overlayLibrary == null)
                    _overlayLibrary = PlayerManager.instance.currentPlayer.GetComponent<UMADynamicAvatar>().context.overlayLibrary;

                return _overlayLibrary;
            }
        }

        private SlotLibraryBase _slotLibrary;
        protected SlotLibraryBase slotLibrary
        {
            get
            {
                if (_slotLibrary == null)
                    _slotLibrary = PlayerManager.instance.currentPlayer.GetComponent<UMADynamicAvatar>().context.slotLibrary;

                return _slotLibrary;
            }
        }

        public override void NotifyItemEquipped(EquippableSlot equipSlot, uint amountEquipped)
        {
            base.NotifyItemEquipped(equipSlot, amountEquipped);

            foreach (var umaEquipSlot in equipSlotsData)
            {
                SlotData slot = GetUMASlot(umaEquipSlot.umaEquipSlot.slotName);
                if (slot == null && umaEquipSlot.umaSlotDataAsset == null)
                {
                    Debug.LogWarning("No suitable UMA slot found for " + umaEquipSlot.umaEquipSlot.slotName);
                    return; // No visual eqipment
                }

                if (umaEquipSlot.umaSlotDataAsset != null)
                {
                    slot = UMAReplaceSlot(umaEquipSlot);
                    UMAMarkAllDirty();
                }

                if (slot != null)
                {
                    UMAAddOverlay(umaEquipSlot, slot, umaEquipSlot.umaOverlayDataAsset.overlayName);
                }
            }

            umaData.isTextureDirty = true;
            umaData.isAtlasDirty = true;
            umaData.Dirty();
        }

        public override void NotifyItemUnEquipped(ICharacterCollection equipTo, uint amountUnEquipped)
        {
            base.NotifyItemUnEquipped(equipTo, amountUnEquipped);

            foreach (var umaEquipSlot in equipSlotsData)
            {
                var slot = GetUMASlot(umaEquipSlot.umaEquipSlot.slotName);
                if (slot == null && umaEquipSlot.umaSlotDataAsset == null)
                {
                    Debug.LogWarning("Couldn't visually equip UMA item, no slot found with name " + umaEquipSlot.umaEquipSlot.slotName, transform);
                    return; // No visual eqipment
                }

                if (umaEquipSlot.umaPrevReplacedSlot != null)
                {
                    UMARestoreReplacedSlot(umaEquipSlot);
                    UMAMarkAllDirty();
                }
                else
                {
                    if (slot != null)
                    {
                        UMARemoveOverlay(slot, umaEquipSlot.umaOverlayDataAsset.overlayName);
                    }
                }
            }


            umaData.isTextureDirty = true;
            umaData.isAtlasDirty = true;
            umaData.Dirty();
        }

        private void UMAAddOverlay(UMAEquipSlotData equipSlotData, SlotData slot, string overlayName)
        {
            slot.AddOverlay(overlayLibrary.InstantiateOverlay(overlayName));

            if (equipSlotData.umaOverrideColor)
                slot.SetOverlayColor(equipSlotData.umaOverlayColor, overlayName);
        }

        private void UMARemoveOverlay(SlotData slot, string overlayName)
        {
            slot.RemoveOverlay(overlayName);
        }

        private SlotData UMAReplaceSlot(UMAEquipSlotData equipSlotData)
        {
            var l = new List<SlotData>(umaData.umaRecipe.slotDataList);

            // Remove the object we're replacing
            if (equipSlotData.umaReplaceSlot != null)
            {
                equipSlotData.umaReplacedSlot = GetUMASlot(equipSlotData.umaReplaceSlot.slotName);
                l.RemoveAll(o => o != null && o.asset.slotName == equipSlotData.umaReplaceSlot.slotName);
            }

            // Add new slot
            var slot = new SlotData(equipSlotData.umaSlotDataAsset);
            l.Add(slot);
            equipSlotData.umaPrevReplacedSlot = slot;

            umaData.umaRecipe.slotDataList = l.ToArray();

            return slot;
        }

        private void UMAMarkAllDirty()
        {
            umaData.isTextureDirty = true;
            umaData.isAtlasDirty = true;
            umaData.isMeshDirty = true;
            umaData.isShapeDirty = true;
        }

        private void UMARestoreReplacedSlot(UMAEquipSlotData equipSlotData)
        {
            var l = new List<SlotData>(umaData.umaRecipe.slotDataList);
            l.Add(equipSlotData.umaReplacedSlot);
            l.Remove(equipSlotData.umaPrevReplacedSlot);
            umaData.umaRecipe.slotDataList = l.ToArray();
        }

        protected virtual SlotData GetUMASlot(string slotName)
        {
            return umaData.umaRecipe.slotDataList.FirstOrDefault(o => o != null && o.asset.slotName == slotName);
        }
    }
}

#endif