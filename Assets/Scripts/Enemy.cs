using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private BoxCollider myColider;

    void Start()
    {
        myColider = gameObject.GetComponent<BoxCollider>();
    }
    void Update() {
        foreach(GameObject shot in GameObject.FindGameObjectsWithTag("shot"))
        {
            if (myColider.bounds.Intersects(shot.GetComponent<SphereCollider>().bounds)) { // если пересекаются с выстрелом, 
                //думал реализовать через OnTriggerEnter, но условие на то что обязательно в скрипте врага, который стоит неподвижно не дает
                Destroy(shot);
                Destroy(gameObject);
            }
        }        
    }

}
