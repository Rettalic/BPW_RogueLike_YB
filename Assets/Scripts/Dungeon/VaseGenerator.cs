using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseGenerator : MonoBehaviour
{
    public GameObject vasePrefab;
    private BoxCollider boxCol;
    public LayerMask floorMask;

    private void Awake()
    {
        boxCol = GetComponent<BoxCollider>();
    }

    public void SetFloor()
    {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, boxCol.size / 3.5f, Quaternion.identity, floorMask);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            hitColliders[i].name = "DungeonFloor";
        }
    }

    public void SetVase()
    {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, boxCol.size / 2.2f, Quaternion.identity, floorMask);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].name != "DungeonFloor")
            {
                int randomNumber = Random.Range(0, 7);
                if (randomNumber == 1)
                {
                    hitColliders[i].name = "Vase";
                    GameObject vaseObj = Instantiate(vasePrefab, new Vector3(hitColliders[i].transform.position.x, 0.5f, hitColliders[i].transform.position.z), Quaternion.Euler(-90, 0, 0));
                }
            }
        }
    }
}
