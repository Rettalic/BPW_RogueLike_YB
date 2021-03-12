using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    ThirdPersonMovement moveScript;

    public float dashSpeed;
    public float dashTime;
    public float extraDash;

    void Start()
    {
        moveScript = GetComponent<ThirdPersonMovement>();
    }

    public void playerDash()
    {
        StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
            moveScript.Controller.Move(moveScript.moveDir * (dashSpeed + extraDash) * Time.deltaTime);

            yield return null;
        }
    }
}
