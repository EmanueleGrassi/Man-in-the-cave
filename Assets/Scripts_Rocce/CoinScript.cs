using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour {
    
    float groundedTime;
    bool grounded;
	// Use this for initialization
	void Start () {
        
	}

    // Update is called once per frame
    void Update()
    {
        if (grounded)
            if (Time.time > groundedTime + 2)
                Destroy(gameObject);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "ground")
        {
            audio.Play();
            groundedTime = Time.time;
            grounded = true;
        }
    }
}
