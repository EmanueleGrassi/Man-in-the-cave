using UnityEngine;
using System.Collections;

public class PG_collider : MonoBehaviour {
    
    public Transform player;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "rock")
        {
            Destroy(col.gameObject);
            //gameObject.SendMessage("OnEndGame");
            PlayScript.State = PlayScript.PlayState.result;
            player.active = false;
            //Destroy(player.gameObject);
        }
    }
}
