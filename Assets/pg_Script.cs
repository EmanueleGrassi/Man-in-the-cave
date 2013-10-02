using UnityEngine;
using System.Collections;

public class pg_Script : MonoBehaviour {

    public static bool isJumping;
    public static bool isMoving;

	// Use this for initialization
	void Start () {
        isJumping = false;
        isMoving = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "ground")
            isJumping = false;
    }
}
