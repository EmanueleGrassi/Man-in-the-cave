using UnityEngine;
using System.Collections;

public class PlayScript : MonoBehaviour
{
    float width, height, nextjump;
	public Transform player;
	public AudioClip shout1;
	public AudioClip shout2;
	public AudioClip shout3;
	public AudioClip shout4;
	public AudioClip shout5;
    public AudioClip jump1, jump2, jump3, jump4, jump5, jump6, jump7;
	public GameObject wplight, pglight;
    public Transform bengala;
	public static bool playShout;
	bool locked;
    bool playJump;
	int random;
	int finger;
    float nextshout, startposition;
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
        startposition = transform.position.y;
        playJump = true;
		State = PlayState.menu; 
		//LUCE A SECONDA DELLA PIATTAFORMA
		if (Application.platform == RuntimePlatform.WP8Player || Application.platform == RuntimePlatform.Android) {
			wplight.active = true;
		} else
			pglight.active = true;
       
	}

    //void OnEndGame ()
    //{
    //    // Disable joystick when the game ends	
    //    BengalaTouchPad.Disable ();
    //}

	public static bool BenngalaAvailable = true;

	void Update ()
	{
        
        if (CameraScript.data.numBengala == 0)
        {
            BengalaTouchPad.Disable();
        }
        if (BengalaTouchPad.IsFingerDown() && BenngalaAvailable && CameraScript.data.numBengala > 0)
        {
            CameraScript.data.numBengala--;
            BenngalaAvailable = false;
            //lancia		
            Vector3 pos = player.position;
            Instantiate(bengala, new Vector3(pos.x, pos.y + 2, pos.z), Quaternion.identity);
        }
        //suono urlo
        if (playShout && Time.time > nextshout)
        {
            playShout = false;
            nextshout = Time.time + 4f;
            PlayClip(this.audio, shout1, shout2, shout3, shout4, shout5);
        }
        //suono salto
        if (playJump && transform.position.y>1.74)
        {
            playJump = false;
            PlayClip(this.audio, jump1, jump2, jump3, jump4, jump5, jump6, jump7);
            nextjump = Time.time + 2;
        }
        if (transform.position.y == 1.74)
        {
            playJump = true;
        }
	}

	public static void PlayClip(AudioSource audio2,params AudioClip[] list)
    {
        int random = Random.Range(0, list.Length);
        audio2.PlayOneShot(list[random]);
    }
}
