using UnityEngine;

public class TrapRockHead : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Rigidbody2D rb;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
