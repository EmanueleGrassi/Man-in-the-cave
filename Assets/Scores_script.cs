using UnityEngine;
using System.Collections;
using System;

public class Scores_script : MonoBehaviour {

    float margin, unit;
    public GUISkin custom;
    Vector2 pos;
    bool goBack;
    public Texture back;
    float scrollparam;
    public Texture2D thumb;

	// Use this for initialization
	void Start () {
        unit = Screen.width / 20;
        margin = Screen.width / 60;
        pos = Vector2.zero;
        goBack = false;
        scrollparam = (Screen.height * 2) / 768;
#if UNITY_METRO
        if (Input.touchCount == 0)
        {
            bool bar = true;
        }
#endif
	}
	
	// Update is called once per frame
	void OnGUI () {
        GUI.skin = custom;
        #if UNITY_METRO
            custom.verticalScrollbarThumb.normal.background = thumb;
        #endif
        GUI.skin.label.fontSize = (int)(unit * 1.5f);
        if (GUI.Button(new Rect(margin * 2, unit, unit * 1f, unit * 1f), back))
            Application.LoadLevel(0);
        GUI.Label(new Rect(margin*3 + unit, margin, unit * 11, unit * 3 + margin), "Highscores");
        GUI.skin.label.fontSize = (int)(unit * 0.7f);
        int i = 0;
        pos = GUI.BeginScrollView(new Rect(margin*3 + unit, unit * 3, Screen.width-(margin*3 + unit), unit * 12), pos, new Rect(0, 0, Screen.width, unit * 30));
        for (i = 0; i < 20; i++)
        {
            if (CameraScript.data.Records[i].x == 0)
            {
                break;
            }
            if (i==0)
            {
                GUI.skin.label.normal.textColor = new Color(246, 193, 0);
                GUI.skin.label.fontSize = (int)(unit * 1f);
               
            }
            else if (i==1)
            {
              
                GUI.skin.label.normal.textColor = new Color(192, 192, 192);
                GUI.skin.label.fontSize = (int)(unit * 1f);
            }
            else if (i==2)
            {
                GUI.skin.label.normal.textColor = new Color(205, 127, 50);
                GUI.skin.label.fontSize = (int)(unit * 1f);
                
            }
            else 
            {
                GUI.skin.label.fontSize = (int)(unit*0.6);
                GUI.skin.label.normal.textColor = Color.white;     
            }
           
            GUI.Label(new Rect(0, (i * (unit*1.3f)), unit * 8, unit*1.5f), formatScore(CameraScript.data.Records[i].x));
            GUI.Label(new Rect(unit * 7, (i * (unit * 1.3f)), unit * 10, unit * 1.5f), CameraScript.data.Records[i].y +
                "/" + CameraScript.data.Records[i].width + "/" + CameraScript.data.Records[i].height);
        }
        GUI.EndScrollView();
        GUI.skin.label.normal.textColor = Color.white;
        GUI.skin.label.fontSize = (int)(unit * 0.6);
	}

    
    void Update()
    {
        if (Input.touchCount == 0)
            return;

        if (Input.GetKey(KeyCode.Escape))
            goBack = true;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            bool fInsideList = IsTouchInsideList(touch.position);

            if (touch.phase == TouchPhase.Moved && fInsideList)
            {
                pos.y += touch.deltaPosition.y * scrollparam;
            }
        }
        if (goBack)
            Application.LoadLevel(0);
        
    }

    public static string formatScore(float a)
    {
        int min = (int)a / 60;
        int seconds = (int)a - min * 60;

        return (String.Format("{0:00}", min) + ":" + String.Format("{0:00}", seconds) );
    }

    bool IsTouchInsideList(Vector2 touchPos)
    {
        Vector2 screenPos = new Vector2(touchPos.x, touchPos.y);
        Rect rAdjustedBounds = new Rect(unit * 6, unit * 4, unit * 18, unit * 12);

        return rAdjustedBounds.Contains(screenPos);
    }
}
