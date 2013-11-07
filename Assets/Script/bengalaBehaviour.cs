using UnityEngine;
using System.Collections;

public class bengalaBehaviour : MonoBehaviour {

    public GameObject bengalaLight;
	float destroy;

	// Use this for initialization
	void Start () 
    {
        Vector3 pos = this.transform.position;
        pos.y += 0.5f;
        pos.z += 0.5f;
        this.rigidbody.AddForce(new Vector3(0, 200, 500));
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!audio.isPlaying)
			destroy=Time.time+4;
	}
	
	void OnDestroy()
	{
		PlayScript.BenngalaAvailable=true;
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "ground")
        {
            //quando NEL SUONO si accende il bengala fai
		    bengalaLight.light.enabled=true;
		    //4 secondi dopo che il suono è finito fai Destroy
			if(Time.time>destroy)
				Destroy(gameObject);
        }
    }
}
