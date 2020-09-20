using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Path : MonoBehaviour
{
    public List<Vector2Int> wayPoints;
    public List<Vector3> Positions{
        get{
            return wayPoints.Select(v2 => new Vector3(){x = v2.x + 0.5f, y = v2.y + 0.5f, z = 0}).ToList();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
