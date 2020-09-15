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
        tilemap.CompressBounds(); //this compresses the tilemap boundbox
        int topRow = tilemap.cellBounds.yMax-1;
        int bottomRow = tilemap.cellBounds.yMin;
        int leftCol = tilemap.cellBounds.xMin + 1;
        int rightCol = tilemap.cellBounds.xMax - 1;
        //generate first node
        nodes[0] = new Vector2Int(rand.Next(leftCol, rightCol),topRow);
        // generate middle nodes
        List<int> exclude = new List<int>();
        for(int i = 1; i < middleNodes + 1; i++){
            int rand1 = rand.Next(leftCol + 1,rightCol - 1);
            
            bool flag = true;
            int rand2 = 0;
            while (flag){
                rand2 = rand.Next(bottomRow + 4, topRow - 1);
                flag = exclude.Any(n => n == rand2);
            }
            exclude.Add(rand2);
            exclude.Add(rand2 - 1);
            exclude.Add(rand2 + 1);
            nodes[i] = new Vector2Int(rand1, rand2);
        }
        //generate last 2 nodes
        nodes[middleNodes + 2] = new Vector2Int(rand.Next(leftCol+1, rightCol-1),bottomRow);
        nodes[middleNodes + 1] = new Vector2Int(nodes[middleNodes + 2].x, bottomRow + 2); // this node will make the end of the path look better
        //sort the middle nodes
        //This might get changed later to give the randomness of the levels some more complexity
        nodes = nodes.OrderBy(o=> -1 * o.y).ToArray();
        for(int i = 0; i < 7; i++){
            Debug.Log(nodes[i]);
        }
        //fill the tilemap
        tilemap.FloodFill(new Vector3Int(0,0,0), fillTile);
        //draw path
        for(int i = 0; i < middleNodes + 2; i++){
            for(int j = nodes[i].y; j >= nodes[i+1].y; j--){
                tilemap.SetTile(new Vector3Int(nodes[i].x,j,0), pathTile);
            }
            if(nodes[i].x < nodes[i+1].x){
                for(int j = nodes[i].x; j <= nodes[i+1].x; j++){
                    tilemap.SetTile(new Vector3Int(j, nodes[i+1].y, 0), pathTile);
                }
            }
            else{
                for(int j = nodes[i+1].x; j <= nodes[i].x; j++){
                    tilemap.SetTile(new Vector3Int(j, nodes[i+1].y,0), pathTile);
                }
            }
        }
        //tilemap.SetTile(new Vector3Int(0,0,0), testTile);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
