using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Animator anim;
    bool isActive = false;   
    
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Player" && isActive == false){
            isActive = true;
            collision.GetComponent<Player>().SaveCheckPoint(transform.position);
            anim.SetTrigger("Active");
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            anim.SetTrigger("Active");
        }
    }
    // Start is called before the first frame update
}
