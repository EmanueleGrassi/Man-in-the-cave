using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour {
    
    float groundedTime;
    bool grounded;
    public AudioClip collected1;
    public AudioClip collected2;
    public AudioClip collected3;
	

    // Update is called once per frame
    void Update()
    {
        if (grounded)
            if (Time.time > groundedTime + 2.5f)
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
            int value, random;
            switch (gameObject.tag)
            {
                case "gold":
                    value = 5;
                    CameraScript.data.points += value;
                    //SUONI
                    random = Random.Range(0, 3);
                    switch (random)
                    {
                        case 0:
                            AudioSource.PlayClipAtPoint(collected1, transform.position);
                            break;
                        case 1:
                            AudioSource.PlayClipAtPoint(collected2, transform.position);
                            break;
                        case 2:
                            AudioSource.PlayClipAtPoint(collected3, transform.position);
                            break;
                    }
                    Destroy(gameObject);
                    break;

                case "silver":
                    value = 4;
                    CameraScript.data.points += value;
                    //SUONI
                    random = Random.Range(0, 3);
                    switch (random)
                    {
                        case 0:
                            AudioSource.PlayClipAtPoint(collected1, transform.position);
                            break;
                        case 1:
                            AudioSource.PlayClipAtPoint(collected2, transform.position);
                            break;
                        case 2:
                            AudioSource.PlayClipAtPoint(collected3, transform.position);
                            break;
                    }
                    Destroy(gameObject);
                    break;

                case "diam":
                    value = 10;
                    CameraScript.data.points += value;
                    //SUONI
                    random = Random.Range(0, 3);
                    switch (random)
                    {
                        case 0:
                            AudioSource.PlayClipAtPoint(collected1, transform.position);
                            break;
                        case 1:
                            AudioSource.PlayClipAtPoint(collected2, transform.position);
                            break;
                        case 2:
                            AudioSource.PlayClipAtPoint(collected3, transform.position);
                            break;
                    }
                    Destroy(gameObject);
                    break;

                case "ruby":
                    value = 8;
                    CameraScript.data.points += value;
                    //SUONI
                    random = Random.Range(0, 3);
                    switch (random)
                    {
                        case 0:
                            AudioSource.PlayClipAtPoint(collected1, transform.position);
                            break;
                        case 1:
                            AudioSource.PlayClipAtPoint(collected2, transform.position);
                            break;
                        case 2:
                            AudioSource.PlayClipAtPoint(collected3, transform.position);
                            break;
                    }
                    Destroy(gameObject);
                    break;

                case "zaff":
                    value = 7;
                    CameraScript.data.points += value;
                    //SUONI
                    random = Random.Range(0, 3);
                    switch (random)
                    {
                        case 0:
                            AudioSource.PlayClipAtPoint(collected1, transform.position);
                            break;
                        case 1:
                            AudioSource.PlayClipAtPoint(collected2, transform.position);
                            break;
                        case 2:
                            AudioSource.PlayClipAtPoint(collected3, transform.position);
                            break;
                    }
                    Destroy(gameObject);
                    break;
            }
        }
    }
}
