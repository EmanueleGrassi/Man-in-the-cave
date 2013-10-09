using UnityEngine;
using System.Collections;

public class Spawner_script : MonoBehaviour {
    
    float speed;
    float nextShot;
    float lastSpawn;
    public Transform rock1, rock2, rock4, coin;

	// Use this for initialization
	void Start () {
        speed = 5;
        nextShot = 2f;
        //per farli iniziare con un tempo diverso, abbiate fede
        lastSpawn = Random.Range(1f, 5f);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        if (Time.time > lastSpawn + nextShot)
        {
            int num = Random.Range(0, 100);
            if (num <= 15)
                Instantiate(coin, transform.position, Quaternion.identity);
            if (num > 15 && num <= 40)
                Instantiate(rock1, transform.position, Quaternion.identity);
            if (num > 40 && num <= 65)
                Instantiate(rock2, transform.position, Quaternion.identity);
            if (num> 65 && num <= 90)
                Instantiate(rock4, transform.position, Quaternion.identity);
            lastSpawn = Time.time;
        }
	}

    void OnCollisionEnter(Collision col)
    {
        speed = -speed;
    }
}
