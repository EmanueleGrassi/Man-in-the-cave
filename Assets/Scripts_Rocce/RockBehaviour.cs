using UnityEngine;
using System.Collections;

public class RockBehaviour : MonoBehaviour {
    
    public Vector3 velocity;
    public static bool Play = false;
    public AudioClip rockSound;
    public static Vector3 deathP;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
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
        }
        int rnd = Random.Range(0, 5);
        if (col.gameObject.tag == "ground" && rnd < 3)
        {
            Play = true;
            deathP = transform.position;
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "backgroundRock")
        {
            Play = true;
            deathP = transform.position;
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }

}
