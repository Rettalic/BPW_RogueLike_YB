using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseBreak : MonoBehaviour
{
    public GameObject goldObject;
 
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerProjectile"))
        {
            Destroy(gameObject);
            Instantiate(goldObject, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.3f, gameObject.transform.position.z), gameObject.transform.rotation);
            Quest.Instance.VasePlus();
        }
    }

}