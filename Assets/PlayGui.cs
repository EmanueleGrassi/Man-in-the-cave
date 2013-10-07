using UnityEngine;
using System.Collections;

public class PlayGui : MonoBehaviour {

    public GUISkin custom;
    float width, height;
    public Texture pause, jump, left, right;
    public static bool isPaused;
    public Transform player;

	// Use this for initialization
	void Start () {
        width = Screen.width;
        height = Screen.height;
	}
	
	// Update is called once per frame
	void OnGUI () {
        GUI.skin = custom;
        //pause
        if (GUI.Button(new Rect(width / 12 * 11, 0, width / 12, width / 12), pause))
        {
            if (isPaused)
            {
                Time.timeScale = 1;
                isPaused = false;
            }
            else
            {
                Time.timeScale = 0;
                isPaused = true;
            }
        }
        //jump
        if (GUI.Button(new Rect(width / 11 * 10, height - width / 11, width / 11, width / 11), jump))
        {
            player.rigidbody.AddForce(new Vector3(0, 14f, 0), ForceMode.Impulse);
            pg_Script.isJumping = true;
        }
        
        //left
        if (GUI.RepeatButton(new Rect(width / 11, height - width / 11, width / 11, width / 11), left))
        {
            if (!pg_Script.isJumping)
            {
                player.rigidbody.AddForce(new Vector3(-3f, 0, 0), ForceMode.VelocityChange);
                pg_Script.isMoving = true;
            }
            else
                player.rigidbody.AddForce(new Vector3(-1f, 0, 0), ForceMode.VelocityChange);
        }
        //right
        if (GUI.RepeatButton(new Rect(width / 11 * 3, height - width / 11, width / 11, width / 11), right))
        {
            if (!pg_Script.isJumping)
            {
                player.rigidbody.AddForce(new Vector3(3f, 0, 0), ForceMode.VelocityChange);
                pg_Script.isMoving = true;
            }
            else
            {
                player.rigidbody.AddForce(new Vector3(1f, 0, 0), ForceMode.VelocityChange);
            }
        }

	}

}
