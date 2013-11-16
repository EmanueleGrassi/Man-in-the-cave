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
            a = true;
            b = CameraScript.PlayTime;
            GameManager_script.spanForResult(a,b);
            pg.active = false;
        }
    }
}
