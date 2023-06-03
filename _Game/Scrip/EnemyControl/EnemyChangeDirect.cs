using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChangeDirect : MonoBehaviour
{
    [SerializeField] bool isRight;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy"){
            if(other.GetComponent<Enemy>().enemyType == EnemyType.Shell){
                other.GetComponent<Enemy>().ChangeAnimation("wallhit");
            }
            other.GetComponent<Enemy>().ChangeDicrect(isRight);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Enemy"){
            if(other.GetComponent<Enemy>().enemyType == EnemyType.Shell){
                other.GetComponent<Enemy>().ChangeAnimation("idle");
            }
        }
    }
}
