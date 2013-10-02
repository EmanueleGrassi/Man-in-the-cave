﻿using UnityEngine;
using System.Collections;

public class Jump_Script : MonoBehaviour {

    public Transform player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touches.Length > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out hit, 1000) && hit.collider.tag == "Jump" && !pg_Script.isJumping)
                {
                    player.rigidbody.AddForce(new Vector3(0, 14f, 0), ForceMode.Impulse);
                    pg_Script.isJumping = true;
                }
            }
        }
	}
}
