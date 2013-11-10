using UnityEngine;
using System.Collections;

public class ReflectorBehaviour : MonoBehaviour {
	
	float speed=12f;
	bool sali=false;

	
	// Update is called once per frame
	void Update () 
	{
		if(sali)
		{
			gameObject.light.range+=speed*Time.deltaTime;
			if (gameObject.light.range>=16.5)
				sali=false;
		}
		else
		{
			gameObject.light.range-=speed*Time.deltaTime;			
			if(gameObject.light.range<=5.5)
				sali=true;
		}	
	}
}
