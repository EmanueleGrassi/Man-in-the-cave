using UnityEngine;
using System.Collections;

public class Scores_script : MonoBehaviour {

    float margin, unit;
    public GUISkin custom;
    int deflab;
    Vector2 pos;

	// Use this for initialization
	void Start () {
        unit = Screen.width / 20;
        margin = Screen.width / 60;
        pos = Vector2.zero;
	}
	
	// Update is called once per frame
    //E' BRUTTO, MA NON METTEDE IL DITO NELLA PIAGA PLIS, ABBIATE FEDE LE COSE STANNO PER CAMBIARE (CIT.)
	void OnGUI () {
        deflab = GUI.skin.label.fontSize;
        GUI.skin = custom;
        GUI.skin.label.fontSize = (int)(unit * 1.8f);
        GUI.Label(new Rect(unit*6-margin, unit, unit*9, unit*2+2*margin), "Highscores");
        GUI.skin.label.fontSize = (int)(unit * 0.7f);
        int i;
        pos = GUI.BeginScrollView(new Rect(unit * 6, unit * 4, unit * 18, unit * 12), Vector2.zero, new Rect(0, 0, unit * 18, unit * 30));
        for (i = 0; i < 20; i++)
        {
            float multmargin = 0;
            if (i==0)
            {
                GUI.skin.label.normal.textColor = Color.yellow;
                multmargin = 1f;
            }
            else if (i==1)
            {
                multmargin = 0.7f;
                GUI.skin.label.normal.textColor = Color.gray;
            }
            else if (i==2)
            {
                GUI.skin.label.normal.textColor = Color.blue;
                multmargin = 0.5f;
            }
            else 
            {
                GUI.skin.label.fontSize = deflab;
                GUI.skin.label.normal.textColor = Color.white;     
            }
            GUI.Label(new Rect(unit*2-2*margin-multmargin*margin, (i * unit), unit * 18, unit), (i + 1) + ". "+ CameraScript.data.Records[i].left + "     " +
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
            Application.LoadLevel(0);
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            bool fInsideList = IsTouchInsideList(touch.position);

            if (touch.phase == TouchPhase.Moved && fInsideList)
            {
                pos.y += touch.deltaPosition.y * 2;
            }
        }
    }

    bool IsTouchInsideList(Vector2 touchPos)
    {
        Vector2 screenPos = new Vector2(touchPos.x, touchPos.y);
        Rect rAdjustedBounds = new Rect(unit * 6, unit * 4, unit * 18, unit * 12);

        return rAdjustedBounds.Contains(screenPos);
    }
}
