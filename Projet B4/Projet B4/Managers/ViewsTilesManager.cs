using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotonB4
{
    public enum Direction
    { 
        North, South, East, West, Center
    }

    public class ViewsTilesManager
    {
        public Entity parent;
        public Vector3 lastTiledPosition = new Vector3();

        public List<ViewTile> lastCkeckedTiles = new List<ViewTile>();
        
        public ViewsTilesManager(Entity _parent, Vector3 _lastTiledPosition)
        {
            parent = _parent;
            lastTiledPosition = _lastTiledPosition;
        }

        /// <summary>
        /// Notifies the new entities that are in my range that i am here, and the entities that are out of range that i'm gone.
        /// </summary>
        public void onMove()
        {
            //remove myself from the last ViewTile...
            Vector3 lastPositionToCheck = lastTiledPosition.smash(parent.myGame.baseRefSize);
            ViewTile lastTile = parent.myGame.worldSpace[lastPositionToCheck.toString()];
            lastTile.entities.Remove(parent.id);

            //inform everyone out of my range i have left...
            foreach (ViewTile t in lastCkeckedTiles)
            {
                if (t.position.Substract(parent.position).Magnitude() < parent.myGame.baseRefSize * (parent.checkRange.x + 1))
                    t.onLeaveTile(parent);
            }

            //reset the checked tiles array 
            lastCkeckedTiles = new List<ViewTile>();

            //inform everyone i am here and update my visible entities.
            for (float x = -parent.checkRange.x; x < parent.checkRange.x; x++)
            {
                for (float y = -parent.checkRange.y; y < parent.checkRange.y; y++)
                {
                    for (float z = -parent.checkRange.z; z < parent.checkRange.z; z++)
                    { 
                        Vector3 positionToCheck = parent.position.smash(parent.myGame.baseRefSize).Add(new Vector3(x*parent.myGame.baseRefSize, y*parent.myGame.baseRefSize, z*parent.myGame.baseRefSize));
                        ViewTile tile = parent.myGame.worldSpace[positionToCheck.toString()];
                        tile.onEnterTile(parent);

                        lastCkeckedTiles.Add(tile);
                    }
                }
            }

            //add myself on the new tile...
            Vector3 newTiledPosition = parent.position.smash(parent.myGame.baseRefSize);

            try
            {
                //if the tile exists...
                ViewTile newTile = parent.myGame.worldSpace[newTiledPosition.toString()];
                newTile.entities.Add(parent.id, parent);
            }
            catch
            { 
                //otherwise we create it...
                ViewTile newTile = new ViewTile(newTiledPosition);
            }
        }

        /// <summary>
        /// removes the parent entity from all entities and its viewTile.
        /// </summary>
        public void disappear()
        {
            //inform everyone i have left...
            foreach (ViewTile t in lastCkeckedTiles)
            {
                t.onLeaveTile(parent);
            }

            //remove myself from my tile...
            Vector3 newTiledPosition = parent.position.smash(parent.myGame.baseRefSize);
            ViewTile newTile = parent.myGame.worldSpace[newTiledPosition.toString()];
            newTile.entities.Remove(parent.id);
        }
    }
}
