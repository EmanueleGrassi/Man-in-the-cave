using UnityEngine;
using System.Collections;

public class BombBehaviour : MonoBehaviour {
	public AudioClip miccia,esplosione,sassi_distrutti,morte1,morte2,morte3;
	public Transform detonatorBello, detonatorMobile;
	float explosiontime;
	bool grounded;
	GameObject pg;
    bool killed;
    float killTime;
	// Use this for initialization
	void Start ()
    {		
		grounded=false;
		pg = GameObject.FindGameObjectWithTag("Player");
        killed = false;
	}
	
	bool esplosioneAvvenuta=false;
	float timeAfterExplosion;
	// Update is called once per frame
	void Update () 
	{

        if (Time.time > killTime + 1.5f && killed)
        {
            print("sei qui");
            PlayScript.State = PlayScript.PlayState.result;
            Destroy(gameObject);
        }

        if (grounded && Time.time > explosiontime)
        {
            if (!esplosioneAvvenuta)
            {
                audio.loop = false;
                print("doverbbe apparire solo una volta");
                audio.PlayOneShot(esplosione);

                timeAfterExplosion = Time.time + 4.5f;
                if (Application.platform == RuntimePlatform.Android ||
                    Application.platform == RuntimePlatform.WP8Player
                    || Application.platform == RuntimePlatform.IPhonePlayer)
                    Instantiate(detonatorMobile, transform.position, Quaternion.identity);
                else
                    Instantiate(detonatorBello, transform.position, Quaternion.identity);

                //
                this.gameObject.renderer.active = false;
                //
                esplosioneAvvenuta = true;

                if (pg.transform.position.x < transform.position.x + 7 && pg.transform.position.x > transform.position.x - 7)
                {
                    Vibrate();

                    int random = Random.Range(0, 3);
                    switch (random)
                    {
                        case 0:
                            AudioSource.PlayClipAtPoint(morte1, transform.position);
                            break;
                        case 1:
                            AudioSource.PlayClipAtPoint(morte2, transform.position);
                            break;
                        case 2:
                            AudioSource.PlayClipAtPoint(morte3, transform.position);
                            break;
                    }
                    pg.SetActive(false);
                    //
                    killed = true;
                    killTime = Time.time;
                    //
                }
                else
                    Destroy(gameObject);
            }
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
