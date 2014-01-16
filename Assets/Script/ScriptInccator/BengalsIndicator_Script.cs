using UnityEngine;
using System.Collections;

public class BengalsIndicator_Script : MonoBehaviour
{
    float margin, height2, height;
    int fontSize;
    public GUISkin custom;
    void Start()
    {
        margin = Screen.width / 60;
        fontSize = (int)((Screen.width / 20) * 0.4);
        height2 = Screen.width / 10;
    }
    void OnGUI()
    {
        if (PlayScript.State == PlayScript.PlayState.play)
        {
            if (GUI.skin != custom)
                GUI.skin = custom;

            GUI.depth = 0;
            GUI.skin.label.fontSize = fontSize;
            var labelPositionSec = GUILayoutUtility.GetRect(new GUIContent(CameraScript.data.NumBengala.ToString()), GUI.skin.label);
            GUI.Label(new Rect(margin + ((height2 - labelPositionSec.width) / 2), height2 * 1.67f,
                labelPositionSec.width, labelPositionSec.height), CameraScript.data.NumBengala.ToString());
        }
    }
}