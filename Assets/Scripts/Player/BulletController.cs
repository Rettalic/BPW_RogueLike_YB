using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;

    void Start()
    {
        Debug.Log(transform.rotation);
    }

    void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime * speed * -1);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
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
