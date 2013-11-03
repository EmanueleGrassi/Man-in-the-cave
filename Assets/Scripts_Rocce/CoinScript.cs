using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour {

    public int value;
    float groundedTime;
    bool grounded;
	// Use this for initialization
	void Start () {
        switch (gameObject.tag)
        {
            case "gold":
                value = 7;
                break;
            case "silver":
                value = 5;
                break;
            case "ruby":
                value = 8;
                break;
            case "zaff":
                value = 8;
                break;
            case "diamond":
                value = 10;
                break;
        }
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
        if (col.gameObject.tag == "Player")
        {
            CameraScript.data.points += value;
            Destroy(gameObject);
        }
            
    }
}
