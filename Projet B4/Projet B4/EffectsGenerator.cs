using System;
using System.Collections;
using System.Collections.Generic;

public class EffectsGenerator {
    public Dictionary<String, float> effects = new Dictionary<String, float>();
	public Dictionary<String, float> useEffects = new Dictionary<String, float>();
	public Dictionary<String, float> selfEffects = new Dictionary<String, float>();
	public Dictionary<String, float> npcEffects = new Dictionary<String, float>();
	
	public EffectsGenerator()
	{
		
		effects.Add("emptyEnchantable", 1); //enchant strorage, can be filled with an enchantement.
		
		effects.Add("souBon", 200); //1sou = 0.22hpRegenBon+0.28mpRegenBon
		effects.Add("staBon", 200); //1sta = 11hp+0.11hpRegenBon
		effects.Add("intBon", 200); //1int = 14mp+0.14mpRegenBon
		effects.Add("strBon", 200); //1str = 6.66dmg
		effects.Add("agiBon", 200); //1agi = 0.4attackSpeedBon+3.33armor
		
		effects.Add("hpBon", 10);
		effects.Add("mpBon", 6);
		effects.Add("hpRegenBon", 100);
		effects.Add("mpRegenBon", 60);
		effects.Add("armorBon", 30);
		effects.Add("resBon", 45);
		effects.Add("dmg", 30);
		effects.Add("crit",800);
		
		effects.Add("spellcrit",1000);
		
		effects.Add("arcaneRes", 20);
		effects.Add("fireRes", 20);
		effects.Add("iceRes", 20);
		effects.Add("natureRes", 20);
		effects.Add("shadowRes", 20);
		
		npcEffects = new Dictionary<String, float>(effects);
		
		effects.Add("spellcritBon",150);
		effects.Add("critBon",100);
		
		effects.Add("spell_dmg", 20);
		
		effects.Add("arcaneBon", 15);
		effects.Add("fireBon", 15);
		effects.Add("iceBon", 15);
		effects.Add("natureBon", 15);
		effects.Add("shadowBon", 15);
		
		effects.Add("dmg_livings", 20);
		effects.Add("dmg_undeads", 10);
		effects.Add("dmg_monsters", 15);
		effects.Add("dmg_humanoids", 25);
		effects.Add("dmg_humans", 20);
		effects.Add("dmg_spirits", 10);
		effects.Add("dmg_ogres", 10);
		effects.Add("dmg_dragons", 10);
		effects.Add("attackSpeedBon", 250);
		

		
		effects.Add("chaosBon", 30); //chaos damages cannot be absorbed by any resistance
		
		selfEffects = new Dictionary<String, float>(effects);
		
		//onHit
		effects.Add("drainHp", 300);
		effects.Add("drainMp", 300);
		effects.Add("ignoreArmor", 60);
		effects.Add("ignoreRes", 45);
		effects.Add("slow", 350);
		effects.Add("stun1", 600);
		effects.Add("stun2", 1200);
		effects.Add("poison1", 100);
		effects.Add("poison2", 200);
		effects.Add("poison3", 300);
		
		//use Only
        useEffects = new Dictionary<String, float>(effects);
		useEffects.Add("Teleport", 500); //how many times you can use teleport
		useEffects.Add("Mark", 250);
		useEffects.Add("Invoke", 500);
		useEffects.Add("HealHp", 8);
		useEffects.Add("HealMp", 6);
		useEffects.Add("Restore", 20);
		useEffects.Add("CastSpell", 350); //spell level
	}
	
	public Hashtable generateRandomEffect(float value)
	{
        Hashtable myEffect = new Hashtable();
        List<String> keys = new List<String>(effects.Keys);
        String myEffectName = keys[(new Random()).Next(0, keys.Count-1)];
           
		//System.out.println("effect: "+myEffectName);
		
		int EffectPrice = (int) effects[myEffectName];
		
		float amount = (float)(value/EffectPrice);
		amount = (float) (Math.Floor(amount*100)/100);
		myEffect.Add("effect", myEffectName);
		myEffect.Add("amount", amount);
		
		return myEffect;
	}
	
	public Hashtable generateArmorEffect(float value)
	{
		Hashtable myEffect = new Hashtable();
		
		int EffectPrice = (int)effects["armorBon"];
		
		float amount = (float)(value/EffectPrice);
		amount = (float) (Math.Floor(amount*100)/100);
		myEffect.Add("effect", "armorBon");
		myEffect.Add("amount", amount);
		
		return myEffect;
	}
	
	public Hashtable generateWeaponEffect(float value)
	{
		Hashtable myEffect = new Hashtable();
		
		int EffectPrice = (int)effects["dmg"];
		
		float amount = (float)(value/EffectPrice);
		amount = (float) (Math.Floor(amount*100)/100);
		
		myEffect.Add("effect", "dmg");
		myEffect.Add("amount", amount);
		
		return myEffect;
	}
	
	public Hashtable generateOnUseEffect(float value)
	{
		Hashtable myEffect = new Hashtable();
		
        List<String> keys = new List<String>(useEffects.Keys);

		String myEffectName = keys[(new Random()).Next(0, keys.Count-1)];
		
		int EffectPrice = (int)useEffects[myEffectName];
		
		float amount = (float)(value/EffectPrice);
		amount = (float) (Math.Floor(amount*100)/100);
		
		myEffect.Add("effect", myEffectName);
		myEffect.Add("amount", amount);
		
		return myEffect;
	}
	
	public Hashtable generateSelfEffects(float value)
	{
		Hashtable myEffect = new Hashtable();
		
        List<String> keys = new List<String>(selfEffects.Keys);

        String myEffectName = keys[(new Random()).Next(0, keys.Count-1)];
		
		int EffectPrice = (int)useEffects[myEffectName];
		
		float amount = (float)(value/EffectPrice);
		amount = (float) (Math.Floor(amount*100)/100);
		
		myEffect.Add("effect", myEffectName);
		myEffect.Add("amount", amount);
		
		return myEffect;
	}
	
	/*public ISFSObject generateNpcEffects(float value)
	{
		ISFSObject myEffect = new SFSObject();
		
		List<Object> valuesList = new ArrayList<Object>(npcEffects.keySet());
		int randomIndex = new Random().nextInt(Math.abs(valuesList.size()));
		String myEffectName = (String) valuesList.get(randomIndex);
		
		int EffectPrice = (Integer)npcEffects.get(myEffectName);
		
		float amount = (Float)(value/EffectPrice);
		amount = (float) (Math.floor(amount*100)/100);
		
		myEffect.AddUtfString("effect", myEffectName);
		myEffect.AddFloat("amount", amount);
		
		return myEffect;
	}*/
}
