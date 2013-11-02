using UnityEngine;
using System.Collections;

public class PlayGui : MonoBehaviour 
{
    float width, height;
    public Texture pause, quit;
    public Transform player; 
	public AudioClip shout1;
	public AudioClip shout2;	
	public AudioClip shout3;
	public AudioClip shout4;
    public static bool playShout;
    bool locked;
	int random;
    int finger;
	
	public enum PlayState
	{
		menu,
		play,
		pause,
		result
	}	
	public static PlayState State = PlayState.menu;
	
	// Use this for initialization
	void Start () 
	{
        width = Screen.width;
        height = Screen.height; 
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
		
	//suono urlo
    if(playShout) {
    	playShout=false;
    	random=Random.Range(0,4);
 		switch(random)
 			{
 				case 0:
 					AudioSource.PlayClipAtPoint(shout1,transform.position);
 					break;
 				case 1:
 					AudioSource.PlayClipAtPoint(shout2,transform.position);
 					break;
 				case 2:
 					AudioSource.PlayClipAtPoint(shout3,transform.position);
 					break;
 				case 3:
 					AudioSource.PlayClipAtPoint(shout4,transform.position);
 					break;
 			}
    }
    }

    public void pauseUnpause()
    {
        if (State == PlayState.pause)
        {
            Time.timeScale = 1;
			State= PlayState.play;
        }
        else
        {
            Time.timeScale = 0;
			State= PlayState.pause;
        }
    }
}
