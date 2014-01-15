using UnityEngine;
using System.Collections;
using System;

public class Scores_script : MonoBehaviour
{

    float margin, size, achiveSize, barSize;
    public GUISkin custom;
    Vector2 pos;
    public Texture back, scores, scoresPressed, achivements, achivementsPressed;
    public Texture jumpAchiv, bengalAchiv, moneyAchiv, timeAchiv;
    float scrollparam;
    public Texture2D thumb;
    float UnTerzo;
    bool IsScore = true; //se false visualizza achivements

    void Start()
    {
        size = Screen.width / 20;      
        margin = Screen.width / 60;
        UnTerzo = Screen.height / 3;
        barSize = (Screen.width * 81 / 1024);
        achiveSize = (Screen.height - barSize) / 2.15f;//   UnTerzo + 0.6f;
        pos = Vector2.zero;
        scrollparam = (Screen.height * 2) / 768;
        #if UNITY_METRO
                custom.verticalScrollbarThumb.normal.background = thumb;
        #endif   
                CameraScript.LoadData();
    }

    
    void OnGUI()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.LoadLevel(0);
        GUI.skin = custom;
        if (GUI.Button(new Rect(margin, margin / 3, ((UnTerzo / 3) * 168) / 141, UnTerzo / 3), back))
        {
            Application.LoadLevel(0);
        }
        if (GUI.Button(new Rect(margin * 2 + ((UnTerzo / 3) * 168) / 141, margin / 3, ((UnTerzo / 3) * 500) / 141, UnTerzo / 3), IsScore?scoresPressed:scores))
        {
            IsScore = true;
        }
        if (GUI.Button(new Rect(margin * 3 + ((UnTerzo / 3) * 168) / 141 + ((UnTerzo / 3) * 500) / 141,
            margin / 3, ((UnTerzo / 3) * 550) / 141, UnTerzo / 3), IsScore ? achivements : achivementsPressed))
        {
            IsScore = false;
        }

        if (IsScore)
            DrawScore();
        else
            DrawAchiv();//visualizza gli achiv
    }
    void DrawScore()
    { 
        GUI.skin.label.fontSize = (int)(size * 0.7f);
        if (CameraScript.data.Records.Count != 0)
        {
            int i = 0;
            int n = CameraScript.data.Records.Count;
            pos = GUI.BeginScrollView(new Rect(margin * 3 + size, barSize, Screen.width - (margin * 3 + size), Screen.height - barSize),
                pos, new Rect(0, 0, Screen.width, size * 2 + size * n * 1.5f));
            for (i = 0; i < 20; i++)
            {                
                if (i == 0)
                {
                    GUI.skin.label.normal.textColor = new Color(246, 193, 0);
                    GUI.skin.label.fontSize = (int)(size * 1f);
                }
                else if (i == 1)
                {
                    GUI.skin.label.normal.textColor = new Color(192, 192, 192);
                    GUI.skin.label.fontSize = (int)(size * 1f);
                }
                else if (i == 2)
                {
                    GUI.skin.label.normal.textColor = new Color(205, 127, 50);
                    GUI.skin.label.fontSize = (int)(size * 1f);
                }
                else
                {
                    GUI.skin.label.fontSize = (int)(size * 0.6);
                    GUI.skin.label.normal.textColor = Color.white;
                }
                GUI.Label(new Rect(0, (i * (size * 1.3f)), size * 8, size * 1.5f), formatScoreSecond(CameraScript.data.Records[i].Seconds));
                GUI.Label(new Rect(0, (i * (size * 1.3f)), size * 8, size * 1.5f), formatScoreSecond(CameraScript.data.Records[i].Points));
                GUI.Label(new Rect(size * 7, (i * (size * 1.3f)), size * 10, size * 1.5f), CameraScript.data.Records[i].Day +
                    "/" + CameraScript.data.Records[i].Month + "/" + CameraScript.data.Records[i].Year);
            }
            GUI.EndScrollView();
        }
    }

    void DrawAchiv()
    {
        GUILayout.BeginArea(new Rect(margin, barSize+margin, Screen.width, Screen.height - barSize));
        GUILayout.BeginVertical(); 
            GUILayout.BeginHorizontal();           
                GUILayout.Label(bengalAchiv, GUILayout.Width(achiveSize), GUILayout.Height(achiveSize));
                GUILayout.Label(timeAchiv, GUILayout.Width(achiveSize), GUILayout.Height(achiveSize));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
                GUILayout.Label(moneyAchiv, GUILayout.Width(achiveSize), GUILayout.Height(achiveSize));
                GUILayout.Label(jumpAchiv, GUILayout.Width(achiveSize), GUILayout.Height(achiveSize));
            GUILayout.EndHorizontal(); 

        GUILayout.EndVertical();        
        GUILayout.EndArea();

        //GUI.DrawTexture(new Rect(Screen.width / 4 - achiveSize / 2, barSize, achiveSize, achiveSize), bengalAchiv);
        //GUI.DrawTexture(new Rect(Screen.width / 4 - achiveSize / 2, barSize + achiveSize, achiveSize, achiveSize), moneyAchiv);
    }

    void Update()
    {
       #if UNITY_METRO
            if (CameraScript.IsTouch)
            {
                Touch touch = Input.touches[0];
                bool fInsideList = IsTouchInsideList(touch.position);

                if (touch.phase == TouchPhase.Moved && fInsideList)
                {
                    pos.y += touch.deltaPosition.y * scrollparam;
                }
            }
        #endif
    }

    public static string formatScoreSecond(float a)
    {
        TimeSpan t = TimeSpan.FromSeconds(a);
        if(t.Minutes>0)
            return (String.Format("{0:0}:{1:00} min", t.Minutes, t.Seconds));
        else
            return (String.Format("{0:#0} sec", t.Seconds));
    }

    bool IsTouchInsideList(Vector2 touchPos)
    {
        Vector2 screenPos = new Vector2(touchPos.x, touchPos.y);
        Rect rAdjustedBounds = new Rect(size * 6, size * 4, size * 18, size * 12);

        return rAdjustedBounds.Contains(screenPos);
    }
}
