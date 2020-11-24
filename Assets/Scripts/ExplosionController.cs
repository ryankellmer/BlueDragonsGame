using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
     // Use this for initialization
     void Update () {
         if(!GetComponent<ParticleSystem>().IsAlive()){
             gameObject.SetActive(false);
         }
     }
}
