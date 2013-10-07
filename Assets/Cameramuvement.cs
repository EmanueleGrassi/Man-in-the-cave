using UnityEngine;
using System.Collections;

public class Cameramuvement : MonoBehaviour 
{
	
	float nexshot = 0.0f;
	float positionX = 40f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{		
		positionX = GameObject.FindGameObjectWithTag("Player").transform.position.x;
		
		transform.position= new Vector3(positionX,  21.51884f  , -1f*Mathf.Sqrt( (1- ((positionX*positionX)/1600))*676 ));
		transform.Rotate(new Vector3(18.5f, Mathf.Atan(-((13*positionX)/(20*Mathf.Sqrt(1600-(positionX*positionX))))), 0));
		nexshot= Time.time+0.01f;
        //print("ok");
	}
}
//