#if EASY_SAVE_2

public class ES2UserType_DevdogInventoryProCurrencyDecoratorSerializationModel : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		Devdog.InventoryPro.CurrencyDecoratorSerializationModel data = (Devdog.InventoryPro.CurrencyDecoratorSerializationModel)obj;
		// Add your writer.Write calls here.
		writer.Write(data.currencyID);
		writer.Write(data.amount);

	}
	
	public override object Read(ES2Reader reader)
	{
		Devdog.InventoryPro.CurrencyDecoratorSerializationModel data = new Devdog.InventoryPro.CurrencyDecoratorSerializationModel();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		Devdog.InventoryPro.CurrencyDecoratorSerializationModel data = (Devdog.InventoryPro.CurrencyDecoratorSerializationModel)c;
		// Add your reader.Read calls here to read the data into the object.
		data.currencyID = reader.Read<System.UInt32>();
		data.amount = reader.Read<System.Single>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_DevdogInventoryProCurrencyDecoratorSerializationModel():base(typeof(Devdog.InventoryPro.CurrencyDecoratorSerializationModel)){}
}

#endif