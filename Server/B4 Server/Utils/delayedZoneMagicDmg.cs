using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace ProjetB4
{

    public class delayedMoonlightArt
    {
        private Entity parentUnit;
        public float x;
        public float y;
        public float z;
        public int waves=0;
        public int period=0;
        public GameCode mainInstance;

        public delayedMoonlightArt(Entity _parentUnit, GameCode _mainInstance, int _waves)
        {
            parentUnit = _parentUnit;
            mainInstance = _mainInstance;
            waves = _waves;
        }

        public void run()
        {
            if (waves > 0)
            {
                //System.out.println("Wave "+waves+" fell");
                Entity randEnnemy = parentUnit.getRandomEnnemy();

                if (randEnnemy != null)
                {
                    if (randEnnemy.getDistance(parentUnit)<15)
                    {
                    parentUnit.x = randEnnemy.x;
                    parentUnit.y = randEnnemy.y;
                    parentUnit.z = randEnnemy.z;

                    randEnnemy.hitMeWithPhysic(parentUnit.id, parentUnit.getAttackValue(), parentUnit.lastCrit);

                    parentUnit.sendSpecialAttack(randEnnemy.id, (int)randEnnemy.x, (int)randEnnemy.y, (int)randEnnemy.z, "special_1");
                        }
                }

                waves--;

                if (period >= 25)
                    mainInstance.ScheduleCallback(run, period); ;
            }
            else
            {
                //myTimer.cancel();
            }
        }
    }

    public class delayedTeleport
    {
        private Entity parentUnit;
        public float x;
        public float y;
        public float z;
        public delayedTeleport(Entity _parentUnit)
        {
            parentUnit = _parentUnit;
        }

        public void run()
        {
            parentUnit.position.x = x;
            parentUnit.position.y = y;
            parentUnit.position.z = z;

            parentUnit.sendTeleport();
        }
    }
    public class delayedZoneMagicDmg
    {
        private Entity parentUnit;
            
        private String target;
        private float dmg;
        private String dmgType;

        float x;
        float y;
        float z;

        float zone;

        int waves;

        public bool centerOnHero = false;
            
        public String invokeOnKill = ""; //invoke the specific unit if the spell kills a unit.

        public float propelValue = 0; //propels the unit on the opposite side of the spell.

        public float decreaseWithDistance = 0;

        public float angleLimit = 0;
        private float casterAngle = 0;
        
        private GameCode mainInstance;

        public int period;

        public delayedZoneMagicDmg(Entity _parentUnit, GameCode _mainInstance, float _dmg, String _dmgType, float ix, float iy, float iz, float _zone, int _waves)
        {
            if (angleLimit > 0)
            {
                casterAngle = (float)Math.Atan2((ix - parentUnit.position.x), -(iz - parentUnit.position.z));
            }

            mainInstance = _mainInstance;
            waves = _waves;
            zone = _zone;
            dmg = _dmg;
            parentUnit = _parentUnit;
            dmgType = _dmgType;

            x = ix;
            y = iy;
            z = iz;
        }

        private float calculateDifferenceBetweenAngles(float firstAngle, float secondAngle)
        {
            float difference = secondAngle - firstAngle;
            while (difference < -180) difference += 360;
            while (difference > 180) difference -= 360;
            return difference;
        }
	

        public void run()
        {
            if (centerOnHero)
            {
                x = parentUnit.position.x;
                y = parentUnit.position.y;
                z = parentUnit.position.z;
            }

            if (waves > 0)
            {
                //System.out.println("Wave "+waves+" fell");

                CheckInZoneUnits(x, y, z);
                waves--;

                if (period>=25)
                mainInstance.ScheduleCallback(run, period); ;
            }
            else
            {
                //myTimer.cancel();
            }
        }

        void CheckInZoneUnits(float _x, float _y, float _z)
        {
           
            if(parentUnit.hp>0)
            {
                Dictionary<String, Entity> unitsList = parentUnit.myGame.units;
            
           //     System.out.println("Bloc Size: "+unitsList.size());   
            
                foreach (Object o in unitsList.Keys)
                {

                    Entity theUnit = parentUnit.myGame.units[o+""];

                    if(theUnit!=null)
                    {
                        bool inCone = true;
                       
                        if (angleLimit > 0)
                        {
                            inCone = false;

                            float tmpAngle = (float)Math.Atan2((theUnit.position.x - parentUnit.position.x), -(theUnit.position.z - parentUnit.position.z));

                            if (calculateDifferenceBetweenAngles(casterAngle, tmpAngle)< angleLimit)
                                inCone=true;
                        }
                        
                        //System.out.println("Unit found! id: "+theUnit.id);
                        float distance = (float)Math.Sqrt(Math.Abs(theUnit.position.x - x) * Math.Abs(theUnit.position.x - x) + Math.Abs(theUnit.position.y - y) * Math.Abs(theUnit.position.y - y) + Math.Abs(theUnit.position.z - z) * Math.Abs(theUnit.position.z - z));
                        if (distance<zone // + Math.Abs(theUnit.y-y)*Math.Abs(theUnit.y-y)
                           && /*(theUnit.team!=parentUnit.team) &&*/ inCone)
                        {
                            if (decreaseWithDistance <= 0)
                                theUnit.hitMeWithMagic(parentUnit.id, dmg, dmgType);
                            else
                            {
                                float tmpDmg = dmg * ((zone - distance) / zone);
                                theUnit.hitMeWithMagic(parentUnit.id, dmg, dmgType);
                            }
                           if(propelValue>0)
                           {
                    	       //theUnit.propelMe(parentUnit, propelValue);
                           }
                       
                           if(invokeOnKill.Length>0 && theUnit.hp<=0)
                           {
                    	       //try{
                    		      /* Unit newUnit = parentUnit.myGame.addUnit(invokeOnKill, parentUnit.team);
                    		       newUnit.lifeSpan = 4*15;
                    		       newUnit.x = theUnit.x;
                    		       newUnit.y = theUnit.y;
                    		       newUnit.z = theUnit.z;
                    		       newUnit.lockPosition();
                    		       newUnit.controlledByServer = true;
                                   newUnit.agressive = true;
                    		       newUnit.team = parentUnit.team;
                    		       newUnit.master = parentUnit;*/
                    	       //}
                    	       //catch(Exception e)
                    	       //{
                    		   
                    	       //}
                    	 
                           }
                        }
                    }
                }
            }
        }
    }
}
