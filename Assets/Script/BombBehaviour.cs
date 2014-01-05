using UnityEngine;
using System.Collections;

public class BombBehaviour : MonoBehaviour {
	public AudioClip miccia,esplosione,morte1,morte2,morte3;
	public Transform detonatorBello, detonatorMobile;
	float explosiontime;
	bool grounded;
    private GameObject pg;
    //public Transform pg;
    public bool killed;
    float killTime;
    bool esplosioneAvvenuta;
	// Use this for initialization
	void Start ()
    {		
		grounded=false;
        pg = GameObject.Find("Player");
        // pg = GameObject.FindGameObjectWithTag("Player");
        killed = false;
        esplosioneAvvenuta = false;
	}
	
	
    //float timeAfterExplosion;
	// Update is called once per frame
    //void Update () 
    //{
    //    //if (esplosioneAvvenuta && !audio.isPlaying)
    //    //{
    //    //    if (killed)
    //    //    {
    //    //        GameManager_script.spanForResult(true, CameraScript.PlayTime);
    //    //        CameraScript.ManageButton(false);
    //    //    }
    //    //    Destroy(gameObject);
    //    //}

    ////    //if (grounded && CameraScript.PlayTime > explosiontime && !esplosioneAvvenuta)
    ////    //{
    ////    //    explode();
    ////    //}
    //}
	void OnCollisionEnter(Collision collision) 
	{
		if(collision.gameObject.tag=="ground")
		{
            //explosiontime = CameraScript.PlayTime + Random.Range(2.5f,5.0f);
            grounded = true;
            print(Time.time);
            StartCoroutine(Bgrounded());
		}
        if (collision.gameObject.tag == "rock")
            explode();
        if (collision.gameObject.tag == "Player" && !grounded)
            explode();
	}

    IEnumerator Bgrounded()
    {
        yield return new WaitForSeconds(Random.Range(2.5f, 5.0f));
        //print("esegue qui");
        explode();
    }

    void explode()
    {
        audio.loop = false;
        audio.PlayOneShot(esplosione);

        //timeAfterExplosion = CameraScript.PlayTime + 4.5f;
#if (UNITY_METRO || UNITY_WP8 || UNITY_ANDROID ||UNITY_IPHONE)
           Instantiate(detonatorMobile, transform.position, Quaternion.identity);
#else
        Instantiate(detonatorBello, transform.position, Quaternion.identity);
#endif  
        //
        gameObject.renderer.enabled = false;
        gameObject.collider.enabled = false;
        
        //
        esplosioneAvvenuta = true;

        if (pg.transform.position.x < transform.position.x + 7 && pg.transform.position.x > transform.position.x - 7 && transform.position.y < 5)
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
            //killed = true;
            //killTime = CameraScript.PlayTime;
            StartCoroutine("killMitch");
            //
            print("" + killTime);
        }
    }

    IEnumerator killMitch()
    {
        CameraScript.ManageButton(false);
        //GameManager_script.spanForResult(true, CameraScript.PlayTime);
        yield return new WaitForSeconds(1.4f);
        if (CameraScript.replayGame)
            CameraScript.replayGame = false;
        PlayScript.State = PlayScript.PlayState.result;
    }

	void Vibrate()
	{	
        #if !UNITY_METRO
		        try {Handheld.Vibrate();} catch {}
        #endif
	}
}
