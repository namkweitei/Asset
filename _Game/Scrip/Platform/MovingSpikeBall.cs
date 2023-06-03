using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpikeBall : MonoBehaviour
{
    [SerializeField] GameObject Chain;
    [SerializeField] GameObject SpikeBall;
    [SerializeField] Vector3 center;
    [SerializeField] float radius;
    [SerializeField] float StartAngle;
    [SerializeField] float EndAngle;
    [SerializeField] float speed = 2f;
    private List<GameObject> Chains = new List<GameObject>();
    private float angle;
    private Vector3 StartPoint;
    private Vector3 EndPoint;
    private bool reverseDirection = false;

    // Transform target;

    void Start()
    {
        CreatePoints();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 direction = ((Vector2)SpikeBall.transform.position - (Vector2)transform.position + (Vector2)center) / ( Chains.Count - 1);
        for (int j = 0; j < Chains.Count; j++)
        {
            Vector2 pointPosition = (Vector2)transform.position + (Vector2)center + direction * j;
            Chains[j].transform.position = pointPosition;
        }
        MoveSpikeBall();
    }
    private void MoveSpikeBall(){
        if (Vector2.Distance(StartPoint, SpikeBall.transform.position) < 0.1f)
        {
            reverseDirection = true;
        }else if(Vector2.Distance(EndPoint, SpikeBall.transform.position) < 0.1f){
            reverseDirection = false;
        }
        angle += reverseDirection ? -speed * Time.deltaTime : speed * Time.deltaTime;
    }
    private void CreatePoints()
    {   
        float distance = Vector2.Distance((Vector2)transform.position + (Vector2)center, (Vector2)SpikeBall.transform.position);
        int numberOfPoints = (int)(distance / 0.1f);
        Vector2 direction = ((Vector2)SpikeBall.transform.position - (Vector2)transform.position + (Vector2)center) / ( numberOfPoints - 1);
        for (int j = 0; j < numberOfPoints; j++)
        {
            Vector2 newTrans = new Vector2(transform.position.x, transform.position.y);
            Vector2 pointPosition = newTrans + (Vector2)center + direction * j;
            GameObject chain = Instantiate(Chain,pointPosition, Quaternion.identity);
            chain.transform.SetParent(transform);
            Chains.Add(chain);
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
    private void MathfPoint(){
        float radian = angle * Mathf.Deg2Rad;
        SpikeBall.transform.position = transform.position + center + new Vector3(Mathf.Cos(radian) * radius, Mathf.Sin(radian) * radius, 0f);
        float radian1 = StartAngle * Mathf.Deg2Rad;
        StartPoint = transform.position + center + new Vector3(Mathf.Cos(radian1) * radius, Mathf.Sin(radian1) * radius, 0f);
        float radian2 = EndAngle * Mathf.Deg2Rad;
        EndPoint = transform.position + center + new Vector3(Mathf.Cos(radian2) * radius, Mathf.Sin(radian2) * radius, 0f);
    }
    private void OnDrawGizmos() {
        MathfPoint();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + (Vector2)center, radius);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine((Vector2)transform.position + (Vector2)center,(Vector2)SpikeBall.transform.position);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(StartPoint, 0.1f);
        Gizmos.DrawSphere(EndPoint, 0.1f);
    }
}
