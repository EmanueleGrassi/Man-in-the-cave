using UnityEngine;
using System.Collections;

public class PG_collider : MonoBehaviour {
    
    public Transform player;
	int random;
	public AudioClip collected1;
	public AudioClip collected2;
	public AudioClip collected3;
	
	// Use this for initialization
	void Start () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "rock")
        {
            Destroy(col.gameObject);
            Destroy(player.gameObject);
        }
        if (col.gameObject.tag == "coin")
        {
            Destroy(col.gameObject);
            //bisogna suonare anche un qualcosa quando pg prende la moneta?! si
			random=Random.Range(0,3);
			switch(random) 
			{
				case 0:
				AudioSource.PlayClipAtPoint(collected1,transform.position);
				break;
				case 1:
				AudioSource.PlayClipAtPoint(collected2,transform.position);
				break;
				case 2:
				AudioSource.PlayClipAtPoint(collected3,transform.position);
				break;
			}
            CameraScript.data.points++;
        }
    }
}
