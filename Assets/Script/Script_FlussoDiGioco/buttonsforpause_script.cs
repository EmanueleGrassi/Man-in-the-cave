using UnityEngine;
using System.Collections;

public class buttonsforpause_script : MonoBehaviour {

	
	// Update is called once per frame
	void Update () 
	{
        if (PlayScript.State==PlayScript.PlayState.play)
            renderer.enabled = true;
        else
            renderer.enabled = false;
	}
}
