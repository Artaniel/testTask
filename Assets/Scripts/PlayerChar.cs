using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChar : MonoBehaviour
{
    public float velocityMutiplier = 1f;
    public Camera camera;
    public GameObject rotatable; // вращаемая часть персонажа, нужно чтобы камеру не вращать с собой
    public KeyCode shootKey = KeyCode.Mouse0; // код кнопки выстрела
    public GameObject projectilePrefab;

    void Start()
    {
        if (camera == null){
            camera = Camera.main;
        }
        camera.transform.parent = transform;
        camera.transform.LookAt(transform.position);
        //тут хорошо бы еще передвинуть камеру чтобы компенсировать возможные спавны игрока на разной дистанции

        Text shotButtonName = GameObject.FindGameObjectWithTag("shotbuttontext").GetComponent<Text>();
        shotButtonName.text = shootKey.ToString(); // это к заданию про элемент интерфейса который показывает кнопку выстрела
    }

    void Update()
    {
        MoveUpdate();
        RotationUpdate();
        ShootCheck();
    }

    void MoveUpdate()
    {
        float dX = Input.GetAxis("Horizontal") * Time.deltaTime * velocityMutiplier;
        float dY = Input.GetAxis("Vertical") * Time.deltaTime * velocityMutiplier;
        transform.position += new Vector3(dX, 0, dY);// ось Y для вертикального движения от плоскости игры, потому двигаем по XOZ

    }

    void RotationUpdate() {
        RaycastHit hit;
        Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit); // рейкаст из камеры в направлении мышки
        Vector3 mousePointOnGround = hit.point - hit.point.y*Vector3.up; // находим точку попадания в любой первый коллаедр, убираем вертикальную составляющую
        //Debug.Log(mousePointOnGround);
        rotatable.transform.rotation = Quaternion.LookRotation(mousePointOnGround - transform.position, Vector3.up); // поворачиваем к мышке
    }

    void ShootCheck() {
        if (Input.GetKeyDown(shootKey)) {            
            Vector3 spawnPos = transform.position + rotatable.transform.forward + Vector3.up;
            GameObject proj = Instantiate(projectilePrefab) as GameObject;
            proj.transform.position = spawnPos;
            proj.transform.rotation = rotatable.transform.rotation;
        }
    }

}
