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
	void OnGUI () {
        deflab = GUI.skin.label.fontSize;
        GUI.skin = custom;
        GUI.skin.label.fontSize = (int)(unit * 1.8f);
        GUI.Label(new Rect(unit*6-margin, unit, unit*9, unit*2+margin), "Highscores");
        GUI.skin.label.fontSize = deflab;
        int i;
        pos = GUI.BeginScrollView(new Rect(unit * 6, unit * 4, unit * 18, unit * 12), Vector2.zero, new Rect(0, 0, unit * 18, unit * 30));
        for (i = 0; i <= 20; i++)
        {
            switch (i)
            {
                case 0:
                    GUI.skin.label.fontSize *= 2;
                    GUI.skin.label.normal.textColor = Color.yellow;
                    break;
                case 1:
                    GUI.skin.label.fontSize =(int)((float)GUI.skin.label.fontSize*1.7f);
                    GUI.skin.label.normal.textColor = Color.gray;
                    break;
                case 2:
                    GUI.skin.label.fontSize =(int)((float)GUI.skin.label.fontSize*1.3f);
                    GUI.skin.label.normal.textColor = Color.blue;
                    break;
                default:
                    GUI.skin.label.fontSize = deflab;
                    GUI.skin.label.normal.textColor = Color.white;
                    break;
            }
            GUI.Label(new Rect(0, (i * unit), unit * 18, unit), (i + 1) + ". 22:22   23/45/1992");
        }
        GUI.EndScrollView();
	}

    void Update()
    {
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
