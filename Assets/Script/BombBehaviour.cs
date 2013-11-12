using UnityEngine;
using System.Collections;

public class BombBehaviour : MonoBehaviour {
	public AudioClip miccia,esplosione,sassi_distrutti,morte1,morte2,morte3;
	public Transform detonatorBello, detonatorMobile;
	float explosiontime;
	bool grounded;
	GameObject pg;
	// Use this for initialization
	void Start ()
    {		
		grounded=false;
		pg = GameObject.FindGameObjectWithTag("Player");
	}
	
	bool esplosioneAvvenuta=false;
	// Update is called once per frame
	void Update () 
	{	
		if(grounded && Time.time>explosiontime)
		{			
			if(!esplosioneAvvenuta)
			{
				if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WP8Player)
					Instantiate(detonatorMobile,transform.position,Quaternion.identity);				
				else
					Instantiate(detonatorBello,transform.position,Quaternion.identity);
				audio.loop=false;
				audio.PlayOneShot(esplosione);
                //
                this.gameObject.renderer.active = false;
                //
				esplosioneAvvenuta = true;
			}
			
			if(pg.transform.position.x<transform.position.x+10 && pg.transform.position.x>transform.position.x-10)
			{
				Vibrate();
				
				int random=Random.Range(0,3);
				switch(random)
				{
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
				pg.SetActive(false);
                //
                PlayScript.State = PlayScript.PlayState.result;
                //
			}
		}
		if(grounded && !audio.isPlaying){
			Destroy(gameObject);
		}
	}
	void OnCollisionEnter(Collision collision) 
	{
		if(collision.gameObject.tag=="ground")
		{
			explosiontime = Time.time+Random.Range(2.5f,5.0f);
			grounded=true;
		}
	}
	
	void Vibrate()
	{		
		try {Handheld.Vibrate();} catch {}
	}
}
