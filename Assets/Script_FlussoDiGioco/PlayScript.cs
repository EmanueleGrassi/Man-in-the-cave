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
	
	
	void Update()
	{
		//suono urlo
        if(playShout) 
        {
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

    public static void pauseUnpause()
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
