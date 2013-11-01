using UnityEngine;
using System.Collections;

public class Marker_script : MonoBehaviour {

    float creation;
    bool canPlay;
    GameObject pg;
	// Use this for initialization
	void Start () {
        creation = Time.time;
        canPlay = true;
        pg = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > creation + 3.5f)
        {
            Destroy(gameObject);
        }
        if (canPlay && transform.position.x<pg.transform.position.x+10f && transform.position.x>pg.transform.position.x-10f)
        {
            audio.Play();
            canPlay = false;
        }
	}
}
