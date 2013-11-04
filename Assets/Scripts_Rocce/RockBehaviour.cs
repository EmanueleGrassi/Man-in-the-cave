using UnityEngine;
using System.Collections;

public class RockBehaviour : MonoBehaviour {
    
    public Vector3 velocity;
    public static bool Play = false;
    public AudioClip rockSound;
    public static Vector3 deathP;
	GameObject pg;
	int random;
	public AudioClip morte1;
	public AudioClip morte2;	
	public AudioClip morte3;
	// Use this for initialization
	void Start () {
		pg=GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (gameObject.tag != "ground")
        {
            velocity = gameObject.rigidbody.velocity;
            if (velocity.y > -0.18f)
            {
                gameObject.tag = "backgroundRock";
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "ground")
        {
            Play = true;
            deathP = transform.position;
			if(deathP.x<pg.transform.position.x+7f && deathP.x>pg.transform.position.x-7f)
			{
				PlayScript.playShout=true;
				try {Handheld.Vibrate();} 
				catch {}
			}
			int rnd = Random.Range(0, 5);
			if ( rnd < 3)
            	Destroy(gameObject);
        }
        else if (col.gameObject.tag == "backgroundRock")
        {
            Play = true;
            deathP = transform.position;
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
		else if (col.gameObject.tag == "Player" && gameObject.tag!="backgroundRock") 
		{
			try {Handheld.Vibrate();} 
				catch {}
			random=Random.Range(0,3);
			switch(random)
 			    {
 				    case 0:
 					    AudioSource.PlayClipAtPoint(morte1,transform.position);
 					    break;
 				    case 1:
 					    AudioSource.PlayClipAtPoint(morte2,transform.position);
 					    break;
 				    case 2:
 					    AudioSource.PlayClipAtPoint(morte3,transform.position);
 					    break;
				}
			Destroy(col.gameObject);
		}
		
    }

}
