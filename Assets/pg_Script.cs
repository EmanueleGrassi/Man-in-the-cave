using UnityEngine;
using System.Collections;

public class pg_Script : MonoBehaviour {

    public static bool isJumping;
    public static bool isMoving;
	public AudioClip footstep;
	float nextPlayAudio;
    public Texture jump;
    public float jumpForce;
    float width, height;
    public GUISkin custom;

	// Use this for initialization
	void Start () {
        isJumping = false;
        isMoving = false;
        width = Screen.width;
        height = Screen.height;
        jumpForce = 60;
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
            if (GUI.Button(new Rect(width / 11 * 10, height - width / 11, width / 11, width / 11), jump))
            {
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
    }
}
