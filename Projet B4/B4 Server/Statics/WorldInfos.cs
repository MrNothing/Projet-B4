/*
	Generated with B4 Built-in Editor
		999* Npcs
		999* Items
		999* Spells
		999* Quests
*/

public class WorldInfos
{
	public HashTable Npcs = new HashTable();
	public HashTable Items = new HashTable();
	public HashTable Spells = new HashTable();
	public HashTable Quests = new HashTable();
	
	public WorldInfos()
	{
		//Item: Healing potion, id: 0
		Item tmpItem_0 = new Item("Healing potion");
		tmpItem_0.description = "Soigne 10 points de vie";
		tmpItem_0.minLevel = 1;
		tmpItem_0.price = 10;
		Spell tmpSpell_0_count_0 = new tmpSpell_0_count_0("heal");
		tmpSpell_0_count_0.effects.push(new Effect("heal"), 10);
		tmpItem_0.onUse.push(tmpSpell_0_count_0);
		tmpItem_0.passiveEffects.push(new Effect("hp"));
	}
	
	public Item getItemByName(String itemName) //generates an item using its pattern. that item has no id until it is specified.
	{
		Item newItem = new Item(Items[itemName]);
		newItem.uses = newItem.infos.charges;
		return newItem;
	}
	
	public EntityInfos getEntityInfosByName(String name)
	{
		return Npcs[name];
	}
}