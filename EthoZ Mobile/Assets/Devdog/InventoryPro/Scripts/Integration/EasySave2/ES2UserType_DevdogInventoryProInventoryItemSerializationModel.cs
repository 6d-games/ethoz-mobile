#if EASY_SAVE_2

public class ES2UserType_DevdogInventoryProInventoryItemSerializationModel : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		Devdog.InventoryPro.InventoryItemSerializationModel data = (Devdog.InventoryPro.InventoryItemSerializationModel)obj;
		// Add your writer.Write calls here.
		writer.Write(data.itemID);
		writer.Write(data.amount);
		writer.Write(data.stats);
		writer.Write(data.collectionName);

	}
	
	public override object Read(ES2Reader reader)
	{
		Devdog.InventoryPro.InventoryItemSerializationModel data = new Devdog.InventoryPro.InventoryItemSerializationModel();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		Devdog.InventoryPro.InventoryItemSerializationModel data = (Devdog.InventoryPro.InventoryItemSerializationModel)c;
		// Add your reader.Read calls here to read the data into the object.
		data.itemID = reader.Read<System.Int32>();
		data.amount = reader.Read<System.UInt32>();
		data.stats = reader.ReadArray<Devdog.InventoryPro.StatDecoratorSerializationModel>();
		data.collectionName = reader.Read<System.String>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_DevdogInventoryProInventoryItemSerializationModel():base(typeof(Devdog.InventoryPro.InventoryItemSerializationModel)){}
}

#endif