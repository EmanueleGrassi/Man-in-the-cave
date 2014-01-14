using UnityEngine;
using System.Collections;
using System;

public class MinSceIndicator_Script : MonoBehaviour
{
    float margin, height, height2;
    public GUISkin custom;
    void Start()
    {
        margin = Screen.width / 60;
        height2 = Screen.width / 10;
        height = Screen.width / 20;
    }
    void OnGUI()
    {
        if (GUI.skin != custom)
            GUI.skin = custom;
        TimeSpan t = (TimeSpan.FromSeconds(CameraScript.PlayTime));
        string TimeText;
        if (t.Minutes >= 1)
            TimeText = "min";
        else
            TimeText = "sec";
        GUI.skin.label.fontSize = (int)(height*0.75);
        var labelPositionSec = GUILayoutUtility.GetRect(new GUIContent(TimeText), GUI.skin.label);
        GUI.Label(new Rect(margin + ((height2 ) / 2), ((height2) / 2)*1.2f + (labelPositionSec.height / 2),
            labelPositionSec.width, labelPositionSec.height), TimeText);
    }
}
