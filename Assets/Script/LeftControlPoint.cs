using UnityEngine;
using System.Collections;

public class LeftControlPoint : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
        GUITexture gui = gameObject.guiTexture;
        gui.pixelInset = new Rect(gui.pixelInset.x * Screen.width / 1280, gui.pixelInset.y * Screen.width / 1280, gui.pixelInset.width * Screen.width / 1280,
            gui.pixelInset.height * Screen.width / 1280);
	}
}
