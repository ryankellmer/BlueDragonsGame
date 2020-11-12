using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSingleton : MonoBehaviour
{
    public static MusicSingleton instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
