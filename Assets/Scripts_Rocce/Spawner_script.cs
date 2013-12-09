using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner_script : MonoBehaviour {
    
    float speed;
    float nextShot;
    float lastSpawn;
    public Transform rock1,rock2, rock3, rock4, rock5, rock6, diamond, gold, silver, ruby, zaffiro, marker, bomb;
	float w, h;
	Vector3 MarkerPosition;
    bool MarkerPositionCaptured;
	// Use this for initialization
	void Start () {
        speed = 5;
        nextShot = 3f;
        //per farli iniziare con un tempo diverso, abbiate fede
        lastSpawn = Random.Range(0f, 4f);
		 w = Screen.width;
		 h = Screen.height;
         MarkerPositionCaptured = false;
	}
  
	// Update is called once per frame
	void Update () {
        if (PlayScript.State == PlayScript.PlayState.play)
        {
            if (gameObject.tag == "spawnersin")
            {
                if (transform.position.y > 28 && transform.position.y < 35)
                    transform.position += new Vector3(0, Mathf.Abs(speed) * Time.deltaTime, 0);
                else
                    transform.position = new Vector3(transform.position.x, 28.1f, transform.position.z);
            }
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);

            //SPAWN DI ROCCE E COINS
            if (CameraScript.PlayTime > lastSpawn + nextShot)
            {
                int num = Random.Range(0, 100);
                if (num <= 17)
                    istanziaGemma();
                else if (num > 17 && num < 24)
                    Instantiate(bomb, transform.position, Quaternion.identity);
                else
                    istanziaRoccia();

                lastSpawn = CameraScript.PlayTime;
				if(MarkerPositionCaptured==false)
				{
					MarkerPosition = Camera.main.ScreenToWorldPoint(new Vector3(w, h, 7.5f));
					MarkerPositionCaptured= true;
				}
                if (num > 14)
                    Instantiate(marker, new Vector3(transform.position.x, MarkerPosition.y, 5.3f), Quaternion.identity);
            }
            nextShot = 5 / (Mathf.Log(CameraScript.PlayTime + 2) - Mathf.Log(CameraScript.PlayTime + 2) / 2);
        }
	}

    private void istanziaRoccia()
    {
        int rnd = Random.Range(1,7);

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

    private void istanziaGemma()
    {
        int num = Random.Range(0, 44);
        if (num <= 4)
            //diamante
            Instantiate(diamond, transform.position, Quaternion.identity);
        else if (num <= 14)
            //oro
            Instantiate(gold, transform.position, Quaternion.identity);
        else if (num <= 29)
            //argento
            Instantiate(silver, transform.position, Quaternion.identity);
        else if (num <= 35)
            //rubino
            Instantiate(ruby, transform.position, Quaternion.identity);
        else
            //zaff
            Instantiate(zaffiro, transform.position, Quaternion.identity);
    } 
   

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "bound")
            speed = -speed;
    }
}
