using UnityEngine;

public class DropPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Rigidbody2D rb;
    [SerializeField] BoxCollider2D col;
    Vector2 startPoint;
    private void Start(){
        startPoint = transform.position;
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    private void Drop(){
        rb.bodyType = RigidbodyType2D.Dynamic;
        col.isTrigger = true;
    }
    private void OnReset(){
        transform.position = startPoint;
        rb.bodyType = RigidbodyType2D.Static;
        col.isTrigger = false;
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0.05){
            Invoke(nameof(Drop), 0.5f);
        }
    }
}
