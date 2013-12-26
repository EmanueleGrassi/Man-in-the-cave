using UnityEngine;
using System.Collections;

public class RedBanner : MonoBehaviour {
	void Start () 
    {
        float UnTerzo = Screen.height / 3;
        guiTexture.pixelInset = new Rect(0,  - UnTerzo / 3, Screen.width, UnTerzo / 3);
	}
}
