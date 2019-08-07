#if DIALOGUE_SYSTEM

using System;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Assertions;

namespace Devdog.InventoryPro.Integration.DialogueSystem
{
    [AddComponentMenu(InventoryPro.AddComponentMenuPath + "Integration/DialogueSystem/Dialogue System Collection Saver Loader")]
    public class DialogueSystemCollectionSaverLoader : CollectionSaverLoaderBase
    {

		public string actorName = string.Empty;
		public string fieldName = string.Empty;

		protected override void Awake()
		{
			base.Awake();
			if (string.IsNullOrEmpty(actorName)) 
			{
				if (Debug.isDebugBuild) Debug.LogWarning("Dialogue System: You must set the Actor Name for the SaverLoader on " + name, this);
				enabled = false;
			} 
			if (string.IsNullOrEmpty(fieldName)) fieldName = name;
		}

		public override void SaveItems(object serializedData, Action<bool> callback)
		{
			Assert.IsNotNull(callback, "Callback has to be set ( null given )");
			Assert.IsTrue(serializedData is string, "Serialized data is not string, json collection serializer can only use a JSON string.");
			
			DialogueLua.SetActorField(actorName, "InventoryPro_" + fieldName, (string)serializedData);
			callback(true);
		}
		
		public override void LoadItems(Action<object> callback)
		{
			Assert.IsNotNull(callback, "Callback has to be set ( null given )");
			
			var stringData = DialogueLua.GetActorField(actorName, "InventoryPro_" + fieldName).AsString;
			if (string.IsNullOrEmpty(stringData)) return;
			callback(stringData);
		}


		public void OnRecordPersistentData()
		{
			if (enabled)
			{
				if (Debug.isDebugBuild) Debug.Log("Inventory Pro - Recording inventory for " + actorName + " into Dialogue System's Save System", this);
				Save();
			}
		}

		public void OnApplyPersistentData()
		{
			if (enabled)
			{
				if (Debug.isDebugBuild) Debug.Log("Inventory Pro - Retrieving inventory for " + actorName + " from Dialogue System's Save System", this);
				Load();
			}
		}

	}
}

#endif