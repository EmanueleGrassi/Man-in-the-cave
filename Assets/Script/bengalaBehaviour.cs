using UnityEngine;
using System.Collections;

public class bengalaBehaviour : MonoBehaviour {

    public GameObject bengalaLight;
	float destroy;
	float lightTime;
	bool inizialize;
	// Use this for initialization
	void Start ()
    {
		lightTime=Time.time+1.7f;
		inizialize=true;
        Vector3 pos = this.transform.position;
        pos.z += 2f;
        this.rigidbody.AddForce(new Vector3(0, 200, 500));
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!audio.isPlaying && inizialize)
		{
			inizialize = false;
			destroy=Time.time+4f;
			bengalaLight.light.enabled=false;
		}
		
		if(Time.time>lightTime)
			bengalaLight.light.enabled=true;
		if(Time.time>destroy && !inizialize)
                Destroy(gameObject);
	}
	
	void OnDestroy()
	{
		PlayScript.BenngalaAvailable=true;
	}

}
