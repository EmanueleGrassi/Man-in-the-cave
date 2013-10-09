using UnityEngine;
using System.Collections;

public class pg_Script : MonoBehaviour {

    public static bool isJumping;
    public static bool isMoving;
	public AudioClip footstep;
	float nextPlayAudio;

	// Use this for initialization
	void Start () {
        isJumping = false;
        isMoving = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!isJumping && isMoving)
		{
			if (Time.time>nextPlayAudio) 
			{
				audio.PlayOneShot(footstep);
				nextPlayAudio=Time.time+0.3f;
			}
		}
        if (rigidbody.velocity == Vector3.zero)
        {
            isMoving = false;
        }
	}
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "ground")
            isJumping = false;
    }
}
