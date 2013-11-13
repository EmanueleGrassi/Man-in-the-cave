using UnityEngine;
using System.Collections;

public class PG_collider : MonoBehaviour 
{
    
	public Transform pg;
    bool a = false;
    float b;
    public AudioClip collected1;
    public AudioClip collected2;
    public AudioClip collected3;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "rock")
        {
            //Destroy(col.gameObject);
            //gameObject.SendMessage("OnEndGame");
            a = true;
            b = Time.time;
            GameManager_script.spanForResult(a,b);
            //print("sei qui.......");
            pg.active = false;			
            //Destroy(player.gameObject);
        }
        switch (col.gameObject.tag)
        {
            case "gold":
                int value = 5;
                CameraScript.data.points += value;
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
                Destroy(col.gameObject);
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
                Destroy(col.gameObject);
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
                Destroy(col.gameObject);
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
                Destroy(col.gameObject);
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
                Destroy(col.gameObject);
                break;
        }

    }
}
