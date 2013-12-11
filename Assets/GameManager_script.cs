using UnityEngine;
using System.Collections;

public class GameManager_script : MonoBehaviour {

    public static bool PGdead;
    public static float deathTime;
    public Transform[] spawners;

	// Use this for initialization
	void Start () {
        PGdead = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (PGdead)
        {
            CameraScript.ManageButton(false);
            if (CameraScript.PlayTime > deathTime + 1.3f)//prima 2.5
            {
                if (CameraScript.replayGame)
                    CameraScript.replayGame = false;
                PlayScript.State = PlayScript.PlayState.result;
            }
        }
	}

    internal static void spanForResult(bool a, float b)
    {
        PGdead = a;
        deathTime = b;
    }
}