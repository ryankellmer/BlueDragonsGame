using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;
using System.Linq;

public static class StaticRandom{
    public static Random rand = new Random();
}

public struct Map{
    public Vector2Int[] nodes;
    public Tilemap tiles;
}

public class MapGenerator : MonoBehaviour
{
    public int middleNodes = 4;
    public Tile fillTile;
    public Tile pathTile;
    public Tile testTile;
    private Vector2Int[] nodes;
    private List<Vector2Int> waypoints;
    public int foliageSpawnChance;
    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
        UpdatePath();
        //GenerateScenary();
    }
    private void GenerateMap(){
        nodes = new Vector2Int[middleNodes + 3];
        Tilemap tilemap = GetComponent<Tilemap>();
        tilemap.CompressBounds(); //this makes the boundbox of the tilemap only contain what is drawn in the prefab
        int topRow = tilemap.cellBounds.yMax - 2;
        int bottomRow = tilemap.cellBounds.yMin + 2;
        int leftCol = tilemap.cellBounds.xMin;
        int rightCol = tilemap.cellBounds.xMax - 1;
        //generate first node
        nodes[0] = new Vector2Int(leftCol, StaticRandom.rand.Next(bottomRow, topRow));
        // generate middle nodes
        List<int> exclude = new List<int>(); // list of excluded columns
        for(int i = 1; i < middleNodes + 1; i++){
            int rand1 = StaticRandom.rand.Next(bottomRow + 1,topRow - 1);
            
            bool flag = true;
            int rand2 = 0;
            while (flag){
                rand2 = StaticRandom.rand.Next(leftCol + 3, rightCol - 4);
                flag = exclude.Any(n => n == rand2);
            }
            exclude.Add(rand2);
            exclude.Add(rand2 - 1);
            exclude.Add(rand2 + 1);
            nodes[i] = new Vector2Int(rand2, rand1);
        }
        //generate last 2 nodes
        nodes[middleNodes + 2] = new Vector2Int(rightCol, StaticRandom.rand.Next(bottomRow, topRow));
        nodes[middleNodes + 1] = new Vector2Int(rightCol - 2, nodes[middleNodes + 2].y); // this node will make the end of the path look better
        //sort the middle nodes
        //This might get changed later to give the randomness of the levels some more complexity
        nodes = nodes.OrderBy(o=> o.x).ToArray();
        //fill the tilemap
        tilemap.FloodFill(new Vector3Int(0,0,0), fillTile);
        //draw path
        for(int i = 0; i < middleNodes + 2; i++){
            for(int j = nodes[i].x; j <= nodes[i+1].x; j++){
                tilemap.SetTile(new Vector3Int(j,nodes[i].y,0), pathTile);
            }
            if(nodes[i].y < nodes[i+1].y){
                for(int j = nodes[i].y; j <= nodes[i+1].y; j++){
                    tilemap.SetTile(new Vector3Int(nodes[i+1].x, j, 0), pathTile);
                }
            }
            else{
                for(int j = nodes[i+1].y; j <= nodes[i].y; j++){
                    tilemap.SetTile(new Vector3Int(nodes[i+1].x, j,0), pathTile);
                }
            }
        }
        //tilemap.SetTile(new Vector3Int(0,0,0), testTile);
        //UpdateWaypoints(nodes);
    }

    void UpdatePath(){
        string pathTileName = pathTile.name;
        Tile straightVertical = Resources.Load<Tile>(pathTileName + "/straight_vertical");
        Tile straightHorizontal = Resources.Load<Tile>(pathTileName + "/straight_horizontal");
        Tile corner1 = Resources.Load<Tile>(pathTileName + "/corner1");
        Tile corner2 = Resources.Load<Tile>(pathTileName + "/corner2");
        Tile corner3 = Resources.Load<Tile>(pathTileName + "/corner3");
        Tile corner4 = Resources.Load<Tile>(pathTileName + "/corner4");

        Tilemap tilemap = GetComponent<Tilemap>();
        bool upTile;
        bool downTile;
        bool rightTile;
        bool leftTile;

        foreach(var t in tilemap.cellBounds.allPositionsWithin){
            if(tilemap.GetTile(t) == pathTile){
                upTile = tilemap.GetTile(t + new Vector3Int(0, 1, 0)) != fillTile;
                downTile = tilemap.GetTile(t + new Vector3Int(0, -1, 0)) != fillTile;
                rightTile = tilemap.GetTile(t + new Vector3Int(1, 0, 0)) != fillTile;
                leftTile = tilemap.GetTile(t + new Vector3Int(-1, 0, 0)) != fillTile;
                if(rightTile){
                    if(upTile){
                        tilemap.SetTile(t, corner1);
                    }
                    else if(leftTile){
                        tilemap.SetTile(t, straightHorizontal);
                    }
                    else{
                        tilemap.SetTile(t, corner2);
                    }
                }
                else if(leftTile){
                    if(upTile){
                        tilemap.SetTile(t, corner4);
                    }
                    else if(downTile){
                        tilemap.SetTile(t, corner3);
                    }
                }
                else
                {
                    tilemap.SetTile(t, straightVertical);
                }
            }
        }
    }

    public void UpdateWaypoints(Vector2Int[] nodes){
        //GameObject path = GameObject.Find("Path");
        List<Vector2Int> waypoints = new List<Vector2Int>();
        for(int i = 0; i < nodes.Length - 1; i++){
            waypoints.Add(nodes[i]);
            waypoints.Add(new Vector2Int(nodes[i+1].x, nodes[i].y));
        }
        waypoints[0] += new Vector2Int(-1, 0);
        waypoints.Add(nodes.Last() + new Vector2Int(1, 0));
        //remove duplicates
        for(int i = 1; i < waypoints.Count; i++){
            if(waypoints[i] == waypoints[i-1]){
                waypoints.RemoveAt(i);
            }
        }
    }

    public void GenerateScenary(){
        Random rand = new Random();
        GameObject ground = GameObject.Find("Grid").transform.GetChild(0).gameObject;
        Tilemap tilemap = ground.GetComponent<Tilemap>();
        Tile fillTile = ground.GetComponent<MapGenerator>().fillTile;
        string prefabFilePath = "";
        GameObject[] prefabs = Resources.LoadAll<GameObject>(prefabFilePath);
        GameObject parent = GameObject.Find("Foliage");
        foreach(var t in tilemap.cellBounds.allPositionsWithin){
            if(tilemap.GetTile(t) == fillTile){
                if(rand.Next(100) <= foliageSpawnChance){
                    GameObject o = Instantiate(prefabs[rand.Next(prefabs.Length)], t + new Vector3(.5f,.5f,-1), Quaternion.Euler(0, 0, rand.Next(361)));
                    o.transform.localScale = new Vector3(0.5f, 0.5f, 0);
                    o.transform.position = new Vector3(o.transform.position.x * 0.5f,o.transform.position.y *  0.5f, o.transform.position.z);
                    o.transform.parent = parent.transform;
                }
            }
        }
       GameObject tp = GameObject.Find("TowerPositions");
       if(tp == null){
           GameObject tpPrefab = Resources.Load<GameObject>("Assets/Prefabs/TowerPositions.prefab");
           tp = Instantiate(tpPrefab, Vector3.zero, Quaternion.identity);
           tp.GetComponent<TowerPositions>().CreateTowerPositions();
       }
       else{
           tp.GetComponent<TowerPositions>().CreateTowerPositions();
       }
    }
    
    public void SetPath(){
        Path.wayPoints = waypoints;
    }
}
