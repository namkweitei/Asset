using UnityEngine;

public class Fruits : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private ParticleSystem colectionVFX;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            other.GetComponent<Player>().AddFruit();
            Instantiate(colectionVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
