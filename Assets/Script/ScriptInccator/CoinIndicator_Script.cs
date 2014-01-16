using UnityEngine;
using System.Collections;

public class CoinIndicator_Script : MonoBehaviour
{
    float margin, height2;
    int fontSize;
    public GUISkin custom;
    void Start()
    {
        margin = Screen.width / 60;
        height2 = Screen.width / 10;
        fontSize = (int)((Screen.width / 20) * 0.4);
    }
    void OnGUI()
    {
        if (PlayScript.State == PlayScript.PlayState.play)
        {
            if (GUI.skin != custom)
                GUI.skin = custom;
            GUI.depth = 0;
            GUI.skin.label.fontSize = fontSize;
            var labelPositionSec = GUILayoutUtility.GetRect(new GUIContent(PlayScript.GameCredits.ToString()), GUI.skin.label);
            GUI.Label(new Rect(margin + ((height2 - labelPositionSec.width) / 2), height2 * 1.47f,
                labelPositionSec.width, labelPositionSec.height), PlayScript.GameCredits.ToString());
        }
    }
}