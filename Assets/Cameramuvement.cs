using UnityEngine;
using System.Collections;

public class Cameramuvement : MonoBehaviour 
{
	
	float nexshot = 0.0f;
	public Transform playerPG;
    public AudioClip rockSound;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame    
	void Update () 
	{   
        if(RockBehaviour.Play)
        {
            AudioSource.PlayClipAtPoint(rockSound, RockBehaviour.deathP);
            RockBehaviour.Play = false;
        }
        if (!pg_Script.isDestroyed)
        {
            float playerPGxPOW = (playerPG.position.x * playerPG.position.x);
            transform.position = new Vector3(playerPG.position.x, Mathf.Sqrt((1 - (playerPGxPOW / 3025)) * 462.25f),
								-1f * Mathf.Sqrt((1 - (playerPGxPOW / 4225)) * 784));//y 21.5f
            transform.rotation= Quaternion.Euler(18.5f, Mathf.Tan(-((28*playerPG.position.x)/(65*Mathf.Sqrt(4225-playerPGxPOW)))) *180/Mathf.PI, 0);
            //transform.Rotate(new Vector3(18.5f, Mathf.Atan(-((13*positionX)/(20*Mathf.Sqrt(1600-(positionX*positionX))))), 0));
            nexshot = Time.time + 0.01f;
            //print("ok");
        }
	}
}
//