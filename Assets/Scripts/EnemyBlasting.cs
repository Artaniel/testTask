using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlasting : Enemy
{
    public int X = 2; //количество снарядов
    public GameObject projectilePrefab;
    void Update()
    {
        ShotCheck();
    }

    new void ShotCheck() { // override для этого метода чтобы избежать спавна снарядов в OnDestroy()
        BoxCollider myColider = gameObject.GetComponent<BoxCollider>();
        foreach (GameObject shot in GameObject.FindGameObjectsWithTag("shot"))
        {
            if (myColider.bounds.Intersects(shot.GetComponent<SphereCollider>().bounds))
            {
                Destroy(shot);
                SpawnShots();
                Destroy(gameObject);
            }
        }
    }
    void SpawnShots()
    {
        float angle;
        GameObject projectile;
        for (int i = 0; i < X; i++) {
            angle = (360f / X) * i; //угол вылета текущего снаряда, точнее азимут
            projectile = Instantiate(projectilePrefab) as GameObject;
            projectile.transform.position = transform.position;
            projectile.transform.Rotate(Vector3.up, angle);
            projectile.GetComponent<Projectile>().enemyShot = true; // переключаем в режим вражеского выстрела
        }
    }
}
