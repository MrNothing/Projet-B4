/*
	Generated with B4 Built-in Editor
		999* tiles
		999* pathTiles
		999* events
		999* entities
*/

public class MapsData
{
	public HashTable maps = new HashTable();
	public MapsData()
	{
		Map map_0 = new Map("fields");
		map_0.name = "fields";
		
		Bloc tmpBloc_0 = new Bloc("blocPrefab");
		tmpBloc_0.id = "0"
		tmpBloc_0.position = new Vector3(0,0,0);
		map_0.tiles.add("0", tmpBloc_0);
		
		Vector3 tile_0 = new Vector3(0,0,0);
		map_0.pathTiles.add(tile_0.toPosRefId(), tile_0);
		
		maps.add("fields", map_0);
	}
}