using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    // Start is called before the first frame update
    // private void OnTriggerEnter2D(Collider2D collision){
    //     if(collision.tag == "Player"){
    //         collision.GetComponent<Player>().Hit();
    //     }
    // }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<Player>().Hit();
        }
    }
}
