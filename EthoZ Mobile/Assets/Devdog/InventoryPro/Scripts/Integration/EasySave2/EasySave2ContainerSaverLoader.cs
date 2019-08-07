#if EASY_SAVE_2

using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Devdog.InventoryPro.Integration.EasySave2
{
    [AddComponentMenu(InventoryPro.AddComponentMenuPath + "Integration/EasySave2/EasySave2 container saver loader")]
    public class EasySave2ContainerSaverLoader : ContainerSaverLoaderBase
    {
        public string fileName = "myFile.txt";

        public override string saveName
        {
            get { return fileName + container.uniqueName; }
        }

        protected override void SetSerializer()
        {
            serializer = new EasySave2ItemsSerializer();
        }


        public override void SaveItems(object serializedData, Action<bool> callback)
        {
            Assert.IsTrue(serializedData is ItemContainerSerializationModel);

            using (ES2Writer writer = ES2Writer.Create(fileName, new ES2Settings() { fileMode = ES2Settings.ES2FileMode.Create }))
            {
                writer.Write(serializedData, saveName);
                writer.Save(true);
            }

            callback(true); // Saved
        }

        public override void LoadItems(Action<object> callback)
        {
            if (ES2.Exists(fileName) == false)
            {
                Debug.Log("Can't load from file " + fileName + " file does not exist.", gameObject);
                return;
            }
            
            using (ES2Reader reader = ES2Reader.Create(fileName))
            {
                callback(reader.Read<ItemContainerSerializationModel>(saveName));
            }
        }
    }
}

#endif