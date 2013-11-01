using UnityEngine;
using System.Collections;

public class Partita : MonoBehaviour {

	// Use this for initialization
	public static int coins=0;
	public static float PlayTime=0;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	Partita.PlayTime = Time.timeSinceLevelLoad;
		print(Partita.PlayTime);
	}
}
