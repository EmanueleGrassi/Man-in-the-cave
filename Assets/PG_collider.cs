using UnityEngine;
using System.Collections;

public class PG_collider : MonoBehaviour {
    
    public Transform player;
	
	
	// Use this for initialization
	void Start () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "rock")
        {
            Destroy(col.gameObject);
            gameObject.SendMessage("OnEndGame");
            PlayScript.State = PlayScript.PlayState.result;
            Destroy(player.gameObject);
        }
    }
}
