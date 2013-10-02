using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

    public static bool isPaused;

    // Use this for initialization
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touches.Length > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (touch.phase == TouchPhase.Began)
                    if (Physics.Raycast(ray, out hit, 100) && hit.collider.tag == "Pause")
                    {
                        if (isPaused)
                        {
                            Time.timeScale = 1;
                            isPaused = false;
                        }
                        else
                        {
                            Time.timeScale = 0;
                            isPaused = true;
                        }
                    }
            }
        }
    }
}
