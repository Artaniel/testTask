using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    void Update() {
        ShotCheck();
    }

    public void ShotCheck() {
        BoxCollider myColider = gameObject.GetComponent<BoxCollider>();
        foreach (GameObject shot in GameObject.FindGameObjectsWithTag("shot"))
        {
            if (myColider.bounds.Intersects(shot.GetComponent<SphereCollider>().bounds))// если пересекаются с выстрелом  
            {         
                Destroy(shot);
                Destroy(gameObject);
            }
        }
    }

}
