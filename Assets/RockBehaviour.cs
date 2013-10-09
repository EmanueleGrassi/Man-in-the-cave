using UnityEngine;
using System.Collections;

public class RockBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.rigidbody.velocity == Vector3.zero)
        {
            gameObject.tag = "ground";
        }
	}
}
