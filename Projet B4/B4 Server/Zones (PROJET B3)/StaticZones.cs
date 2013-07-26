using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetB3.Zones
{
    public class Zone1:ZonePattern
    {
        //freely fill data now...
        public Zone1()
        {
            UnitsInfos tmpInfos = new UnitsInfos();

            zoneName = "Map0";
            Entities.Add("#testUnit1", new Entity(null, "#testUnit1", "Bob De La Lune", tmpInfos.getEntityInfosByName("BobDeLaLune"), new Vector3(-0.4155649f, -33.95914f, 66.99482f)));
            Entities.Add("#testUnitTK", new Entity(null, "#testUnitTK", "TikTik", tmpInfos.getEntityInfosByName("TikTik"), new Vector3(163.9227f, -146.1143f, 324.0215f)));
            Entities.Add("#theWoodenG", new Entity(null, "#theWoodenG", "Wooden Golem", tmpInfos.getEntityInfosByName("Wooden Golem"), new Vector3(31.28071f, -148.0476f, 14.4193f)));

            Entities.Add("#testUnit2", new Entity(null, "#testUnit2", "Lever", tmpInfos.getEntityInfosByName("Lever"), new Vector3(-15.16774f, -34.31057f, 34.15692f)));
            Entities["#testUnit2"].type = EntityType.trigger;
            Entities["#testUnit2"].myTrigger = new Triggers(Entities["#testUnit2"]);
            Entities["#testUnit2"].myTrigger.triggersToEnable = new Triggers[1];
           
            Entities.Add("#testUnit3", new Entity(null, "#testUnit3", "Button", tmpInfos.getEntityInfosByName("Button"), new Vector3(-12.66341f, -34.31057f, 35.94048f)));
            Entities["#testUnit3"].type = EntityType.trigger;
            Entities["#testUnit3"].myTrigger = new Triggers(Entities["#testUnit3"]);

            Entities.Add("#testUnit4", new Entity(null, "#testUnit4", "MillHelice", tmpInfos.getEntityInfosByName("MillHelice"), new Vector3(-67.1724f, -25.95159f, 14.11105f)));
            Entities["#testUnit4"].type = EntityType.trigger;
            Entities["#testUnit4"].myTrigger = new Triggers(Entities["#testUnit4"]);
            Entities["#testUnit4"].checkRange = 0;
            Entities["#testUnit2"].myTrigger.triggersToEnable[0] = Entities["#testUnit4"].myTrigger;
            
            string[] mobsGrp1 = new string[1];
            mobsGrp1[0] = "Blob";
            spawnZones.Add("blobs1", new SpawnZone(null, new Vector3(91.94307f, 2.874027f, 148.06f), mobsGrp1, 20));

            string[] mobsGrp2 = new string[1];
            mobsGrp2[0] = "Ent";
            spawnZones.Add("ents1", new SpawnZone(null, new Vector3(107.2685f, 4.934978f - 150f, 221.5885f), mobsGrp2, 100));
            spawnZones["ents1"].mobsTeam = "ents";
            spawnZones["ents1"].agressivityLevel = AgressivityLevel.agressive;

            UnitsInfos tmpInfosa1 = new UnitsInfos();
            Entities.Add("#ent1", new Entity(null, "#ent1", "Ent", tmpInfosa1.getEntityInfosByName("Ent"), new Vector3(59.4497f + 300, -0.350647f, 40.832f)));
            Entities["#ent1"].wanderAround = new Vector3(5, 0, 5);
           
            //add the train and some guards...
            Entity train = new Entity(null, "#theTrain1", "Train", new EntityInfos("Train1"), new Vector3(-16.27856f - 700, -4.932142f - 143, -13.50952f + 300));
            Entities.Add(train.id, train);
            Entities[train.id].type = EntityType.trigger;
            Entities[train.id].myTrigger = new Triggers(train);
            Entities[train.id].myTrigger.autoTrigger = 1000;
            Entities[train.id].checkRange = 0;
        }
    }

    public class Zone2 : ZonePattern
    {
        //freely fill data now...
        public Zone2()
        {
            zoneName = "Map1";
            UnitsInfos tmpInfos = new UnitsInfos();
            Entities.Add("#ent1", new Entity(null, "#testUnit1", "Ent", tmpInfos.getEntityInfosByName("Ent"), new Vector3(59.4497f, -0.350647f, 40.832f)));
            Entities["#ent1"].wanderAround = new Vector3(5,0,5);

            //add the train and some guards...
            Entity train = new Entity(null, "#theTrain1", "Train", new EntityInfos("Train1"), new Vector3(-16.27856f, -4.932142f, -13.50952f));
            Entities.Add(train.id, train);
            Entities[train.id].type = EntityType.trigger;
            Entities[train.id].myTrigger = new Triggers(train);
            Entities[train.id].myTrigger.autoTrigger = 200;
        }
    }

    public class Zone3 : ZonePattern
    {
        //freely fill data now...
        public Zone3()
        {
            zoneName = "Map2";
            UnitsInfos tmpInfos = new UnitsInfos();

            //add vendor npcs and guards...
            
        }
    }

    public class Zone4 : ZonePattern
    {
        //freely fill data now...
        public Zone4()
        {
            zoneName = "Map3";
            UnitsInfos tmpInfos = new UnitsInfos();

           
            //add small Ents and the elder ent
            //add the event including the elder ent
        }
    }
}