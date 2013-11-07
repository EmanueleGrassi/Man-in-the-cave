using UnityEngine;
using System.Collections;

public class bengalaBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//quando NEL SUONO si accende il bengala fai
		Component g = gameObject.GetComponent("Light");
		g.light.enabled=true;
		//4 secondi dopo che il suono è finito fai Destroy
	}
	
	void OnDestroy()
	{
		PlayScript.BenngalaAvailable=true;
	}
}
