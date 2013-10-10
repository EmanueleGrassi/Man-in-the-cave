using UnityEngine;
using System.Collections;

public class Cameramuvement : MonoBehaviour 
{
	
	float nexshot = 0.0f;
	public Transform playerPG;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (!pg_Script.isDestroyed)
        {
            float playerPGxPOW = (playerPG.position.x * playerPG.position.x);
            transform.position = new Vector3(playerPG.position.x, 21.51884f, -1f * Mathf.Sqrt((1 - (playerPGxPOW / 4225)) * 784));
            //transform.rotation= Quaternion.Euler(18.5f, Mathf.Atan(-((13*playerPG.position.x)/(20*Mathf.Sqrt(1600-playerPGxPOW)))), 0);
            //transform.Rotate(new Vector3(18.5f, Mathf.Atan(-((13*positionX)/(20*Mathf.Sqrt(1600-(positionX*positionX))))), 0));
            nexshot = Time.time + 0.01f;
            //print("ok");
        }
	}
}
//