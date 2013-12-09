using UnityEngine;
using System.Collections;

public class UrloScript : MonoBehaviour {

    public AudioClip[] shouts;
    float nextshout;
    public static bool playShout, canPlay;

	// Use this for initialization
	void Start () {
        playShout = true;
	}
	
	// Update is called once per frame
	void Update () {
        

        if (Time.time > nextshout)
            canPlay = true;
        if (playShout)
        {
            playShout = false;
            canPlay = false;
            nextshout = Time.time + 4f;
            PlayScript.PlayClip(this.audio, shouts);
        }
	}
}
