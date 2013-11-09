﻿using UnityEngine;
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
	public int numBengala=3;
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
	
	//munu
	float height;
	float margin, margin2;
	public Texture PlayButton, ScoreButton, ItemsButton, BuyItemsButton;
	public Texture facebook,twitter,review,celialab;
	
	void Start ()
	{
		//carica i salvataggi
		//data= la classe deserializzata
		//per ora lascio le linee sotto, poi andra tolto
		data = new Data ();
		//fino a qui
        data.numBengala = 2;
		
		nexshot = 0.0f;
		smoothTime = 0.3f;
		Volume = 0.2f;
		PlayTime = 0;
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WP8Player)
			visualizePause = false;
		
		height = Screen.width / 20;
		margin = Screen.width / 60;
		margin2 = Screen.width / 70;
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
			
			transform.position = new Vector3 (playerPG.position.x, 
							Mathf.Sqrt ((1 - (playerPGxPOW / 3600)) * 25f),
				-1*Mathf.Sqrt ((1 - (playerPGxPOW / 1550)) * 25f));	
			
			
			transform.rotation = Quaternion.Euler (2.9f, 
				Mathf.Tan(-((2 * playerPG.position.x) / (5 * Mathf.Sqrt (2500 - playerPGxPOW)))) * 180 / Mathf.PI,
				0);			
			nexshot = Time.time + 0.01f;           
		}
	}
	
	void OnGUI ()
	{
		GUI.skin = custom;
		if (isDebuging)
			if (GUI.Button (new Rect (Screen.width / 2 - height / 2, 0, height, height), quit))
				Application.Quit ();
		
		if(PlayScript.State == PlayScript.PlayState.play)
			drawPlay();
		else if(PlayScript.State == PlayScript.PlayState.menu)
			drawMenu();
	}
	
	// PlayButton, ScoreButton, ItemsButton, BuyItemsButton;
	void drawMenu()
	{
		float piccoliBottoniSize =Screen.width/4.6f - margin2;
		float titleSize=0;
		float playSize=Screen.width/8;
			/*GUI.DrawTexture (new Rect ((Screen.width/2)-Title.width/2, margin, Screen.width/1.9f, height), 
														Title, ScaleMode.ScaleToFit, true);	*/
			if (GUI.Button(new Rect((Screen.width/2)-playSize/2, (margin2*2)+ titleSize, playSize, playSize) ,PlayButton)) 
	        {
	        	PlayScript.State = PlayScript.PlayState.play;
			}
		
				float piccoliBottoniHight= (((piccoliBottoniSize)*169)/400);
				if (GUI.Button( new Rect((Screen.width-((piccoliBottoniSize*2)+margin2))/2, (margin2*3)+ titleSize + playSize,
										Screen.width/2.3f,piccoliBottoniHight), ScoreButton))
		        {
		        	;//vai nella pagina BuyItems
				}
				if (GUI.Button( new Rect(((Screen.width-((piccoliBottoniSize*2)+margin2))/2) +(piccoliBottoniSize+margin2),
											(margin2*3)+ titleSize + playSize, Screen.width/2.3f,piccoliBottoniHight), ItemsButton))
		        {
		        	;//vai nella pagina BuyItems
				}			
		
			float BuyHeight= (((Screen.width/2.3f)*160)/740);
			if (GUI.Button( new Rect((Screen.width/2)-((Screen.width/2.3f)/2), (margin2*4)+ titleSize+ playSize+ piccoliBottoniHight,
									Screen.width/2.3f,BuyHeight), BuyItemsButton))
	        {
	        	;//vai nella pagina BuyItems
			}
        
		GUILayout.BeginArea(new Rect(Screen.width-(height+margin), Screen.height-(height*3+margin*3),height+margin,height*3+margin*3));
		GUILayout.BeginVertical();
		if (GUILayout.Button( facebook, GUILayout.Width(height)))
	        {
	        	;//vai su facebook
			}
		if (GUILayout.Button( twitter, GUILayout.Width(height)))
	        {
	        	;//vai su twitter 
			}
		if (GUILayout.Button( review, GUILayout.Width(height)))
	        {
	        	;//vai su review
			}
		GUILayout.EndVertical();
		GUILayout.EndArea();
		var celialabHeight=((Screen.width/5)*59)/200;
		GUI.Button(new Rect(margin,Screen.height-(celialabHeight+margin), Screen.width/5,celialabHeight), celialab);
	}
	
	void drawPlay()
	{
		// Visualizza punti. Lo script si adatta atutte le risoluzioni
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
        {
            if (GUI.Button(new Rect(Screen.width / 2 - height / 2, 0, height, height), quit))
                Application.Quit();           
        }
	}
}
