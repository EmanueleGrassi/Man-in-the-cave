using UnityEngine;
using System.Collections;

public class PlayGui : MonoBehaviour {

    public GUISkin custom;
    float width, height;
    public Texture pause, left, right, quit, jump;
    public static bool isPaused;
    public Transform player;
    public float speed;
    public float jumpForce;
    public Vector3 currSpeed;
    public float maxSpeed;

	// Use this for initialization
	void Start () {
        width = Screen.width;
        height = Screen.height;
        speed = 0.6f;
        isPaused = false;
        maxSpeed = 6;
        jumpForce = 200;
	}
	
	// Update is called once per frame
	void OnGUI () {
        GUI.skin = custom;

        if (pg_Script.isDestroyed)
        {
            if (GUI.Button(new Rect(width / 2 - width / 8, height / 2 - width / 8, width / 4, width / 4), "reset"))
                Application.LoadLevel(0);
        }
        else
        {
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

            if (!PlayGui.isPaused)
            {
                //left
                if (GUI.RepeatButton(new Rect(width / 11, height - width / 11, width / 11, width / 11), left))
                {
                    moveLeft();
                }
                //right
                if (GUI.RepeatButton(new Rect(width / 11 * 3, height - width / 11, width / 11, width / 11), right))
                {
                    moveRight();
                }
                //jump
                if (GUI.RepeatButton(new Rect(width / 11 * 10, height - width / 11, width / 11, width / 11), jump))
                {
                    Jump();
                }
                //////////// NON FUNZIONANO...
                //// jump & left
                //if (GUI.RepeatButton(new Rect(width / 11, height - width / 11, width / 11, width / 11), left)
                //    && GUI.RepeatButton(new Rect(width / 11 * 10, height - width / 11, width / 11, width / 11), jump))
                //{
                //    Jump();
                //    moveLeft();
                //}
                //// jump & right
                //if (GUI.RepeatButton(new Rect(width / 11 * 3, height - width / 11, width / 11, width / 11), right)
                //    && GUI.RepeatButton(new Rect(width / 11 * 10, height - width / 11, width / 11, width / 11), jump))
                //{
                //    moveRight();
                //    Jump();
                //}
            }
            // quit (only for test)
            if (GUI.Button(new Rect(0, 0, width / 11, width / 11), quit))
                Application.Quit();
        }

	}
    #region spostamento e salto
    void moveLeft()
    {
        if (!pg_Script.isJumping)
        {
            if (currSpeed.x > -maxSpeed)
            {
                player.rigidbody.AddForce(new Vector3(-speed, 0, 0), ForceMode.VelocityChange);
                pg_Script.isMoving = true;
            }
        }
        else
            if (currSpeed.x > -maxSpeed)
            {
                player.rigidbody.AddForce(new Vector3(-speed / 3, 0, 0), ForceMode.VelocityChange);
            }
    }

    void moveRight()
    {
        if (!pg_Script.isJumping)
        {
            if (currSpeed.x < maxSpeed)
            {
                player.rigidbody.AddForce(new Vector3(speed, 0, 0), ForceMode.VelocityChange);
                pg_Script.isMoving = true;
            }
        }
        else
        {
            if (currSpeed.x < maxSpeed)
            {
                player.rigidbody.AddForce(new Vector3(speed / 3, 0, 0), ForceMode.VelocityChange);
            }
        }
    }

    void Jump()
    {
        if (!pg_Script.isJumping)
        {
            player.rigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            pg_Script.isJumping = true;
        }
    }
    #endregion
}
