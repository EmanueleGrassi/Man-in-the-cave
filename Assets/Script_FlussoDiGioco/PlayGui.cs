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
    public Vector3 currSpeed, accelerometer;
	Quaternion gyro;
    public float maxSpeed;
    bool locked, accelerometerIsOn;
    int finger;

	// Use this for initialization
	void Start () {
        width = Screen.width;
        height = Screen.height;
        speed = 0.6f;
        isPaused = false;
        maxSpeed = 6;
        jumpForce = 250;
        locked = false;
        accelerometerIsOn = false;
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

            //if (!isPaused)
            //{
            //    //left
            //    if (Input.touches.Length > 0 && !isPaused)
            //    {
            //        foreach (Touch touch in Input.touches)
            //        {
            //            RaycastHit hit;
            //            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            //            if (touch.phase == TouchPhase.Began && !locked)
            //            {
            //                finger = touch.fingerId;
            //                locked = true;
            //            }

            //            if (Physics.Raycast(ray, out hit, 10) && hit.collider.tag == "mLeft" && touch.fingerId == finger)
            //            {
            //                moveLeft();
            //            }
            //            if (touch.phase == TouchPhase.Ended && touch.fingerId == finger)
            //            {
            //                locked = false;
            //                pg_Script.isMoving = false;
            //            }
            //        }
            //    }
            //    //right
            //    if (Input.touches.Length > 0 && !isPaused)
            //    {
            //        foreach (Touch touch in Input.touches)
            //        {
            //            RaycastHit hit;
            //            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            //            if (touch.phase == TouchPhase.Began && !locked)
            //            {
            //                finger = touch.fingerId;
            //                locked = true;
            //            }

            //            if (Physics.Raycast(ray, out hit, 10) && hit.collider.tag == "mRight" && touch.fingerId == finger)
            //            {
            //                moveRight();
            //            }
            //            if (touch.phase == TouchPhase.Ended && touch.fingerId == finger)
            //            {
            //                locked = false;
            //                pg_Script.isMoving = false;
            //            }
            //        }
            //    }
            //    //jump
            //    if (Input.touches.Length > 0 && !isPaused)
            //    {
            //        foreach (Touch touch in Input.touches)
            //        {
            //            RaycastHit hit;
            //            Ray ray = Camera.main.ScreenPointToRay(touch.position);

            //            if (Physics.Raycast(ray, out hit, 10) && hit.collider.tag == "Jump" && !pg_Script.isJumping)
            //                Jump();
            //        }
            //    }
            //}
            // quit (only for test)
            if (GUI.Button(new Rect(0, 0, width / 11, width / 11), quit))
                Application.Quit();
            //            #region accelerometro e giroscopio
            //            if (GUI.Button(new Rect(width/2, 0, height/3, height/8), "Activate accelerometer"))
            //                accelerometerIsOn = !accelerometerIsOn;
            //            if (accelerometerIsOn)
            //            {
            //                accelerometer = Input.acceleration;
            //                gyro = Input.gyro.attitude;
            //                if (Input.gyro.enabled) 
            //                {

            //                }
            //                else 
            //                {
            //                    if(!(accelerometer.x < 0.25 && accelerometer.x > -0.25))
            //                    {	
            //                        if(accelerometer.x<0)
            //                            moveLeft();
            //                        else 
            //                            moveRight();
            //                    }
            //                }

            //            GUI.Label(new Rect(width / 2, 0, width / 2, height / 3), "Accelerometer x: " + accelerometer.x + @"
            //    y: " + accelerometer.y + " z: " + accelerometer.z);
            //            GUI.Label(new Rect(width / 2, height/3, width / 2, height / 3), " av: "+ Input.gyro.enabled +" Gyro x: " + gyro.eulerAngles.x+ @"
            //            y: " + gyro.y + " z: " + gyro.z);
            //            }
            //        }

            //            #endregion
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
            //player.rigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            player.rigidbody.velocity += new Vector3(0, 10, 0);
            pg_Script.isJumping = true;
        }
    }
    #endregion
}
