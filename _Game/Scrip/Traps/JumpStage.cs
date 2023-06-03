using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStage : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float force;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            other.GetComponent<Player>().JumpForce(force);
            anim.SetTrigger("active");
        }
    }
}
