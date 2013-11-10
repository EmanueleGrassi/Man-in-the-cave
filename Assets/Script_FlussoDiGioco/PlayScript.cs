using UnityEngine;
using System.Collections;

public class PlayScript : MonoBehaviour
{
	float width, height;
	public Transform player;
	public AudioClip shout1;
	public AudioClip shout2;
	public AudioClip shout3;
	public AudioClip shout4;
	public AudioClip shout5;
	public GameObject wplight, pglight;
    public Transform bengala;
	public static bool playShout;
	bool locked;
	int random;
	int finger;
	float nextshout;
	public JoystickC BengalaTouchPad;
	public enum PlayState
	{
		menu,
		play,
		pause,
		result
	}
	private static PlayState state;

	public static PlayState State {
		get { return state; }
		set {
			state = value;
			if (state == PlayState.play) 
			{
				Time.timeScale = 1;
			} 
			else if (state == PlayState.pause) 
			{
				Time.timeScale = 0;
			} 
			else if (state == PlayState.menu) 
			{
				Time.timeScale = 0;
			} 
			else if (state == PlayState.result) 
			{
				Time.timeScale = 0;
			}
		}
	}
	
	
	
	void Start ()
	{
		State = PlayState.menu; 
		//LUCE A SECONDA DELLA PIATTAFORMA
		if (Application.platform == RuntimePlatform.WP8Player || Application.platform == RuntimePlatform.Android) {
			wplight.active = true;
		} else
			pglight.active = true;		
	}

	void OnEndGame ()
	{
		// Disable joystick when the game ends	
		BengalaTouchPad.Disable ();
	}

	public static bool BenngalaAvailable = true;

	void Update ()
	{
		
        //if (CameraScript.data.numBengala == 0) 
        //{			
        //    BengalaTouchPad.Disable ();
        //}
        //if (BengalaTouchPad.IsFingerDown () && BenngalaAvailable && CameraScript.data.numBengala > 0)
        //{	
        //    CameraScript.data.numBengala--;
        //    BenngalaAvailable = false;
        //    //lancia		
        //    Vector3 pos = player.position;
        //    Instantiate(bengala, new Vector3(pos.x, pos.y + 2, pos.z), Quaternion.identity);
        //}
        ////suono urlo
        //if (playShout && Time.time>nextshout) 
        //{
        //    playShout = false;
        //    nextshout = Time.time+4f;
        //    random = Random.Range (0, 5);
        //    switch (random) {
        //    case 0:
        //        AudioSource.PlayClipAtPoint (shout1, transform.position);
        //        break;
        //    case 1:
        //        AudioSource.PlayClipAtPoint (shout2, transform.position);
        //        break;
        //    case 2:
        //        AudioSource.PlayClipAtPoint (shout3, transform.position);
        //        break;
        //    case 3:
        //        AudioSource.PlayClipAtPoint (shout4, transform.position);
        //        break;
        //    case 4:
        //        AudioSource.PlayClipAtPoint (shout5, transform.position);
        //        break;
        //    }
        //}
	}

	void OnGUI ()
	{
		if (State == PlayState.pause) {
			Rect w2h2centrato = new Rect (Screen.width / 4, Screen.height / 4, Screen.width / 4, Screen.height / 4);
            
			GUI.backgroundColor = Color.black;
			if (GUI.Button (w2h2centrato, "Continue"))
				State = PlayState.play;
			if (GUI.Button (new Rect (Screen.width / 4, Screen.height / 4, Screen.width / 4, Screen.height / 2), "Main Menu"))
				State = PlayState.menu;
            
		}
	}
}
