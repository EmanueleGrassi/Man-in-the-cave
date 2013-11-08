using UnityEngine;
using System.Collections;

public class SmokeBehaviour : MonoBehaviour {
	
	float DestroyTime;
	// Use this for initialization
	void Start () 
	{
		DestroyTime=Time.time+1f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Time.time>DestroyTime)
                Destroy(gameObject);
	}
}
