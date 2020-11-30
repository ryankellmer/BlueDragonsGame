using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnResEnemyController : EnemyController
{
    void OnEnable()
    {
        Invoke("ResetStuff", 0.2f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
