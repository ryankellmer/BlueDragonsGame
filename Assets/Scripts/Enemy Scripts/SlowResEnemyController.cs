using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowResEnemyController : EnemyController
{
    void OnEnable()
    {
        ResetStuff();
        Invoke("ResetPos", 0.1f);
    }
}
