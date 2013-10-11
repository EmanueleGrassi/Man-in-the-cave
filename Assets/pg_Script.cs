using UnityEngine;
using System.Collections;

public class pg_Script : MonoBehaviour {

    public static bool isJumping;
    public static bool isMoving;
    public static bool isDestroyed;
	public AudioClip footstep;
	float nextPlayAudio;
    float width, height;
    public GUISkin custom;
    public static float score;

	// Use this for initialization
	void Start () {
        isJumping = false;
        isMoving = false;     
        width = Screen.width;
        height = Screen.height;
        isDestroyed = false;
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
        if (col.gameObject.tag == "rock")
        {
            Debug.Log("hai perso");
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
