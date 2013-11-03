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
        if (col.gameObject.tag == "rock")
        {
            Destroy(col.gameObject);
            gameObject.SendMessage("OnEndGame");
            PlayScript.State = PlayScript.PlayState.result;
            Destroy(player.gameObject);
        }
        if (col.gameObject.tag == "coin")
        {
            prendiCoin(col.gameObject);
            Destroy(col.gameObject);
        }
    }

    private void prendiCoin(GameObject g)
    {
        //VALORE
        int value = 0;
        switch (g.tag)
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
    }
}
