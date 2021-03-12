using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniMapScript : MonoBehaviour
{
    public Transform player;

    void LateUpdate ()
    {
        //getting position from the player and making the minimapcamera the position of the player
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        //rotating camera with player
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
