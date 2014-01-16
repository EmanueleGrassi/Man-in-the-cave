using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour
{

    float groundedTime;
    bool grounded;
    public int value;
    public AudioClip collected1;
    public AudioClip collected2;
    public AudioClip collected3;

    // Update is called once per frame
    void Update()
    {
        if (grounded && !audio.isPlaying)
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
            PlayScript.GameCredits += value;
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