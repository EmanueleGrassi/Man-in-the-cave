using UnityEngine;
using System.Collections;

public class PlayGui : MonoBehaviour 
{
    float width, height;
    public Texture pause, quit;
    public static bool isPaused;
    public Transform player;    
    bool locked;
    int finger;

	// Use this for initialization
	void Start () 
	{
        width = Screen.width;
        height = Screen.height;       
        isPaused = false;        
        locked = false;
	}
	
	// Update is called once per frame
	void OnGUI () 
	{       
        if (player==null)
        {
            if (GUI.Button(new Rect(width / 2 - width / 8, height / 2 - width / 8, width / 4, width / 4), "reset"))
                Application.LoadLevel(0);
        }
        else
        {
            //////////pause
            ////////if (GUI.Button(new Rect(width / 12 * 11, 0, width / 12, width / 12), pause))
            ////////{
            ////////    if (isPaused)
            ////////    {
            ////////        Time.timeScale = 1;
            ////////        isPaused = false;
            ////////    }
            ////////    else
            ////////    {
            ////////        Time.timeScale = 0;
            ////////        isPaused = true;
            ////////    }
            ////////}

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
            if (GUI.Button(new Rect(width-200, 0, width / 11, width / 11), quit))
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

    public void pauseUnpause()
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
}
