using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public float speed = 6;

    void Start()
    {

    }

    void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime * speed * -1);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            return;
        }

        if (other.CompareTag("Vase"))
        {
            Destroy(other);
        }
        Destroy(gameObject);
    }
}
