using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;
using System.Linq;

public class MapGenerator : MonoBehaviour
{
    public int middleNodes = 4;
    public Tile fillTile;
    public Tile pathTile;
    public Tile testTile;
    // Start is called before the first frame update
    void Start()
    {
        Vector2Int[] nodes = new Vector2Int[middleNodes + 3];
        Random rand = new Random();
        Tilemap tilemap = GetComponent<Tilemap>();
        tilemap.CompressBounds(); //this makes the boundbox of the tilemap only contain what is drawn in the prefab
        int topRow = tilemap.cellBounds.yMax - 2;
        int bottomRow = tilemap.cellBounds.yMin + 2;
        int leftCol = tilemap.cellBounds.xMin;
        int rightCol = tilemap.cellBounds.xMax - 1;
        //generate first node
        nodes[0] = new Vector2Int(leftCol, rand.Next(bottomRow, topRow));
        // generate middle nodes
        List<int> exclude = new List<int>(); // list of excluded columns
        for(int i = 1; i < middleNodes + 1; i++){
            int rand1 = rand.Next(bottomRow + 1,topRow - 1);
            
            bool flag = true;
            int rand2 = 0;
            while (flag){
                rand2 = rand.Next(leftCol + 3, rightCol - 4);
                flag = exclude.Any(n => n == rand2);
            }
            exclude.Add(rand2);
            exclude.Add(rand2 - 1);
            exclude.Add(rand2 + 1);
            nodes[i] = new Vector2Int(rand2, rand1);
        }
        //generate last 2 nodes
        nodes[middleNodes + 2] = new Vector2Int(rightCol, rand.Next(bottomRow, topRow));
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
        UpdatePath();
        UpdateWaypoints(nodes);
    }
    // Update is called once per frame
    void Update()
    {
        
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

    void UpdateWaypoints(Vector2Int[] nodes){
        GameObject path = GameObject.Find("Path");
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
        
        path.GetComponent<Path>().wayPoints = waypoints;
    }
}
