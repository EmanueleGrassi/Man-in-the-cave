using UnityEngine;
using System.Collections;

public class bengalaBehaviour : MonoBehaviour {

    public GameObject bengalaLight;

	// Use this for initialization
	void Start () 
    {
        this.rigidbody.AddForce(new Vector3(0, 200, 500));
	}
	
	// Update is called once per frame
	void Update () 
	{
		
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
        }
    }
}
