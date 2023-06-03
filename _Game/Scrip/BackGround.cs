using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
   //Initialize variables
    [SerializeField] float speed;
    [SerializeField] int background_count;
    [SerializeField] float background_height;
	
// Update is called once per frame
    void Update () {
	//Move down
	    transform.Translate (Vector3.down *  speed  * Time.deltaTime);
		
	    Vector2 pos = transform.position;
	    if (pos.y < -background_height) {
		    pos.y += background_height * background_count;
		    transform.position = pos;
	    }
    }

}
