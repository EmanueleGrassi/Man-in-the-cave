using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour {

    public float value;
	// Use this for initialization
	void Start () {
	    value = 5;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "ground")
        {
            audio.Play();
            value = 0;
        }
        if (col.gameObject.tag == "Player")
        {
            Vector3 position = transform.position;
            value += position.y;
            pg_Script.score += value;
            Debug.Log("coin: " + value + "  Player score: " + pg_Script.score);
            Destroy(gameObject);
        }
            
    }
}
