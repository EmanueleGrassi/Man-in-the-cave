using UnityEngine;
using System.Collections;

public class IndicTexture : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        float height2 = Screen.width / 6;
        float margin = Screen.width / 60;
        guiTexture.pixelInset = new Rect(margin, -(margin + height2 * 700 / 600), height2, height2 * 700 / 600);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
