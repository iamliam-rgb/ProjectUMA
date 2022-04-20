using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    //This script is responsible for handling the character animations

    //temp
    public Rigidbody rb;
    public Animator animator;
    
    private void Update()
    {
        if (rb.velocity == Vector3.zero)
        {
            animator.Play("");
        }
        else
        {
            animator.Play("");
        }
    }
}
