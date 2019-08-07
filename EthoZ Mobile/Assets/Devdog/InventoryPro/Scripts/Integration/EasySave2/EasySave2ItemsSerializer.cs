namespace Devdog.InventoryPro
{
    public class EasySave2ItemsSerializer : IItemsSerializer
    {
        public object SerializeCollection(ItemCollectionBase collection)
        {
            // Easy save 2 handles all this for us..
            return new ItemCollectionSerializationModel(collection);
        }

        public object SerializeContainer(IInventoryItemContainer container)
        {
            // Easy save 2 handles all this for us..
            return new ItemContainerSerializationModel(container);
        }

        public ItemCollectionSerializationModel DeserializeCollection(object serializedData)
        {
            // Easy save 2 handles all this for us..
            return (ItemCollectionSerializationModel) serializedData;
        }

        public ItemContainerSerializationModel DeserializeContainer(object serializedData)
        {
            // Easy save 2 handles all this for us..
            return (ItemContainerSerializationModel) serializedData;
        }
    }
}
