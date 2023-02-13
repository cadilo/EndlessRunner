using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 offset; //ѕоказывает рассто€ние между игроком и камерой


    void Start()
    {
        offset = transform.position - player.position; //«адаем начальное положение камеры
    }

    void FixedUpdate()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z + player.position.z);    //ѕозици€ камеры - это позици€ игрока - offset
        transform.position = newPosition;
    }
}
