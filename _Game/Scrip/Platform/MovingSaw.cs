
using UnityEngine;

public class MovingSaw : MonoBehaviour
{
    [SerializeField] Vector2[] Point;
    [SerializeField] GameObject Chain;
    [SerializeField] GameObject saw;
    [SerializeField] float speed = 2f;
    private GameObject Saw;
    private int currentWaypointIndex;
    private bool reverseDirection = false;

    // Transform target;

    void Start()
    {
        CreatePoints();
        currentWaypointIndex = 0;
        Saw = Instantiate(saw, (Vector2)transform.position + Point[currentWaypointIndex], Quaternion.identity);
        Saw.transform.SetParent(transform);
        
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance((Vector2)transform.position + Point[currentWaypointIndex], Saw.transform.position) < 0.1f)
        {
            currentWaypointIndex += reverseDirection ? -1 : 1;
            
            if (currentWaypointIndex >= Point.Length)
            {
                currentWaypointIndex = Point.Length - 1;
                reverseDirection = true;
            }
            else if (currentWaypointIndex < 0)
            {
                currentWaypointIndex = 0;
                reverseDirection = false;
            }
        }
        Saw.transform.position = Vector2.MoveTowards(Saw.transform.position, (Vector2)transform.position + Point[currentWaypointIndex], Time.deltaTime * speed);
    }
    private void CreatePoints()
    {   
        for(int i = 0; i < Point.Length; i++){
            if(i + 1 < Point.Length){
                float distance = Vector2.Distance(Point[i], Point[i+1]);
                int numberOfPoints = (int)(distance / 0.15f);
                Vector2 direction = (Point[i+1] - Point[i]) / ( numberOfPoints - 1);
                for (int j = 0; j < numberOfPoints; j++)
                {
                    Vector2 newTrans = new Vector2(transform.position.x, transform.position.y);
                    Vector2 pointPosition = newTrans + Point[i] + direction * j;
                    GameObject chain = Instantiate(Chain,pointPosition, Quaternion.identity);
                    chain.transform.SetParent(transform);
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player"){
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player"){
            collision.transform.SetParent(null);
        }
    }
    private void OnDrawGizmos() {
        if(Point != null && Point.Length > 0 ){
            for(int i = 0; i < Point.Length; i++){
                if(i + 1 < Point.Length){
                    Vector3 position1 = new Vector3(Point[i].x, Point[i].y, 0f);
                    Vector3 position2 = new Vector3(Point[i+1].x, Point[i+1].y, 0f);
                    Gizmos.color = Color.green;
                    Gizmos.DrawSphere(transform.position + position1, 0.1f);
                    Gizmos.DrawSphere(transform.position + position2, 0.1f);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(transform.position + position1,transform.position + position2);
                }
            }
        }
    }
}
