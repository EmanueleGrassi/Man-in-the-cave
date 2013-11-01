using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner_script : MonoBehaviour {
    
    float speed;
    float nextShot;
    float lastSpawn;
    public Transform rock1, rock2, rock4, coin, marker;
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
        if (gameObject.tag == "spawnersin")
        {
            if (transform.position.y > 28 && transform.position.y < 35)
                transform.position += new Vector3(0, Mathf.Abs(speed) * Time.deltaTime, 0);
            else
                transform.position = new Vector3(transform.position.x, 28.1f, transform.position.z);
        }
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        if (Time.time > lastSpawn + nextShot)
        {            
            int num = Random.Range(0, 100);
            if (num <= 15)
            {
                Instantiate(coin, transform.position, Quaternion.identity);
            }
            if (num > 15 && num <= 40)
            {
                Instantiate(rock1, transform.position, Quaternion.identity);
                rock1.rigidbody.AddForce(new Vector3(0, Random.Range(-10, 0)));
            }
            if (num > 40 && num <= 65)
            {
                Instantiate(rock2, transform.position, Quaternion.identity);
                rock2.rigidbody.AddForce(new Vector3(0, Random.Range(-10, 0)));
            }
            if (num> 65 && num <= 90)
            {
                Instantiate(rock4, transform.position, Quaternion.Euler(90,0,0));
                rock4.rigidbody.AddForce(new Vector3(0, Random.Range(-10,0)));
            }
            lastSpawn = Time.time;            
			var c = Camera.main.ScreenToWorldPoint(new Vector3(w, h, 7.5f));
            if (num > 15)
			    Instantiate(marker, new Vector3(transform.position.x, c.y, 5.3f),Quaternion.identity);
        }
	} 
   

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "bound")
            speed = -speed;
    }
}
