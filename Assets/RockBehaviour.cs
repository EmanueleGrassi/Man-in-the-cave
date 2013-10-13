﻿using UnityEngine;
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
                gameObject.tag = "backgroundRock";
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        int rnd = Random.Range(0, 5);
        if (col.gameObject.tag == "ground" && rnd < 3)
        {
            //DANIELE METTI QUI IL SUONO DELLA ROCCIA CHE SI DISTRUGGE
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "backgroundRock")
        {
            //E PURE QUA!
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }

}
