using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetB4
{
    //To use this pathfinder, call start() then get the "result" List<Vector3>
	public class PathFinder
	{
        int baseStep = 1;

		//AStar pathfinder for Npcs
        SortedDictionary<float, Vector3> openTiles;
        Dictionary<String, bool> closedTiles;

        Dictionary<String, Vector3> wayPoints;
        
        Vector3 target;

        public PathFinder(Dictionary<String, Vector3> _wayPoints, int _baseStep)
        {
            openTiles = new SortedDictionary<float, Vector3>();
            closedTiles = new Dictionary<String, bool>();
            wayPoints = _wayPoints;
            baseStep = _baseStep;
        }

        public void start(Vector3 start, Vector3 _target)
        {
            target = _target;
            openTiles = new SortedDictionary<float, Vector3>();
            closedTiles = new Dictionary<String, bool>();

            result = new List<Vector3>();

            bestPath = null;

            search(start);
        }

        int checkRange = 1; //not recommented to increase this, may have some weired (funny?) results...
        
        /*    
               Checkrange example for 1:
               
               XXX
               X X
               XXX
        */

        private Vector3 bestPath=null;

        private void search(Vector3 lastPoint)
        { 
            //Check around lastPoint...
            for (int i = -checkRange; i < checkRange; i++)
            {
                for (int j = -checkRange; j < checkRange; j++)
                {
                    Vector3 newPoint = lastPoint.Add(new Vector3(i * baseStep, j * baseStep, 0));
                    String rangeId = newPoint.toPosRefId(baseStep);

                    if (wayPoints[rangeId] != null && closedTiles[rangeId] == null)
                    {
                        closedTiles.Add(rangeId, true);

                        //if i am closer than the last point to the target
                        if ((lastPoint.Substract(target)).Magnitude() > newPoint.Substract(target).Magnitude())
                        {
                            openTiles.Add(newPoint.Substract(target).Magnitude(), newPoint);
                            newPoint.parent = lastPoint;
                        }
                    }
                }
            }

            //if i have ways left...
            if (openTiles.Count > 0)
            {
                float minKey = openTiles.Keys.OrderBy(x => x).Last();
                openTiles.Remove(minKey);

                bestPath = openTiles[minKey];

                search(bestPath);
            }
            else 
            {
                if (bestPath != null)
                {
                    if (bestPath.Substract(target).Magnitude() == 0)
                    {
                        //Path found!
                        end();
                    }
                    else
                    { 
                        //The destination path was not found, but a closer path was found.
                        end();
                    }
                }
                else
                { 
                    //no path was found!
                }
            }
        }

        public List<Vector3> result;
        private void end()
        {
            Vector3 lastPath = bestPath;
            do
            {
                result.Add(lastPath);
                lastPath = lastPath.parent;
            }
            while (lastPath.parent != null);
        }
	}
}