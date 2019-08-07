#if EASY_SAVE_2

public class ES2UserType_DevdogInventoryProItemContainerSerializationModel : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		Devdog.InventoryPro.ItemContainerSerializationModel data = (Devdog.InventoryPro.ItemContainerSerializationModel)obj;
		// Add your writer.Write calls here.
		writer.Write(data.items);

	}
	
	public override object Read(ES2Reader reader)
	{
		Devdog.InventoryPro.ItemContainerSerializationModel data = new Devdog.InventoryPro.ItemContainerSerializationModel();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		Devdog.InventoryPro.ItemContainerSerializationModel data = (Devdog.InventoryPro.ItemContainerSerializationModel)c;
		// Add your reader.Read calls here to read the data into the object.
		data.items = reader.ReadArray<Devdog.InventoryPro.InventoryItemSerializationModel>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_DevdogInventoryProItemContainerSerializationModel():base(typeof(Devdog.InventoryPro.ItemContainerSerializationModel)){}
}

#endif