using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] ParticleSystem BulletBreck;
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player" || other.tag == "Wall" || other.tag == "Ground"){
            if(other.tag == "Player"){
                other.GetComponent<Player>().Hit();
            }
            Instantiate(BulletBreck,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
