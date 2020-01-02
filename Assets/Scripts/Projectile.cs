using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float timeFromSpawn = 0f;
    public float lifeTime = 1f;
    public float velocityMultiplier = 1f;
    
    void Update()
    {
        timeFromSpawn += Time.deltaTime;
        if (timeFromSpawn >= lifeTime) {
            Destroy(gameObject);
        }
        transform.position += transform.forward * velocityMultiplier * Time.deltaTime;
    }

    void OnTriggerEnter(Collider c)
    {
        Debug.Log(1);
    }
}
