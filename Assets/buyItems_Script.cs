using UnityEngine;
using System.Collections;
using System;

public class buyItems_Script : MonoBehaviour {

    public Texture arcoLight, bengal, bluLight, greenLight, piccone, pinkLight, reborn, redLight, coins;
    float size, margin;
    public GUISkin custom;
    public static event EventHandler plus500, plus1000, plus5000;
    // Use this for initialization
    void Start()
    {
        size = Screen.width / 20;
        margin = Screen.width / 60;
    }
    Vector2 position = Vector2.zero;

    void OnGUI()
    {
		if(Input.GetKey(KeyCode.Escape))
			Application.LoadLevel(0);
        GUI.skin = custom;
        custom.label.normal.textColor = new Color(255, 255, 255);
        custom.label.fontSize = (int)(size * 1.5);
        GUI.Label(new Rect(margin, margin, size * 10, size * 2 + margin), "Buy Items"); // titolo
        custom.label.fontSize = (int)(size * 0.5f);
        //reborn e picconi
        GUI.Label(new Rect(margin, margin * 11 - 3, size * 6, size), "n Bengal: " + CameraScript.data.numBengala);
        GUI.Label(new Rect(size * 5, margin * 11 - 3, size * 6, size), "n Reborn: " + CameraScript.data.NumberReborn);
        //coins
        GUI.DrawTexture(new Rect(size * 10 , margin*2, size, size), coins);
        GUI.Label(new Rect(size * 11+ margin, margin * 2, size*2, size), "" + CameraScript.data.points);
        //purchases
        custom.button.normal.textColor = new Color(170,92,0);
        custom.button.fontSize = (int)(size * 0.5f);
        if (GUI.Button(new Rect(size * 14, margin * 2, size + margin, size), "+500"))
            if (plus500 != null)
                plus500(this, new EventArgs());
        custom.button.normal.textColor = new Color(164, 164, 164);
        custom.button.fontSize = (int)(size * 0.5f)+1;
        if (GUI.Button(new Rect(size * 16-margin/2, margin *2, size+margin, size), "+1000"))
            if (plus1000 != null)
                plus1000(this, new EventArgs());
        custom.button.normal.textColor = new Color(234, 202, 0);
        custom.button.fontSize = (int)(size * 0.5f) + 2;
        if (GUI.Button(new Rect(size * 18-margin, margin * 2, size + 2*margin, size), "+5000"))
            if (plus5000 != null)
                plus5000(this, new EventArgs());
        //colonna di sinistra
        if (GUI.Button(new Rect(margin, margin * 13, size * 9, size * 3), bengal))
            if (CameraScript.data.points >= 90)
            {
                CameraScript.data.points -= 90;
                CameraScript.data.numBengala += 2;
            }
        if (GUI.Button(new Rect(margin, margin * 12 + size * 3, size * 9, size * 3), reborn))
            if (CameraScript.data.points >= 300)
            {
                CameraScript.data.points -= 300;
                CameraScript.data.NumberReborn++;
            }
        //scrollview
        position = GUI.BeginScrollView(new Rect(size * 10, margin * 7, size * 10, size * 10), position, new Rect(0, 0, size * 10, size * 14));
        if (GUI.Button(new Rect(0, 0, size * 9, size * 3), redLight) && !CameraScript.data.lightRed)
        {
            if (CameraScript.data.points >= 500 && !CameraScript.data.lightRed)
            {
                CameraScript.data.points -= 500;
                CameraScript.data.lightRed = true;
                CameraScript.data.helmet = Helmet.red;
                //SUONA CASSA
            }
        }
        if (GUI.Button(new Rect(0, size * 2 + margin*2, size * 9, size * 3), bluLight) && !CameraScript.data.lightBlue)
            if (CameraScript.data.points >= 550 && !CameraScript.data.lightRed)
            {
                CameraScript.data.points -= 550;
                CameraScript.data.lightRed = true;
                CameraScript.data.helmet = Helmet.red;
                //SUONA CASSA
            }
        if (GUI.Button(new Rect(0, margin*4 + size * 4, size * 9, size * 3), greenLight) && !CameraScript.data.lightGreen)
            if (CameraScript.data.points >= 750 && !CameraScript.data.lightRed)
            {
                CameraScript.data.points -= 750;
                CameraScript.data.lightRed = true;
                CameraScript.data.helmet = Helmet.red;
                //SUONA CASSA
            }
        if (GUI.Button(new Rect(0, margin *6 + size * 6, size * 9, size * 3), pinkLight) && !CameraScript.data.lightPink)
            if (CameraScript.data.points >= 800 && !CameraScript.data.lightRed)
            {
                CameraScript.data.points -= 800;
                CameraScript.data.lightRed = true;
                CameraScript.data.helmet = Helmet.red;
                //SUONA CASSA
            }
        if (GUI.Button(new Rect(0, margin*8 + size * 8, size * 9, size * 3), arcoLight) && !CameraScript.data.lightRainbow)
            if (CameraScript.data.points >= 6000 && !CameraScript.data.lightRed)
            {
                CameraScript.data.points -= 6000;
                CameraScript.data.lightRed = true;
                CameraScript.data.helmet = Helmet.red;
                //SUONA CASSA
            }
        GUI.EndScrollView(); 
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            bool fInsideList = IsTouchInsideList(touch.position);
            print(fInsideList);

            if (touch.phase == TouchPhase.Moved && fInsideList)
            {
                position.y += touch.deltaPosition.y * 5;
            }
        }
    }

    bool IsTouchInsideList(Vector2 touchPos)
    {
        Vector2 screenPos = new Vector2(touchPos.x, touchPos.y);
        print("" + screenPos.x + " " + screenPos.y);
        Rect rAdjustedBounds = new Rect(size * 10, margin * 7, size * 10, size * 16);

        return rAdjustedBounds.Contains(screenPos);
    }

}
