#if EASY_SAVE_2

public class ES2UserType_DevdogInventoryProItemCollectionSerializationModel : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		Devdog.InventoryPro.ItemCollectionSerializationModel data = (Devdog.InventoryPro.ItemCollectionSerializationModel)obj;
		// Add your writer.Write calls here.
		writer.Write(data.items);
		writer.Write(data.currencies);

	}
	
	public override object Read(ES2Reader reader)
	{
		Devdog.InventoryPro.ItemCollectionSerializationModel data = new Devdog.InventoryPro.ItemCollectionSerializationModel();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		Devdog.InventoryPro.ItemCollectionSerializationModel data = (Devdog.InventoryPro.ItemCollectionSerializationModel)c;
		// Add your reader.Read calls here to read the data into the object.
		data.items = reader.ReadArray<Devdog.InventoryPro.InventoryItemSerializationModel>();
		data.currencies = reader.ReadArray<Devdog.InventoryPro.CurrencyDecoratorSerializationModel>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_DevdogInventoryProItemCollectionSerializationModel():base(typeof(Devdog.InventoryPro.ItemCollectionSerializationModel)){}
}

#endif