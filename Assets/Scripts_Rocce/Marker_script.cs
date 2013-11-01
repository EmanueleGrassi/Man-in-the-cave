using UnityEngine;
using System.Collections;

public class Marker_script : MonoBehaviour {

    float creation;
	// Use this for initialization
	void Start () {
        creation = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > creation + 3.5f)
        {
            Destroy(gameObject);
        }
	}
}
