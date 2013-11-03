using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Data
{
	public int points;
	public List<Record> Records = new List<Record>();
	int pickaxeState;/*50 max*/
	public int PickaxeState {
		get{ return pickaxeState;}
		set {
			if (value == 0 && NumberPickaxe > 0) {
				NumberPickaxe--;
				pickaxeState = 50;
			} else
				pickaxeState = value;
			
		}
	}

	public int NumberPickaxe;
	public int NumberReborn;
	public bool lightRed = false;
	public bool lightBlue = false;
	public bool lightGreen = false;
	public bool lightPink = false;
	public bool lightRainbow = false;
	public Helmet helmet=Helmet.white;
}
public enum Helmet
{
	white,red,blue,green,pink,rainbow
}
public class Record
{
	public int Seconds;
	public DateTime When;
}

public class CameraScript : MonoBehaviour
{
	public GUISkin custom;
	float nexshot;
	public Transform playerPG;
	public AudioClip rockSound;
	public AudioClip background;
	float smoothTime;
	public float Volume;
	Vector2 velocity = new Vector2 ();
	public Texture coin, pause, quit;
	public bool isDebuging = true;
	public static Data data;
	public static float PlayTime;
	bool visualizePause = true;
	
	void Start ()
	{
		//carica i salvataggi
		//data= la classe deserializzata
		//per ora lascio le linee sotto, poi andra tolto
		data = new Data ();
		//fino a qui
		
		nexshot = 0.0f;
		smoothTime = 0.3f;
		Volume = 0.2f;
		PlayTime = 0;
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WP8Player)
			visualizePause = false;
	}

	// Update is called once per frame    
	void Update ()
	{
		PlayTime += Time.deltaTime;
		if (!audio.isPlaying) {
			audio.clip = background;		
			audio.Play ();
			audio.volume = Volume;
		}

		if (RockBehaviour.Play) {
			AudioSource.PlayClipAtPoint (rockSound, RockBehaviour.deathP);
			RockBehaviour.Play = false;
		}
		if (PlayScript.State == PlayScript.PlayState.play) 
        {
            // SEGUE IL PERSONAGGIO CON LA TELECAMERA
			float playerPGxPOW = (playerPG.position.x * playerPG.position.x);
			/*ellisse 
			 * semiassi:
			 * z=6
			 * x=
			 */
			transform.position = new Vector3 (playerPG.position.x, Mathf.Sqrt ((1 - (playerPGxPOW / 4225)) * 77.44f),
								-1f * Mathf.Sqrt ((1 - (playerPGxPOW / 4225)) * 36f));//y 21.5f			
			
			//transform.rotation= Quaternion.Euler(18.5f, Mathf.Tan(-((28*playerPG.position.x)/(65*Mathf.Sqrt(4225-playerPGxPOW)))) *180/Mathf.PI, 0);
			transform.rotation = Quaternion.Euler (2.9f, Mathf.Tan (-((28 * playerPG.position.x) / (65 * Mathf.Sqrt (4225 - playerPGxPOW)))) * 180 / Mathf.PI, 0);
			//transform.Rotate(new Vector3(18.5f, Mathf.Atan(-((13*positionX)/(20*Mathf.Sqrt(1600-(positionX*positionX))))), 0));
			nexshot = Time.time + 0.01f;
           
		}
	}
	
	void OnGUI ()
	{
		GUI.skin = custom;
		// Visualizza punti. Lo script si adatta atutte le risoluzioni
		float height = Screen.width / 20;
		float margin = Screen.width / 60;
		GUI.DrawTexture (new Rect (margin, margin, height, height), coin, ScaleMode.ScaleToFit, true);		
		GUI.skin.label.fontSize = (int)height;
		GUI.Label (new Rect (height + (margin * 2), margin / 1.5f, /*moltiplicare la metà delle cifre moneta per height*/ 600f, 300f),
			"" + data.points);
		
		// se non si gioca su android o wp allora visualizza pausa
		if (visualizePause) {
			if (GUI.Button (new Rect (Screen.width - (margin + height), margin, height, height), pause)) 
            {
                PlayScript.State = PlayScript.PlayState.pause;
			}
		}        
		//Visualizza il tempo
		//GUI.DrawTexture(new Rect (margin,margin,height,height), clock, ScaleMode.ScaleToFit, true);	
		var t = (TimeSpan.FromSeconds (PlayTime));
		GUI.Label (new Rect ((float)Screen.width - (height * 3.0f + 2f * margin), 
							margin / 1.5f,
							height * 3.0f, /*moltiplicare la metà delle cifre tempo per height*/
							300.0f), string.Format ("{0}:{1:00}", t.Minutes, t.Seconds));
		
		if (isDebuging)
		if (GUI.Button (new Rect (Screen.width / 2 - height / 2, 0, height, height), quit))
			Application.Quit ();
	}
}
//