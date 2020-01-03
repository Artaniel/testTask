using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColor : Enemy
{
    [SerializeField]
    private float reactRadius = 1f;
    [SerializeField]
    private Color changedColor = Color.white;
    [SerializeField]
    private Material changedMaterial = null; //запасной материал чтобы не сменить цвет остальным enemy со старым материалом

    void Update()
    {
        ColorCheck();
        ShotCheck(); //оставляем возможность умереть от выстрела и потомку
    }

    void ColorCheck() {
        if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) <= reactRadius)
        {
            gameObject.GetComponent<MeshRenderer>().material = changedMaterial;
            gameObject.GetComponent<MeshRenderer>().material.color = changedColor;//я бы определил новый цвет в материалах, но если надо в инспекторе, то вот так. Назначаем запасной и меняем его цвет         
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, reactRadius);
    }
}
