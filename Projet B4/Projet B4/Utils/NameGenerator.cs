using System;
using System.Collections;
using System.Collections.Generic;


public class NameGenerator {

    public Dictionary<String, String> owners = new Dictionary<String, String>();
    public Dictionary<String, String> status = new Dictionary<String, String>();
    public Dictionary<String, String> prefix = new Dictionary<String, String>();
    public Dictionary<String, String> badPrefixes = new Dictionary<String, String>();
    public Dictionary<int, String> leveledPrefixes = new Dictionary<int, String>();
    public Dictionary<int, String> leveledMobPrefixes = new Dictionary<int, String>();
	
	public String generateLeveledName(String type, int rarity)
	{
		return leveledPrefixes[rarity]+" "+type;
	}

	public String generateItemName(String type, int rarity)
	{
		String finalName="";
		
		if(rarity>=10)
		{
            Hashtable myEffect = new Hashtable();
            List<String> keys = new List<String>(owners.Keys);
            String myOwnerName = keys[(new Random()).Next(0, keys.Count - 1)];
			finalName+=myOwnerName+"'s ";
		}
		
		if(rarity>2)
		{
            Hashtable myEffect = new Hashtable();
            List<String> keys = new List<String>(prefix.Keys);
            String myPrefixName = keys[(new Random()).Next(0, keys.Count - 1)];
			
			finalName+=myPrefixName+" ";
		}
		else
		{
            Hashtable myEffect = new Hashtable();
            List<String> keys = new List<String>(badPrefixes.Keys);
            String myPrefixName = keys[(new Random()).Next(0, keys.Count - 1)];
			
			finalName+=myPrefixName+" ";
		}
		
		finalName+=type;
		
		if((rarity>5 && rarity<10) || rarity>=13)
		{
            Hashtable myEffect = new Hashtable();
            List<String> keys = new List<String>(status.Keys);
            String myOwnerName = keys[(new Random()).Next(0, keys.Count - 1)];
			finalName+=" "+myOwnerName;
		}
		
		return finalName;
	}
	public NameGenerator()
	{
		owners.Add("Boris", "Boris");
		owners.Add("Agam", "Agam");
		owners.Add("Ijoyen", "Ijoyen");
		owners.Add("Agmon", "Agmon");
		owners.Add("Amber", "Amber");
		owners.Add("Goku", "Goku");
		owners.Add("Droum", "Droum");
		owners.Add("Kanapesh", "Kanapesh");
		owners.Add("Shin", "Shin");
		owners.Add("Akalunia", "Akalunia");
		owners.Add("Kanlug", "Kanlug");
		owners.Add("Zug Zug", "Zug Zug");
		owners.Add("Zan", "Zan");
		owners.Add("Lilly", "Lilly");
		owners.Add("Androk", "Androk");
		owners.Add("Manam", "Manam");
		owners.Add("Hillan", "Hillan");
		owners.Add("Athos", "Athos");
		owners.Add("Karam", "Karam");
		owners.Add("Gok", "Gok");
		owners.Add("King", "King");
		owners.Add("Prince", "Prince");
		owners.Add("Uther", "Uther");
		owners.Add("Arthur", "Arthur");
		owners.Add("The Magic Fish", "The Magic Fish");
		owners.Add("The Mad Scientist", "The Mad Scientist");
		owners.Add("Azer", "Azer");
		owners.Add("LoL Cat", "LoL Cat");
		owners.Add("Troll", "Troll");
		owners.Add("Ogre", "Ogre");
		owners.Add("Undead", "Undead");
		owners.Add("Bishop", "Bishop");
		owners.Add("Joker", "Joker");
		owners.Add("Mr. Nothing", "Mr. Nothing");
		owners.Add("Mr. Green", "Mr. Green");
		owners.Add("Mr. Beta", "Mr. Beta");
		owners.Add("Mr. Alpha", "Mr. Alpha");
        owners.Add("Rodik", "Rodik");
        owners.Add("Samy", "Samy");
        owners.Add("Piccolo", "Piccolo");
        owners.Add("Pedobear", "Pedobear");

		status.Add("of Darkness", "of Darkness");
		status.Add("of the Dammned", "of the Dammned");
		status.Add("of Light", "of Light");
		status.Add("of the Archmage", "of the Archmage");
		status.Add("of the Warlock", "of the Warlock");
		status.Add("of the Moon", "of the Moon");
		status.Add("of Hell", "of Hell");
		status.Add("of the Wise", "of the Wise");
		status.Add("of the Mad Sorcerer", "of the Mad Sorcerer");
		status.Add("of the Barbarian", "of the Barbarian");
		status.Add("of the King", "of the King");
		status.Add("of the Prince", "of the Prince");
		status.Add("of the Witch", "of the Witch");
		status.Add("of the Black Widow", "of the Black Widow");
		status.Add("of the Dead", "of the Dead");
		status.Add("of the Dragon", "of the Dragon");
		status.Add("of the Elite Knight", "of the Elite Knight");
		status.Add("of the Elite Warrior", "of the Elite Warrior");
		status.Add("of the Elite Wizard", "of the Elite Wizard");
		status.Add("of the Priest", "of the Priest");
		status.Add("of Knowledge", "of Knowledge");
		status.Add("of Legends", "of Legends");
		status.Add("of the Great World", "of the Great World");
		status.Add("of the Forest Spirit", "of the Forest Spirit");
		status.Add("of the Old Wizard", "of the Old Wizard");
		status.Add("of the Lonely Guy", "of the Lonely Guy");
		status.Add("of the Zombie Lord", "of the Zombie Lord");
		status.Add("of the Dark Lord", "of the Dark Lord");
		status.Add("of the Evil Lord", "of the Evil Lord");
		status.Add("of the Lord", "of the Lord");
		status.Add("of Evil", "of Evil");
		status.Add("of Love", "of Love");
		status.Add("of the Ogre", "of the Ogre");
		status.Add("of the Troll", "of the Troll");
		status.Add("of the Fancy Man", "of the Fancy Man");
		status.Add("of the Developper", "of the Developper");
		status.Add("of the Sea King", "of the Sea King");
		status.Add("of the Sea Lord", "of the Sea Lord");
		status.Add("of the Forgotten", "of the Forgotten");
		status.Add("of Despair", "of Despair");
		status.Add("of the Map Pope", "of the Map Pope");
		status.Add("of the Joking Knight", "of the Joking Knight");
		status.Add("of the Sad Frog", "of the Sad Frog");
		status.Add("of the Star", "of the Star");
        status.Add("of the sexy beast", "of the sexy beast");
        status.Add("of the cute little pony", "of the cute little pony");
        status.Add("of the bearded Viking", "");
        status.Add("of the decieving snake", "of the decieving snake");
        status.Add("of the fat man", "of the fat man");
        status.Add("of the arse-sexed sheep", "of the arse-sexed sheep");
        status.Add("of the little gal", "of the little gal");
		
		prefix.Add("White", "White");
		prefix.Add("Gray", "Gray");
		prefix.Add("Black", "Black");
		prefix.Add("Blue", "Blue");
		prefix.Add("Golden", "Golden");
		prefix.Add("Orange", "Orange");
		prefix.Add("Burning", "Burning");
		prefix.Add("Weired", "Weired");
		prefix.Add("Spicy", "Spicy");
		prefix.Add("Pink", "Pink");
		prefix.Add("Red", "Red");
		prefix.Add("Strong", "Strong");
		prefix.Add("Small", "Strong");
		prefix.Add("Mighty", "Mighty");
		prefix.Add("Legendary", "Legendary");
		
		prefix.Add("Epic", "Epic");
		prefix.Add("Shiny", "Shiny");
		prefix.Add("Wooden", "Wooden");
		prefix.Add("Mithril", "Mithril");
		prefix.Add("Green", "Green");
		prefix.Add("Huge", "Huge");
		prefix.Add("Superior", "Superior");
		prefix.Add("Gloomy", "Gloomy");
		prefix.Add("Amazing", "Amazing");
		prefix.Add("Lovely", "Lovely");
		prefix.Add("Beautiful", "Beautiful");
		prefix.Add("Dangerous", "Dangerous");
		prefix.Add("Stunning", "Stunning");
		prefix.Add("Dry", "Dry");
		prefix.Add("Wet", "Wet");
		prefix.Add("Funny", "Funny");
		prefix.Add("Pitful", "Pitful");
		prefix.Add("Raging", "Raging");
		prefix.Add("Frenzy", "Frenzy");
		prefix.Add("Astronomic", "Astronomic");
		prefix.Add("Simple", "Simple");
		prefix.Add("Ordinary", "Ordinary");
		prefix.Add("Living", "Living");
		prefix.Add("Suspicious", "Suspicious");
		prefix.Add("Fancy", "Fancy");
		prefix.Add("Unknown", "Unknown");
		
		prefix.Add("Crasy", "Crasy");
		prefix.Add("Rare", "Rare");
		prefix.Add("", "");
		
		badPrefixes.Add("Bad", "");
		badPrefixes.Add("Old", "");
		badPrefixes.Add("Rotten", "");
		badPrefixes.Add("Rotting", "");
		badPrefixes.Add("Broken", "");
		badPrefixes.Add("Fragile", "");
		badPrefixes.Add("Weak", "");
		badPrefixes.Add("Cheap", "");
		badPrefixes.Add("Useless", "");
		badPrefixes.Add("Failed", "Failed");
		badPrefixes.Add("Noob", "Noob");
		
		leveledPrefixes.Add(0,"Worthless");
		leveledPrefixes.Add(1,"Inferior");
		leveledPrefixes.Add(2,"Average");
		leveledPrefixes.Add(3,"");
		leveledPrefixes.Add(4,"Good");
		leveledPrefixes.Add(5,"Very Good");
		leveledPrefixes.Add(6,"Best");
		leveledPrefixes.Add(7,"Superior");
		leveledPrefixes.Add(8,"Extermely Superior");
		leveledPrefixes.Add(9,"Supreme");
		leveledPrefixes.Add(10,"Ultimate");
		leveledPrefixes.Add(11,"Legendary");
		leveledPrefixes.Add(12,"God like");
		leveledPrefixes.Add(13,"Divine");
		leveledPrefixes.Add(14,"Godly Divine");
		leveledPrefixes.Add(15,"Unique");
		
		leveledMobPrefixes.Add(0,"Worthless");
		leveledMobPrefixes.Add(1,"Weak");
		leveledMobPrefixes.Add(2,"Young");
		leveledMobPrefixes.Add(3,"Apprentice");
		leveledMobPrefixes.Add(4,"");
		leveledMobPrefixes.Add(5,"Strong");
		leveledMobPrefixes.Add(6,"Skilled");
		leveledMobPrefixes.Add(7,"Master");
		leveledMobPrefixes.Add(8,"General");
		leveledMobPrefixes.Add(9,"Elite");
		leveledMobPrefixes.Add(10,"Powerfull");
		leveledMobPrefixes.Add(11,"Legendary");
		leveledMobPrefixes.Add(12,"God like");
		leveledMobPrefixes.Add(13,"Divine");
		leveledMobPrefixes.Add(14,"Godly Divine");
		leveledMobPrefixes.Add(15,"King of");
		
	}
	
}
