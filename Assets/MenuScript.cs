using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using System.Net;
using System.Threading;

public class MenuScript : MonoBehaviour {
	public GUISkin custom;
	float height;
	float margin, margin2;
	public Texture PlayButton, ScoreButton, ItemsButton, BuyItemsButton;
	public Texture Title, facebook,twitter,review,celialab;
	// Use this for initialization
	void Start () {
		height = Screen.width / 20;
		margin = Screen.width / 60;
		margin2 = 0;
        CameraScript.LoadData();
        StartWebRequest("http://celialab.com/Promotion.txt");

	}
	
	// Update is called once per frame
	void OnGUI () {
		GUI.skin=custom;
		float piccoliBottoniSize =Screen.width/4.6f - margin2;
		float titleHeight=((Screen.width/2)*305)/1094;
		float playSize=Screen.width/8;
		float BuyHeight= (((piccoliBottoniSize*2)*160)/740);
		float SocialSize=Screen.width/13;
		GUI.DrawTexture(new Rect ((Screen.width/2)-Screen.width/4, 0, Screen.width/2, titleHeight), 
		                Title, ScaleMode.ScaleToFit, true);	
		if (GUI.Button(new Rect((Screen.width/2)-playSize/2, titleHeight+margin*2, playSize, playSize) ,PlayButton)) 
		{
			Application.LoadLevel("main"); 
		}
		
		float piccoliBottoniHight= (((piccoliBottoniSize)*169)/400);
		if (GUI.Button( new Rect((Screen.width-((piccoliBottoniSize*2)+margin2))/2,
		                         titleHeight + playSize + margin*2,
		                         piccoliBottoniSize,piccoliBottoniHight), ScoreButton))
		{
			Application.LoadLevel("Scores");
		}
		if (GUI.Button( new Rect(((Screen.width-((piccoliBottoniSize*2)+margin2))/2) +(piccoliBottoniSize),
		                         (margin2*3)+ titleHeight + playSize+ margin*2,
		                         piccoliBottoniSize ,piccoliBottoniHight), ItemsButton))
		{
			Application.LoadLevel("Items");//vai nella pagina Items
		}			
		
		
		if (GUI.Button( new Rect((Screen.width/2)-((piccoliBottoniSize*2)/2),
		                         (margin2*4)+ titleHeight+ playSize+ piccoliBottoniHight+margin*2,
		                         piccoliBottoniSize*2,BuyHeight), BuyItemsButton))
		{
			Application.LoadLevel("BuyItems");//vai nella pagina BuyItems
		}
		
		if (GUI.Button(new Rect(Screen.width-SocialSize, Screen.height-SocialSize*3, SocialSize, SocialSize) ,facebook)) 
		{
			Application.OpenURL("https://www.facebook.com/Celialab");//vai su facebook
		}
		if (GUI.Button(new Rect(Screen.width-SocialSize, Screen.height-SocialSize*2, SocialSize, SocialSize) ,twitter)) 
		{
			Application.OpenURL("https://twitter.com/celialabGames");//vai su twitter
		}
		if (GUI.Button(new Rect(Screen.width-SocialSize, Screen.height-SocialSize, SocialSize, SocialSize) ,review)) 
		{
			if(Application.platform == RuntimePlatform.Android)
				Application.OpenURL("");//vai su review
			else if (Application.platform == RuntimePlatform.IPhonePlayer)
				Application.OpenURL("");//vai su review
			else if(Application.platform == RuntimePlatform.WP8Player || Application.platform == RuntimePlatform.WindowsPlayer)
				if(GOReviews!=null)
					GOReviews(this, new EventArgs());
			
		}
		
		var celialabHeight=((Screen.width/5)*59)/200;
		if(GUI.Button(new Rect(margin,Screen.height-(celialabHeight+margin), Screen.width/5,celialabHeight), celialab))
		{
			Application.OpenURL("http://celialab.com/");
		}
		if(Input.GetKey(KeyCode.Escape))
			Application.Quit();
	}
	public static event EventHandler GOReviews;


    private void StartWebRequest(string url)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.BeginGetResponse(new AsyncCallback(FinishWebRequest), request);
    }
   
    private void FinishWebRequest(IAsyncResult result)
    {
        try
        {
            HttpWebResponse response = (result.AsyncState as HttpWebRequest).EndGetResponse(result) as HttpWebResponse;
           // Debug.WriteLine(response.ContentType);
            if (response.StatusCode == HttpStatusCode.NotFound)
                print("non c'è");
            else
                print("c'è");
        }
        catch (Exception e)
        {
            print(e.Message);
        }

    }

}