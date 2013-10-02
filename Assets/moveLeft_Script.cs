using UnityEngine;
using System.Collections;

public class moveLeft_Script : MonoBehaviour {

    public Transform player;
    bool locked;
    int finger;

	// Use this for initialization
	void Start () {
        locked = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touches.Length > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (touch.phase == TouchPhase.Began && !locked)
                {
                    finger = touch.fingerId;
                    locked = true;
                }

                if (Physics.Raycast(ray, out hit, 1000) && hit.collider.tag == "mLeft" && touch.fingerId == finger)
                {
                    if (!pg_Script.isJumping)
                        player.rigidbody.AddForce(new Vector3(-3f, 0, 0), ForceMode.VelocityChange);
                    else
                        player.rigidbody.AddForce(new Vector3(-1f, 0, 0), ForceMode.VelocityChange);
                }
                if (touch.phase == TouchPhase.Ended && touch.fingerId == finger)
                {
                    locked = false;
                }
            }
        }
	}
}
