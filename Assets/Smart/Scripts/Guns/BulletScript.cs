using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float Time = 15f;
       
    private void Awake()
    {
        Destroy(gameObject, Time);
    }



}
