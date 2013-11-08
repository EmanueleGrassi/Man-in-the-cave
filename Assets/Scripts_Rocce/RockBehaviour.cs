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
	public Transform RockSmoke;
	public Transform PGblood;
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
	
	void OnDestroy()
	{
		Instantiate(RockSmoke,
			new Vector3( gameObject.transform.position.x, -0.3f, gameObject.transform.position.z),
			Quaternion.identity);		
	}
	bool ucciso=false;
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "ground" && ucciso == false)
        {
            Play = true;
            deathP = transform.position;
			if(deathP.x<pg.transform.position.x+6f && deathP.x>pg.transform.position.x-6f)
			{
				PlayScript.playShout=true;
				try {Handheld.Vibrate();} 
				catch {}
			}
			int rnd = Random.Range(0, 5);
			if ( rnd < 3)
				Destroy(gameObject);
			else
				Instantiate(RockSmoke, 
					new Vector3( gameObject.transform.position.x, -0.3f, gameObject.transform.position.z),
					Quaternion.identity);
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
			Instantiate(PGblood, 
					new Vector3( gameObject.transform.position.x, 
								gameObject.transform.position.y-1.5f, gameObject.transform.position.z),
				Quaternion.identity);			
			Destroy(col.gameObject);
			ucciso=true;
		}
		
    }

}
