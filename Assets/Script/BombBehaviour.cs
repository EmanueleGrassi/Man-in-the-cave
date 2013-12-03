using UnityEngine;
using System.Collections;

public class BombBehaviour : MonoBehaviour {
	public AudioClip miccia,esplosione,morte1,morte2,morte3;
	public Transform detonatorBello, detonatorMobile;
	float explosiontime;
	bool grounded;
    GameObject pg;
    //public Transform pg;
    bool killed;
    float killTime;
    bool esplosioneAvvenuta;
	// Use this for initialization
	void Start ()
    {		
		grounded=false;
        pg = GameObject.FindGameObjectWithTag("Player");
        killed = false;
        esplosioneAvvenuta = false;
	}
	
	
    //float timeAfterExplosion;
	// Update is called once per frame
	void Update () 
	{
		
		if(esplosioneAvvenuta && !audio.isPlaying){
	        if (killed)
	        {
	            print("sei qui");
	            PlayScript.State = PlayScript.PlayState.result;
	            print("siamo in result");
	        }
			Destroy(gameObject);
		}

        if (grounded && CameraScript.PlayTime > explosiontime && !esplosioneAvvenuta)
        {
            
            audio.loop = false;                
            audio.PlayOneShot(esplosione);

            //timeAfterExplosion = CameraScript.PlayTime + 4.5f;
            if (Application.platform == RuntimePlatform.Android ||
                Application.platform == RuntimePlatform.WP8Player
                || Application.platform == RuntimePlatform.IPhonePlayer)
                Instantiate(detonatorMobile, transform.position, Quaternion.identity);
            else
                Instantiate(detonatorBello, transform.position, Quaternion.identity);

            //
            gameObject.renderer.enabled = false;
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
                print("disattivato");
                //
                killed = true;
                killTime = CameraScript.PlayTime;
                //
                print("" + killTime);
			}
         }
	}
	void OnCollisionEnter(Collision collision) 
	{
		if(collision.gameObject.tag=="ground")
		{
			explosiontime = CameraScript.PlayTime + Random.Range(2.5f,5.0f);
			grounded=true;
		}
	}

	void Vibrate()
	{		
		try {Handheld.Vibrate();} catch {}
	}
}
