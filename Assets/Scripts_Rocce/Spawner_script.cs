using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner_script : MonoBehaviour {
    
    float speed;
    float nextShot;
    float lastSpawn;
    public Transform rock1,rock2, rock3, rock4, rock5, rock6, diamond, gold, silver, ruby, zaffiro, marker;
	float w, h;
	// Use this for initialization
	void Start () {
        speed = 5;
        nextShot = 3f;
        //per farli iniziare con un tempo diverso, abbiate fede
        lastSpawn = Random.Range(0f, 4f);
		 w = Screen.width;
		 h = Screen.height;
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
            if (Time.time > lastSpawn + nextShot)
            {
                int num = Random.Range(0, 100);
                if (num <= 5)
                {
                    // DA MODIFICARE CON LE VARIE POSSIBILITà DI GOLD SILVER ZAFF ECC
                    istanziaGemma();
                }
                else if (num > 5 && num <= 30)
                {
                    Instantiate(rock1, transform.position, Quaternion.Euler(new Vector3(90, 0 ,0 )));
                    rock1.rigidbody.AddForce(new Vector3(0, Random.Range(-10, 0)));
                }
                else if (num > 30 && num <= 65)
                {
                    Instantiate(rock2, transform.position, Quaternion.identity);
                    rock2.rigidbody.AddForce(new Vector3(0, Random.Range(-10, 0)));
                }
                else if (num > 65 && num <= 90)
                {
                    Instantiate(rock4, transform.position, Quaternion.Euler(90, 0, 0));
                    rock4.rigidbody.AddForce(new Vector3(0, Random.Range(-10, 0)));
                }
                lastSpawn = Time.time;
                Vector3 c = Camera.main.ScreenToWorldPoint(new Vector3(w, h, 7.5f));
                if (num > 10)
                    Instantiate(marker, new Vector3(transform.position.x, c.y, 5.3f), Quaternion.identity);
            }
            nextShot = 5 / (Mathf.Log(Time.time + 2) - Mathf.Log(Time.time + 2) / 2);
            //Debug.Log("" + nextShot);
        }
	}

    private void istanziaGemma()
    {
        int num = Random.Range(0, 100);
        if (num <= 6)
            //diamond
            return;
        else if (num <= 19)
            //oro
            Instantiate(gold, transform.position, Quaternion.identity);
        else if (num <= 40)
            //argento
            Instantiate(silver, transform.position, Quaternion.identity);
        else if (num <= 70)
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
