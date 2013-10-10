using UnityEngine;
using System.Collections;

public class pg_Script : MonoBehaviour {

    public static bool isJumping;
    public static bool isMoving;
    public static bool isDestroyed;
	public AudioClip footstep;
	float nextPlayAudio;
    float width, height;
    public float jumpForce;
    public Texture jump;
    public GUISkin custom;
    public static float score;
    Vector3 maxJumpForce;

	// Use this for initialization
	void Start () {
        isJumping = false;
        isMoving = false; 
        jumpForce = 200;
        width = Screen.width;
        height = Screen.height;
        isDestroyed = false;
        //maxJumpForce = new Vector3(0, 200, 0);
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

    void OnGUI()
    {
        GUI.skin = custom;
        if (!PlayGui.isPaused)
        {
            //jump
            if (GUI.RepeatButton(new Rect(width / 11 * 10, height - width / 11, width / 11, width / 11), jump))
            {
                //Vector3 velocity = transform.rigidbody.velocity;
                if (!pg_Script.isJumping)
                {
                    rigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
                    pg_Script.isJumping = true;
                }
            }
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
