using UnityEngine;
using System.Collections;

public class PlayScript : MonoBehaviour
{
    public Transform player;
    public GameObject wplight, pglight;
    public Transform bengala;
    public static int gamePoints;
    public JoystickC BengalaTouchPad;
    public enum PlayState
    {
        menu,
        play,
        pause,
        result
    }
    private static PlayState state;

    public static PlayState State
    {
        get { return state; }
        set
        {
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
                Time.timeScale = 1;
            }
            else if (state == PlayState.result)
            {
                Time.timeScale = 0;
            }
        }
    }
    void Start()
    {
        CameraScript.LoadData();
        State = PlayState.menu;
        //LUCE A SECONDA DELLA PIATTAFORMA
        if (Application.platform == RuntimePlatform.WP8Player ||
            iPhone.generation == iPhoneGeneration.iPodTouch4Gen || iPhone.generation == iPhoneGeneration.iPodTouch3Gen ||
            iPhone.generation == iPhoneGeneration.iPodTouch2Gen || iPhone.generation == iPhoneGeneration.iPodTouch1Gen ||
            iPhone.generation == iPhoneGeneration.iPhone3GS || iPhone.generation == iPhoneGeneration.iPhone3G ||
            iPhone.generation == iPhoneGeneration.iPad2Gen || iPhone.generation == iPhoneGeneration.iPad1Gen)
        {
            pglight.SetActive(false);
            wplight.SetActive(true);
        }
        else
        {
            wplight.SetActive(true);
			wplight.light.range=6f;
            pglight.SetActive(true);
        }
        gamePoints = 0;
    }

    public static bool BenngalaAvailable = true;

    void Update()
    {
        if (!audio.isPlaying)
            audio.volume = 1;
        if (CameraScript.data.numBengala == 0)
        {
            BengalaTouchPad.Disable();
        }
        else if (BengalaTouchPad.IsFingerDown() && BenngalaAvailable && CameraScript.data.numBengala > 0)
        {
            LanciaBengala();
        }
        #if UNITY_METRO
        if( (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift)) && BenngalaAvailable && CameraScript.data.numBengala > 0)
            LanciaBengala();
        #endif
    }

    private void LanciaBengala()
    {
        CameraScript.data.numBengala--;
        BenngalaAvailable = false;
        //lancia		
        Vector3 pos = player.position;
        Instantiate(bengala, new Vector3(pos.x, pos.y + 2, pos.z), Quaternion.identity);
    }
    public static void PlayClip(AudioSource audio2, params AudioClip[] list)
    {
        int random = Random.Range(0, list.Length);
        audio2.PlayOneShot(list[random]);
    }    
}
