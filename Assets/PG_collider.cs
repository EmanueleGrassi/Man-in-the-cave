using UnityEngine;
using System.Collections;

public class PG_collider : MonoBehaviour 
{
    
	public Transform pg;
    bool a = false;
    float b;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "rock")
        {
            //Destroy(col.gameObject);
            //gameObject.SendMessage("OnEndGame");
            a = true;
            b = Time.time;
            GameManager_script.spanForResult(a,b);
            //print("sei qui.......");
            pg.active = false;			
            //Destroy(player.gameObject);
        }
    }
}
