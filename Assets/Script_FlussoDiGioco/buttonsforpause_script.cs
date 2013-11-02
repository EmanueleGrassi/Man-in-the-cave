using UnityEngine;
using System.Collections;

public class buttonsforpause_script : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayGui.State==PlayGui.PlayState.play)
            renderer.enabled = true;
        else
            renderer.enabled = false;
	}
}
