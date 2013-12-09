using UnityEngine;
using System.Collections;

public class UrloScript : MonoBehaviour
{

    public AudioClip[] shouts;
    float nextshout;
    public static bool playShout, canPlay;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextshout)
            canPlay = true;
        if (playShout)
        {
            playShout = false;
            if (Random.Range(0, 2) == 0)
            {
                canPlay = false;
                nextshout = Time.time + 4f;
                PlayScript.PlayClip(this.audio, shouts);
            }
        }
    }
}
