using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animation anim;
    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //animator = gameObject.GetComponent<Animator>();
        anim = GetComponent<Animation>();  
    }

    // Update is called once per frame
    void Update()
    {
    
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.Play("Walking");
            Debug.Log("pressed A");
        }
        
    }
}
