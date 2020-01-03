using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float timeFromSpawn = 0f;
    public float lifeTime = 1f;
    public float velocityMultiplier = 1f;
    public bool enemyShot = false;
    
    void Update()
    {
        Move();
        PlayerHitCheck();
    }

    public void Move()
    {
        timeFromSpawn += Time.deltaTime;
        if (timeFromSpawn >= lifeTime)
        {
            Destroy(gameObject);
        }
        transform.position += transform.forward * velocityMultiplier * Time.deltaTime;
    }

    void PlayerHitCheck() {
        if (enemyShot) {//если выстрел врага, не игрока
            if (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CapsuleCollider>() != null) // защита от повторного вызова убитых объектов
            {
                Bounds playerbounds = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CapsuleCollider>().bounds;
                if (gameObject.GetComponent<SphereCollider>().bounds.Intersects(playerbounds)) { //пересечение пули и игрока
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerChar>().GameOver();
                }
            }
        }
    }

}
