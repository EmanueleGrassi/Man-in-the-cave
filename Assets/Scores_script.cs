using UnityEngine;
using System.Collections;

public class Scores_script : MonoBehaviour {

    float margin, unit;
    public GUISkin custom;
    int deflab;
    Vector2 pos;
    bool goBack;
    public Texture back;

	// Use this for initialization
	void Start () {
        unit = Screen.width / 20;
        margin = Screen.width / 60;
        pos = Vector2.zero;
        goBack = false;
	}
	
	// Update is called once per frame
    //E' BRUTTO, MA NON METTEDE IL DITO NELLA PIAGA PLIS, ABBIATE FEDE LE COSE STANNO PER CAMBIARE (CIT.)
	void OnGUI () {
        GUI.skin = custom;
        GUI.skin.label.fontSize = (int)(unit * 2.2f);
        if (GUI.Button(new Rect(margin * 2, unit, unit * 1.5f, unit * 1.5f), back))
            Application.LoadLevel(0);
        GUI.Label(new Rect(unit*6-3*margin, margin, unit*11, unit*3+margin), "Highscores");
        GUI.skin.label.fontSize = (int)(unit * 0.7f);
        int i = 0;
        pos = GUI.BeginScrollView(new Rect(unit * 6, unit * 4, unit * 18, unit * 12), pos, new Rect(0, 0, unit * 18, unit * 30));
        for (i = 0; i < 20; i++)
        {
            float multmargin = 0;
            if (i==0)
            {
                GUI.skin.label.normal.textColor = Color.yellow;
                GUI.skin.label.fontSize = (int)(unit * 1f);
                multmargin = 2.5f;
            }
            else if (i==1)
            {
                multmargin = 0.7f;
                GUI.skin.label.normal.textColor = Color.gray;
                GUI.skin.label.fontSize = (int)(unit * 0.9f);
            }
            else if (i==2)
            {
                GUI.skin.label.normal.textColor = Color.blue;
                GUI.skin.label.fontSize = (int)(unit * 0.8f);
                multmargin = 0.5f;
            }
            else 
            {
                GUI.skin.label.fontSize = deflab;
                GUI.skin.label.normal.textColor = Color.white;     
            }
            GUI.Label(new Rect(unit*2-3*margin-multmargin*margin, (i * (unit*1.3f)), unit * 18, unit*1.5f), (i + 1) + ". "+ CameraScript.data.Records[i].left + "     " +
                CameraScript.data.Records[i].top + "/" + CameraScript.data.Records[i].width + "/" + CameraScript.data.Records[i].height);
        }
        GUI.EndScrollView();
        GUI.skin.label.normal.textColor = Color.white;
        GUI.skin.label.fontSize = deflab;
	}



    //NON FUNZIONA, CHIUNQUE LO SA FIXARE E' IL BENVENUTO
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            goBack = true;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            bool fInsideList = IsTouchInsideList(touch.position);

            if (touch.phase == TouchPhase.Moved && fInsideList)
            {
                pos.y += touch.deltaPosition.y * 2;
            }
        }
        if (goBack)
            Application.LoadLevel(0);
        
    }

    bool IsTouchInsideList(Vector2 touchPos)
    {
        Vector2 screenPos = new Vector2(touchPos.x, touchPos.y);
        Rect rAdjustedBounds = new Rect(unit * 6, unit * 4, unit * 18, unit * 12);

        return rAdjustedBounds.Contains(screenPos);
    }
}
