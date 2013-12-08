using UnityEngine;
using System.Collections;

public class ruby_script : MonoBehaviour {

    int value;
    public AudioClip collected1;
    public AudioClip collected2;
    public AudioClip collected3;

    // Use this for initialization
    void Start()
    {
        value = 8;
    }

    void OnCollisionEnter(Collision col)
    {
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
