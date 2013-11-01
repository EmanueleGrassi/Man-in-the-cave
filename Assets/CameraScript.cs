using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour 
{
	
	float nexshot = 0.0f;
	public Transform playerPG;
    public AudioClip rockSound;
    public AudioClip background;
	
	float smoothTime = 0.3f;
	public float Volume=0.2f;
	Vector2 velocity= new Vector2();
	public Texture coin;
	
	// Update is called once per frame    
	void Update () 
	{
        if (!audio.isPlaying)  
		{
			audio.clip = background;		
            audio.Play();
			audio.volume = Volume;
		}

        if(RockBehaviour.Play)
        {
            AudioSource.PlayClipAtPoint(rockSound, RockBehaviour.deathP);
            RockBehaviour.Play = false;
        }
        if (!pg_Script.isDestroyed)
        {
            float playerPGxPOW = (playerPG.position.x * playerPG.position.x);
			
            Vector3 currentTransform = new Vector3(playerPG.position.x, Mathf.Sqrt((1 - (playerPGxPOW / 3025)) * 462.25f),
								-1f * Mathf.Sqrt((1 - (playerPGxPOW / 4225)) * 784));//y 21.5f
			transform.position= new Vector3( 
			/* X */	Mathf.SmoothDamp( currentTransform.x, playerPG.position.x, ref velocity.x, smoothTime),
			/* Y  Mathf.SmoothDamp( currentTransform.y, playerPG.position.y, ref velocity.y, smoothTime),*/ currentTransform.y,
			/* Z */	currentTransform.z );
			
            transform.rotation= Quaternion.Euler(18.5f, Mathf.Tan(-((28*playerPG.position.x)/(65*Mathf.Sqrt(4225-playerPGxPOW)))) *180/Mathf.PI, 0);
            //transform.Rotate(new Vector3(18.5f, Mathf.Atan(-((13*positionX)/(20*Mathf.Sqrt(1600-(positionX*positionX))))), 0));
            nexshot = Time.time + 0.01f;
           
        }
	}
	
	void OnGUI()
	{		
		// Visualizza moneta. Lo script si adatta atutte le risoluzioni
		float height= Screen.width/20;
		float margin= Screen.width/60;
		GUI.DrawTexture(new Rect (margin,margin,height,height), coin, ScaleMode.ScaleToFit, true);		
		GUI.skin.label.fontSize = (int)height;
		GUI.Label (new Rect (height+(margin*2), margin/1.5f,/*moltiplicare la metà delle cifre moneta per height*/ 600f ,300f), ""+height);
		
		//Visualizza il tempo
		//GUI.DrawTexture(new Rect (margin,margin,height,height), clock, ScaleMode.ScaleToFit, true);	
		GUI.Label(new Rect( (float)Screen.width - (height*2.5f +margin), 
							margin/1.5f, 
							height*4.5f, /*moltiplicare la metà delle cifre tempo per height*/
							300.0f), "02:37");
	}
}
//