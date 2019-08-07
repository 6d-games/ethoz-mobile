#if EASY_SAVE_2

public class ES2UserType_DevdogInventoryProStatDecoratorSerializationModel : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		Devdog.InventoryPro.StatDecoratorSerializationModel data = (Devdog.InventoryPro.StatDecoratorSerializationModel)obj;
		// Add your writer.Write calls here.
		writer.Write(data.statID);
		writer.Write(data.value);
		writer.Write(data.actionEffect);
		writer.Write(data.isFactor);

	}
	
	public override object Read(ES2Reader reader)
	{
		Devdog.InventoryPro.StatDecoratorSerializationModel data = new Devdog.InventoryPro.StatDecoratorSerializationModel();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		Devdog.InventoryPro.StatDecoratorSerializationModel data = (Devdog.InventoryPro.StatDecoratorSerializationModel)c;
		// Add your reader.Read calls here to read the data into the object.
		data.statID = reader.Read<System.Int32>();
		data.value = reader.Read<System.String>();
		data.actionEffect = reader.Read<Devdog.InventoryPro.StatDecorator.ActionEffect>();
		data.isFactor = reader.Read<System.Boolean>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_DevdogInventoryProStatDecoratorSerializationModel():base(typeof(Devdog.InventoryPro.StatDecoratorSerializationModel)){}
}

#endif