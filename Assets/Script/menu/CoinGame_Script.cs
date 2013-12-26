using UnityEngine;
using System.Collections;

public class CoinGame_Script : MonoBehaviour {

    float margin;
    public GUISkin customSkin;
	void Start () 
    {
        margin = Screen.width / 60;
        customSkin.label.fontSize = (int)((Screen.width/20) * 0.6);
	}
    Rect labelPosition2;
	 void OnGUI()
     {
         if (GUI.skin != customSkin)
             GUI.skin = customSkin;
         var height2 = Screen.width / 10;
         labelPosition2 = GUILayoutUtility.GetRect(new GUIContent(PlayScript.gamePoints.ToString()), customSkin.label);
         print("width: " + labelPosition2.width + " h = " + labelPosition2.height);
         GUI.Label(new Rect(margin + height2 / 2 + height2-(height2 / 4  + labelPosition2.width), margin + height2 / 2 - (labelPosition2.height / 4), labelPosition2.width, labelPosition2.height),
               PlayScript.gamePoints.ToString());
	}
}
