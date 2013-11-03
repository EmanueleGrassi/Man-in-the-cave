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
	
	public AudioClip morte1;
	public AudioClip morte2;	
	public AudioClip morte3;
    public static bool playShout;
	public static bool playDeath;
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
		if(playDeath)
		{
			random=Random.Range(0,3);
			switch(random)
 			    {
 				    case 0:
 					    AudioSource.PlayClipAtPoint(morte1,transform.position);
 					    break;
 				    case 1:
 					    AudioSource.PlayClipAtPoint(morte2,transform.position);
 					    break;
 				    case 2:
 					    AudioSource.PlayClipAtPoint(morte3,transform.position);
 					    break;
				}
		}
		//suono urlo
        if(playShout) 
        {
    	    playShout=false;
    	    random=Random.Range(0,5);
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
					case 4:
						AudioSource.PlayClipAtPoint(shout5,transform.position);
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
