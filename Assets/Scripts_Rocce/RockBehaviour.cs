using UnityEngine;
using System.Collections;

public class RockBehaviour : MonoBehaviour {
    
    public Vector3 velocity;
    public static bool Play = false;
    public AudioClip rockSound;
    public static Vector3 deathP;
	GameObject pg;
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
			if(deathP.x<pg.transform.position.x+10f && deathP.x>pg.transform.position.x-10f)
				PlayScript.playShout=true;
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
			//finisce il gioco
			//la roccia suona l'urlo di morte del pg
			Destroy(col.gameObject);
		}
		
    }

}
