using UnityEngine;
using System.Collections;

public class BombBehaviour : MonoBehaviour {
	public AudioClip miccia,esplosione,sassi_distrutti,morte1,morte2,morte3;
	float explosiontime;
	bool grounded;
	bool explosion;
	GameObject pg;
	// Use this for initialization
	void Start () {
		audio.loop=true;
		grounded=false;
		pg=GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(!grounded)
			explosiontime=Time.time;
		audio.PlayOneShot(miccia);
		if(Time.time>explosiontime){
			audio.loop=false;
			audio.PlayOneShot(esplosione);
			explosion=true;
		}
		if(explosion && pg.transform.position.x<transform.position.x+10 && pg.transform.position.x>transform.position.x-10){
			Destroy(pg);
			int random=Random.Range(0,3);
			switch(random){
			case 0:
				AudioSource.PlayClipAtPoint(morte1,transform.position);
				break;
			case 1:
				AudioSource.PlayClipAtPoint(morte2,transform.position);
				break;
			case 2:
				AudioSource.PlayClipAtPoint(morte3,transform.position);
				break;
			}
		}
		if(!audio.isPlaying){
			Destroy(gameObject);
		}
	}
	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag=="ground")
			explosiontime=Time.time+Random.Range(2.5f,5.0f);
	}
}
