using UnityEngine;
using System.Collections;

public class SpawnerInfameScript : MonoBehaviour {

    GameObject pg;
    public Transform rock1, rock2, rock3, rock4, rock5, rock6, marker;
    Vector3 MarkerPosition;
    float w, h;
    bool iHaveToSpawn, stoControllando;
    Vector3 spawnPosition;

	// Use this for initialization
	void Start () {
        pg = GameObject.Find("Player");
        w = Screen.width;
        h = Screen.height;
        stoControllando = false;
	}
	
	// Update is called once per frame
	void Update () {
        if ( !stoControllando)
        {
            StartCoroutine(controllo(pg.transform.position));
        }
        if (iHaveToSpawn)
        {
            spawnPosition = new Vector3(pg.transform.position.x, transform.position.y, transform.position.z);
            transform.position = spawnPosition;
            istanziaRoccia();
            MarkerPosition = Camera.main.ScreenToWorldPoint(new Vector3(w, h, 7.5f));
            Instantiate(marker, new Vector3(transform.position.x, MarkerPosition.y, 5.3f), Quaternion.identity);
            iHaveToSpawn = false;
        }
	}

    IEnumerator controllo(Vector3 actualPos)
    {
        stoControllando = true;
        yield return new WaitForSeconds(2.5f);  //aspetto 2 secondi e mezzo
        if (pg.transform.position.x < actualPos.x+2 && pg.transform.position.x > actualPos.x-2)  //se la sua posizione non è cambiata più di tanto, cazzi sua
            iHaveToSpawn = true;
        stoControllando = false;
    }

    private void istanziaRoccia()
    {
        int rnd = Random.Range(1, 7);

        switch (rnd)
        {
            case 1:
                Instantiate(rock1, transform.position, Quaternion.Euler(new Vector3(90, 0, 0)));
                rock1.rigidbody.AddForce(new Vector3(0, Random.Range(-10, 0)));
                break;
            case 2:
                Instantiate(rock2, transform.position, Quaternion.identity);
                rock2.rigidbody.AddForce(new Vector3(0, Random.Range(-10, 0)));
                break;
            case 3:
                Instantiate(rock3, transform.position, Quaternion.identity);
                rock2.rigidbody.AddForce(new Vector3(0, Random.Range(-10, 0)));
                break;
            case 4:
                Instantiate(rock4, transform.position, Quaternion.identity);
                rock2.rigidbody.AddForce(new Vector3(0, Random.Range(-10, 0)));
                break;
            case 5:
                Instantiate(rock5, transform.position, Quaternion.identity);
                rock2.rigidbody.AddForce(new Vector3(0, Random.Range(-10, 0)));
                break;
            case 6:
                Instantiate(rock6, transform.position, Quaternion.identity);
                rock2.rigidbody.AddForce(new Vector3(0, Random.Range(-10, 0)));
                break;
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 2000, 200), "" + pg.rigidbody.velocity.x);
    }
}
