using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChar : MonoBehaviour
{
    public float velocityMutiplier = 1f;
    public GameObject rotatable; // вращаемая часть персонажа, нужно чтобы камеру не вращать с собой
    public KeyCode shootKey = KeyCode.Mouse0; // код кнопки выстрела
    public GameObject projectilePrefab;
    public bool gameOverMode = false;

    void Start()
    {
        Camera.main.transform.parent = transform;
        Camera.main.transform.LookAt(transform.position);
        //тут хорошо бы еще передвинуть камеру чтобы компенсировать возможные спавны игрока на разной дистанции, но для одной сцены не требуется

        Text shootButtonName = GameObject.FindGameObjectWithTag("shotbuttontext").GetComponent<Text>();
        shootButtonName.text = shootKey.ToString(); // это к заданию про элемент интерфейса который показывает кнопку выстрела
    }

    void Update()
    {
        if (!gameOverMode)
        {
            MoveUpdate();
            RotationUpdate();
            ShootCheck();
        }
    }

    void MoveUpdate()
    {
        float dX = Input.GetAxis("Horizontal") * Time.deltaTime * velocityMutiplier;
        float dY = Input.GetAxis("Vertical") * Time.deltaTime * velocityMutiplier;
        transform.position += new Vector3(dX, 0, dY);// ось Y для вертикального движения от плоскости игры, потому двигаем по XOZ

    }

    void RotationUpdate()
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit); // рейкаст из камеры в направлении мышки
        Vector3 mousePointOnGround = hit.point - hit.point.y * Vector3.up; // находим точку попадания в любой первый коллаедр, убираем вертикальную составляющую                                                                               
        rotatable.transform.rotation = Quaternion.LookRotation(mousePointOnGround - transform.position, Vector3.up); 

    }

    void ShootCheck() {
        if (Input.GetKeyDown(shootKey)) {            
            Vector3 spawnPos = transform.position + rotatable.transform.forward + Vector3.up;
            GameObject proj = Instantiate(projectilePrefab) as GameObject;
            proj.transform.position = spawnPos;
            proj.transform.rotation = rotatable.transform.rotation;
        }
    }

    public void GameOver()
    {
        if (!gameOverMode){ //защита от повторных вызовов
            //Debug.Log("Game over");
            gameOverMode = true;
            Destroy(rotatable);//удаляем только видимую часть персонажа, чтобы этот скрипт продолжал гасить свет, и не создавал ошибки при запросах дистанции до игрока
            StartCoroutine("LightColorLerp");
        }
    }

    IEnumerator LightColorLerp()//не очень чистый способ, т.к. могут появиться другие источники света, можно поискать варианты через непрозрачный UI если будет нужно
    {
        Color start = GameObject.FindGameObjectWithTag("light").GetComponent<Light>().color;
        Color end = Color.black;
        float time = 0f;
        float endTime = 2f;
        while (time < endTime)
        {
            GameObject.FindGameObjectWithTag("light").GetComponent<Light>().color = Color.Lerp(start, end, time/endTime);
            yield return new WaitForSeconds(0.01f);
            time += 0.01f;
        }
        GameObject.FindGameObjectWithTag("light").GetComponent<Light>().color = end;        
    }
}
