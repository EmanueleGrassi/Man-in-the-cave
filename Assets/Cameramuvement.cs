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
		nexshot= Time.time+0.01f;
		print("ok");
	}
}
//