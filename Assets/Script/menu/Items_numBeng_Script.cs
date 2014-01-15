using UnityEngine;
using System.Collections;

public class Items_numBeng_Script : MonoBehaviour
{

    public GUISkin custom;
    float size, margin, UnTerzo;
    public Texture piccone, bengala, reborn;
    void Start()
    {
        size = Screen.width / 20;
        margin = Screen.width / 60;
        UnTerzo = Screen.height / 3;
        custom.label.fontSize = (int)(size * 0.8f);
    }

    
    void OnGUI()
    {
        if (GUI.skin != custom)
            GUI.skin = custom;
        Rect labelPosition = GUILayoutUtility.GetRect(new GUIContent(CameraScript.data.NumBengala.ToString()), custom.label);
        Rect label2Position = GUILayoutUtility.GetRect(new GUIContent(CameraScript.data.NumberReborn.ToString()), custom.label);

        float firstPosition = Screen.width - (size * 2 + labelPosition.width + label2Position.width + margin * 4);
        GUI.DrawTexture(new Rect(firstPosition, UnTerzo / 6 - size / 2, size, size), bengala, ScaleMode.ScaleToFit, true);
        firstPosition += margin + size;
        GUI.Label(new Rect(firstPosition, UnTerzo / 6 - labelPosition.height / 2, labelPosition.width, labelPosition.height),
                                                                                          CameraScript.data.NumBengala.ToString());
        firstPosition += margin + labelPosition.width;
        GUI.DrawTexture(new Rect(firstPosition, UnTerzo / 6 - size / 2, size, size), reborn, ScaleMode.ScaleToFit, true);
        firstPosition += margin + size;
        GUI.Label(new Rect(firstPosition, UnTerzo / 6 - labelPosition.height / 2, labelPosition.width, labelPosition.height),
                                                                                        CameraScript.data.NumberReborn.ToString());
    }
}
