using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour
{

    float groundedTime;
    bool grounded;
    int value;
    public AudioClip collected1;
    public AudioClip collected2;
    public AudioClip collected3;

    void Start()
    {
        switch (gameObject.tag)
        {
            case "gold":
                value = 5;
                break;
            case "silver":
                value = 4;
                break;
            case "diam":
                value = 10;
                break;
            case "ruby":
                value = 8;
                break;
            case "zaff":
                value = 7;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (grounded && !audio.isPlaying)
            Destroy(gameObject);
        if (Time.time > groundedTime + 0.2f)
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "ground")
        {
            audio.Play();
            grounded = true;
            renderer.enabled = false;
            collider.enabled = false;
        }
        if (col.gameObject.tag == "Player")
        {
            PlayScript.gamePoints += value;
            //SUONI
            int random = Random.Range(0, 3);
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
        }
    }
}