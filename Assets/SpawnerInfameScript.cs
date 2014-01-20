using UnityEngine;
using System.Collections;

public class SpawnerInfameScript : MonoBehaviour {

    public Transform pg;
    public Transform rock1, rock2, rock3, rock4, rock5, rock6, marker;
    Vector3 MarkerPosition;
    float w, h;
    bool iHaveToSpawn, stoControllando,initFinished, flag;
    Vector3 spawnPosition;
    GameObject bis;

	// Use this for initialization
	void Start () 
    {        
        w = Screen.width;
        h = Screen.height;
        stoControllando = false;
        initFinished = false;
        flag = true;
        bis = GameObject.Find("20bis");
	}

    IEnumerator Init()
    {
        yield return new WaitForSeconds(7);
        initFinished = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!initFinished && flag)
        {
            StartCoroutine(Init());
            flag = false;
        }
        else if (initFinished && bis.active)
        {
            if (!stoControllando)
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
	}

    IEnumerator controllo(Vector3 actualPos)
    {
        stoControllando = true;
        yield return new WaitForSeconds(2f);  //aspetto 2 secondi e mezzo
        if (pg.transform.position.x < actualPos.x+2.5 && pg.transform.position.x > actualPos.x-2.5)  //se la sua posizione non è cambiata più di tanto, cazzi sua
            iHaveToSpawn = true;
        stoControllando = false;
    }

    private void istanziaRoccia()
    {
        int rnd = Random.Range(1, 7);
        switch (rnd)
        {
            case 1:
                Instantiate(rock1, transform.position, Quaternion.Euler(new Vector3(270, 0, 0)));
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
}
