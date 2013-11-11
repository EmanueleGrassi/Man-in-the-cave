using UnityEngine;
using System.Collections;

public class LightBehaviour : MonoBehaviour {

    int r, g, b;

	// Use this for initialization
	void Start () 
    {
        if (CameraScript.data.helmet == Helmet.red)
        {
            this.light.color = Color.red;
        }
        if (CameraScript.data.helmet == Helmet.blue)
        {
            this.light.color = Color.blue;
        }
        if (CameraScript.data.helmet == Helmet.pink)
        {
            this.light.color = Color.magenta;
        }
        if (CameraScript.data.helmet == Helmet.green)
        {
            this.light.color = Color.green;
        }
	}
	
	// Update is called once per frame
    void Update()
    {    }
}
