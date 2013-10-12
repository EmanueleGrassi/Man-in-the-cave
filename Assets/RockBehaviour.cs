using UnityEngine;
using System.Collections;

public class RockBehaviour : MonoBehaviour {
    
    public Vector3 velocity;
    bool Played = false;
    public AudioClip rockSound;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.tag == "ground" && !Played)
        {
            audio.PlayOneShot(rockSound);
            Played = true; 
        }
        if (gameObject.tag != "ground")
        {
            velocity = gameObject.rigidbody.velocity;
            if (velocity.y > -0.18f)
            {
                gameObject.tag = "ground";
            }
        }
	}
}
