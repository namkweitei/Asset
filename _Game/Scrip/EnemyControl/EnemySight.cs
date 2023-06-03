using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Enemy enemy;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            enemy.SetTarget(other.GetComponent<Player>());
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            enemy.SetTarget(null);
        }
    }
}
