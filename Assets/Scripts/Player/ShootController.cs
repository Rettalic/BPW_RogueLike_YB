using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public GameObject kunai;
    public Transform kunaiSpawnPoint;

    public GameObject shuriken;
    public Transform shurikenSpawnPoint;
    public int shurikenAmount;
    public GameObject rotationObject;
    public float shurikenDelay;

    private static ShootController _instance;

    public static ShootController Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void ShootKunai()
    {
        Debug.Log("shoot");
        Instantiate(kunai, kunaiSpawnPoint.position, kunaiSpawnPoint.rotation);
    }

    public void ShootShuriken()
    {
        StartCoroutine(ShootAround());
    }

    IEnumerator ShootAround()
    {
        for (int i = 0; i < shurikenAmount; i++)
        {
            Instantiate(shuriken, shurikenSpawnPoint.position, shurikenSpawnPoint.rotation);
            yield return new WaitForSeconds(shurikenDelay);
            rotationObject.transform.Rotate(Vector3.up, 360.0f / (shurikenAmount), Space.Self);
        }

    }
}

