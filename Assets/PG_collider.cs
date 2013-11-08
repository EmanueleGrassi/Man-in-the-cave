using UnityEngine;
using System.Collections;

public class PG_collider : MonoBehaviour {
    
    public Transform player;
	int random;
	public AudioClip collected1;
	public AudioClip collected2;
	public AudioClip collected3;
	
	// Use this for initialization
	void Start () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        int value;
        switch (col.gameObject.tag)
        {
            case "gold":
                value = 1;
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
                Destroy(col.gameObject);
                break;

            case "silver":
                value = 1;
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
                Destroy(col.gameObject);
                break;

            case "diam":
                value = 1;
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
                Destroy(col.gameObject);
                break;

            case "ruby":
                value = 1;
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
                Destroy(col.gameObject);
                break;

            case "zaff":
                value = 1;
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
                Destroy(col.gameObject);
                break;

            case "rock":
                Destroy(col.gameObject);
                gameObject.SendMessage("OnEndGame");
                PlayScript.State = PlayScript.PlayState.result;
                Destroy(player.gameObject);
                break;
        }
    }
}
