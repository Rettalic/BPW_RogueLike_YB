
using UnityEngine;

public class PlayerCameraFollow : MonoBehaviour
{
    public Transform targetPlayer;
    public float smoothSpeedCam = 0.5f;
    [SerializeField] Vector3 offset;
          
    void LateUpdate()
    {
        Vector3 desiredPos = targetPlayer.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeedCam);
        transform.position = smoothPos;

        transform.LookAt(targetPlayer);
    }
}
