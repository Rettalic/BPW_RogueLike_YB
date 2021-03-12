using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private BoxCollider boxCol;
    
    private void Awake()
    {
        boxCol = GetComponent<BoxCollider>();
    }

    private void Start()
    { 
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            GoldPoints.Instance.dropGold(15);
        }
    }
}
