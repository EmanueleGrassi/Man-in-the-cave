﻿using UnityEngine;
using System.Collections;

public class RedBanner : MonoBehaviour {
	void Start () 
    {
        float UnTerzo = Screen.height / 3;
        guiTexture.pixelInset = new Rect(0, -Screen.width * 81 / 1024, Screen.width, Screen.width * 81 / 1024);
	}
}
