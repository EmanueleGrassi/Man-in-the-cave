using UnityEngine;
using System.Collections;

public class GameManager_script : MonoBehaviour {

    public static bool PGdead;
    public static float deathTime;

	// Use this for initialization
	void Start () {
        PGdead = false;
	}
	
	// Update is called once per frame
	void Update () 
    {    
        if (PGdead)
            if (Time.time > deathTime + 2.5f)
                PlayScript.State = PlayScript.PlayState.result;
	}

    internal static void spanForResult(bool a, float b)
    {
        PGdead = a;
        deathTime = b;
    }
}