using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootController : MonoBehaviour
{
    public GameObject bullet;
    public int speed = 6;
    public Transform bulletSpawnPoint;

    public void Start()
    {
        transform.Translate(transform.forward * Time.deltaTime * speed * -1);
    }




}
