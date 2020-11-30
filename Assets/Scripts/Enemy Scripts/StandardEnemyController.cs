using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnemyController : EnemyController
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
