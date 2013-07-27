using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetB4
{
    public class SpawnZone
    {

	    public enum spawnType
	    {
		    random=1, periodic=2, atTime=3,
	    }

        public bool enabled = true;

	    public string mobsTeam="";
	    public string[] monsters;
	    public float zone=30;
	    public float maxAmountSimulaneously=10;

        public Vector3 position;
        public Vector3 destination = new Vector3(0, 0, 0);
	
	    public spawnType type=spawnType.random; //random or waves
	    public float waveSpawnHourOrPeriod=0;
	    // Use this for initialization
	
	    public bool noReward=false;
        public string specificName="";
	
	    float respawnPeriodCounter=0;
	
	    public int totalAmount = 0;

        public AgressivityLevel agressivityLevel = AgressivityLevel.neutral;

        public GameCode mainInstance;

        public SpawnZone(GameCode _mainInstance, Vector3 _position, string[] _monsters, float _zone)
        {
            mainInstance = _mainInstance;
            position = _position;
            monsters = _monsters;
            zone = _zone;

            firstLaunch = (int)maxAmountSimulaneously;
        }

        public Random mainSeed = new Random();

        private int firstLaunch = 0;
	    public void run () {
		
		    if(!enabled)
			    return;

		    if(type==spawnType.random)
		    {
			    if(respawnPeriodCounter<=0 || firstLaunch >0)
			    {
                    if (firstLaunch > 0)
                        firstLaunch--;

				    respawnPeriodCounter = 60;
				
				    if(totalAmount<maxAmountSimulaneously)
				    {
                        int typeSeed = (int)mainSeed.Next(0, monsters.Length - 1);
					    if(typeSeed>monsters.Length)
						    typeSeed = monsters.Length;
					
					    Vector3 tmpPos = position.getNewInstance();
                        tmpPos.x += (float)mainSeed.NextDouble()*zone - (float)mainSeed.NextDouble()*zone;
                        tmpPos.z += (float)mainSeed.NextDouble()*zone - (float)mainSeed.NextDouble()*zone;
					    
                        string _specificName;

                        if (specificName.Length > 0)
                            _specificName = specificName;
                        else
                            _specificName = monsters[typeSeed];

                        Entity tmpCont = new Entity(mainInstance, "", _specificName, new UnitsInfos().getEntityInfosByName(monsters[typeSeed]), tmpPos);

                        tmpCont.spawnZone = this;
                        tmpCont.isTemp = true;
                        tmpCont.enableRewards = !noReward;

                        tmpCont.agressivity = agressivityLevel;
                        tmpCont.team = mobsTeam;

                        mainInstance.addUnit(tmpCont);

					    if(!destination.isZero())
					    {
						    tmpCont.destination = destination;
					    }
					    else
					    {
						    tmpCont.wanderAround.x = zone;
						    tmpCont.wanderAround.z = zone;
					    }
					
					    totalAmount ++;
				    }
			    }
			
			    if(respawnPeriodCounter>0)
				    respawnPeriodCounter-=1;
		    }
		
		    if(type==spawnType.periodic)
		    {
			    if(respawnPeriodCounter<=0)
			    {
				    respawnPeriodCounter = waveSpawnHourOrPeriod;
				
				    if(totalAmount<=0)
				    {
					    for(int i=0; i<maxAmountSimulaneously; i++)
					    {
                            int typeSeed = (int)mainSeed.Next(0, monsters.Length - 1);
                            if (typeSeed > monsters.Length)
                                typeSeed = monsters.Length;

                            Vector3 tmpPos = position;
                            tmpPos.x += (float)mainSeed.NextDouble() * zone - (float)mainSeed.NextDouble() * zone;
                            tmpPos.z += (float)mainSeed.NextDouble() * zone - (float)mainSeed.NextDouble() * zone;

                            string _specificName;

                            if (specificName.Length > 0)
                                _specificName = specificName;
                            else
                                _specificName = monsters[typeSeed];

                            Entity tmpCont = new Entity(mainInstance, "", _specificName, new UnitsInfos().getEntityInfosByName(monsters[typeSeed]), tmpPos);
                            tmpCont.spawnZone = this;
                            tmpCont.isTemp = true;
                            tmpCont.enableRewards = !noReward;

                            tmpCont.agressivity = agressivityLevel;
                            tmpCont.team = mobsTeam;

                            mainInstance.addUnit(tmpCont);

                            if (!destination.isZero())
                            {
                                tmpCont.destination = destination;
                            }
                            else
                            {
                                tmpCont.wanderAround.x = zone;
                                tmpCont.wanderAround.z = zone;
                            }
						
						    totalAmount ++;
					    }		
				    }
			    }
			
			    if(respawnPeriodCounter>0)
				    respawnPeriodCounter-=1;
		    }
		
		    if(type==spawnType.atTime)
		    {
			   
		    }
	    }

    }
}
