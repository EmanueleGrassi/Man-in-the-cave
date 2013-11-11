using UnityEngine;
using System.Collections;

public class PG_collider : MonoBehaviour 
{
    
	public Transform pg;
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "rock")
        {
            //Destroy(col.gameObject);
            //gameObject.SendMessage("OnEndGame");
            PlayScript.State = PlayScript.PlayState.result;
			print("sei qui.......");
            pg.active = false;			
            //Destroy(player.gameObject);
        }
    }
}
