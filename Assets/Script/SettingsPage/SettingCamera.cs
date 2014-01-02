using UnityEngine;
using System.Collections;

public class SettingCamera : MonoBehaviour {

    float margin, titleWidth, titleHeight, barSize;
    public GUISkin custom;
    public Texture back, title;
    #if UNITY_METRO
        float istructionWidth,istructionHeight;
        public Texture  istruction;
        Rect labelPosition;
    #endif
    float UnTerzo;
    void Start()
    {
        titleWidth = Screen.width * 0.7f;
        titleHeight = titleWidth * 233 / 1024;
        margin = Screen.width / 60;
        UnTerzo = Screen.height / 3;
        barSize = (Screen.width * 81 / 1024);
        #if UNITY_METRO
            istructionWidth = Screen.width * 0.6f;
            istructionHeight = istructionWidth * 115 / 488;
        #endif

        
    }
	
	// Update is called once per frame
    void OnGUI()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.LoadLevel(0);
        GUI.skin = custom;
        if (GUI.Button(new Rect(margin, margin / 3, ((UnTerzo / 3) * 168) / 141, UnTerzo / 3), back))
        {
            Application.LoadLevel(0);
        }
        GUI.skin.label.fontSize = Screen.width / 30;
        GUI.DrawTexture(new Rect(Screen.width / 2 - (titleWidth) / 2, barSize, titleWidth, titleHeight), title);
        labelPosition = GUILayoutUtility.GetRect(new GUIContent("a celialab game. http://celialab.com/"), custom.label);
        GUI.Label(new Rect(Screen.width / 2 - labelPosition.width / 2, barSize + titleHeight,
            labelPosition.width, labelPosition.height), "a celialab game. http://celialab.com/");

        #if UNITY_METRO
                GUI.DrawTexture(new Rect(Screen.width / 2 - (istructionWidth) / 2, Screen.height - (istructionHeight),
                    istructionWidth, istructionHeight), istruction);
      
        #endif
       

    }
}
