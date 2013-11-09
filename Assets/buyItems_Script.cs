using UnityEngine;
using System.Collections;

public class buyItems_Script : MonoBehaviour {

    public Texture arcoLight, bengal, bluLight, greenLight, piccone, pinkLight, reborn, redLight;
    float size, margin;
    public GUISkin custom;
    // Use this for initialization
    void Start()
    {
        size = Screen.width / 20;
        margin = Screen.width / 60;

    }
    Vector2 position = Vector2.zero;
    // Update is called once per frame
    void OnGUI()
    {
        GUI.skin = custom;
        GUI.DrawTexture(new Rect(margin, margin * 13, size * 9, size * 3), piccone, ScaleMode.ScaleToFit, true);
        GUI.DrawTexture(new Rect(margin, margin * 13 + size * 3, size * 9, size * 3), reborn, ScaleMode.ScaleToFit, true);
        position = GUI.BeginScrollView(new Rect(size * 10, margin * 7, size * 10, size * 10), position, new Rect(0, 0, size * 10, size * 16));
        if (GUI.Button(new Rect(0, 0, size * 9, size * 3), redLight))
            return;
        if (GUI.Button(new Rect(0, margin / 2 + size * 3, size * 9, size * 3), bluLight))
            return;
        if (GUI.Button(new Rect(0, margin + size * 6, size * 9, size * 3), greenLight))
            return;
        if (GUI.Button(new Rect(0, margin + margin/2 + size * 9, size * 9, size * 3), pinkLight))
            return;
        if (GUI.Button(new Rect(0, margin * 2 + size * 12, size * 9, size * 3), arcoLight))
            return;
        GUI.EndScrollView(); 
       
    }
}
