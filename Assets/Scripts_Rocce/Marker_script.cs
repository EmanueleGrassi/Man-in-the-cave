using UnityEngine;
using System.Collections;

public class Marker_script : MonoBehaviour {

    float creation;
    bool canPlay;
    GameObject pg;
	public AudioClip alarm;
	// Use this for initialization
	void Start () {
        creation = Time.time;
        canPlay = true;
        pg = GameObject.FindGameObjectWithTag("Player");
		if(Random.Range(0,2)==1)
			audio.clip=alarm;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > creation + 3.5f)
        {
            Destroy(gameObject);
        }
        if (pg != null)
            if (canPlay && transform.position.x<pg.transform.position.x+10f && transform.position.x>pg.transform.position.x-10f)
            {
                audio.Play();
                canPlay = false;
            }
	}
}
