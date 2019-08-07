using System;
using System.Collections;
using Devdog.General;
using UnityEngine;

namespace Devdog.InventoryPro
{
    public abstract class CollectionSaverLoaderBase : SaverLoaderBase
    {
        [SerializeField]
        private ItemCollectionBase _collection;
        protected ItemCollectionBase collection
        {
            get 
            {
                if (_collection == null)
                {
                    _collection = GetComponent<ItemCollectionBase>();
                }

                return _collection;
            }
        }

        public override string saveName
        {
            get
            {
                return SaveNamePrefix + "Collection_" + collection.collectionName.ToLower().Replace(" ", "_");
            }
        }

        public override void Save()
        {
            try
            {
                var serializedCollection = serializer.SerializeCollection(collection);
                SaveItems(serializedCollection, (bool saved) =>
                {
                    DevdogLogger.LogVerbose("Saved collection " + collection.collectionName);
                });
            }
            catch (SerializedObjectNotFoundException e)
            {
                Debug.LogWarning(e.Message + e.StackTrace);
            }
        }

        public override void Load()
        {
            try
            {
                LoadItems((object data) =>
                {
                    DevdogLogger.LogVerbose("Loaded collection " + collection.collectionName);

                    var model = serializer.DeserializeCollection(data);
                    model.ToCollection(collection);
                });
            }
            catch (SerializedObjectNotFoundException e)
            {
                Debug.LogWarning(e.Message + e.StackTrace);
            }
        }
    }
}
