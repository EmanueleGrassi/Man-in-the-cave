using UnityEngine;
using System.Collections;
using System;

public class Items_Script : MonoBehaviour
{
    float size, margin;
    public Texture blueligth, pinkligth, redligth, greenligth, ranbowligth, white, buyButton;
    public GUISkin custom;
    int availableLights;
    bool draw, impressing;
    public AudioClip equipSound;
    public Texture back;
    float elementSize, positionYButtons;
    float UnTerzo;
    Vector2 position = Vector2.zero;
    float scrollparam;
    // Use this for initialization
    void Start()
    {
        CameraScript.LoadData();
        size = Screen.width / 20;
        margin = Screen.width / 60;
        UnTerzo = Screen.height / 3;
        elementSize = UnTerzo;
        positionYButtons = UnTerzo;

        scrollparam = (Screen.width * 2) / 768;
        impressing = false;

        availableLights = 0;
        custom.label.fontSize = (int)(size);
        custom.button.fontSize = (int)(size / 2);
        custom.button.normal.textColor = Color.white;
        switch (CameraScript.data.helmet)
        {

            case Helmet.red: position = new Vector2(elementSize,0);
                break;
            case Helmet.blue: position = new Vector2(elementSize * 2,0);
                break;
            case Helmet.green: position = new Vector2(elementSize * 3,0);
                break;
            case Helmet.pink: position = new Vector2(elementSize * 4,0);
                break;
            case Helmet.rainbow: position = new Vector2(elementSize * 5,0);
                break;
            default:
                break;
        }
        //test
        //CameraScript.LoadData();
        //CameraScript.data.lightRed = CameraScript.data.lightBlue = CameraScript.data.lightGreen = CameraScript.data.lightPink = CameraScript.data.lightRainbow = true;
    }

    void OnGUI()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            CameraScript.SaveData();
            Application.LoadLevel(0);
        }

        if (GUI.skin != custom)
            GUI.skin = custom;

        if (GUI.Button(new Rect(margin, margin / 3, ((UnTerzo / 3) * 168) / 141, UnTerzo / 3), back))
        {
            CameraScript.SaveData();
            Application.LoadLevel(0);
        }
        Rect labelPosition = GUILayoutUtility.GetRect(new GUIContent("Items"), custom.label);
        GUI.Label(new Rect(margin * 2 + ((UnTerzo / 3) * 168) / 141, UnTerzo / 6 - labelPosition.height / 2, labelPosition.width, labelPosition.height), "Items");

        drawElements();
    }

    //float getSize(Helmet e)
    //{
    //    if(e==CameraScript.data.helmet)
    //        sele
    //}
    bool trovatoSetected = false;

    void drawElements()
    {
        float selected = elementSize * 1.5f;
        float difference = (selected - elementSize);

        //scrollview
        int elem = 0;
        position = GUI.BeginScrollView(new Rect(0, UnTerzo / 3, Screen.width, Screen.height - (UnTerzo / 3)), position,
                                       new Rect(0, 0, (6 * elementSize) + (margin * 5) + (Screen.width / 2 - elementSize / 2) * 2, Screen.height - (UnTerzo / 3)), true, false);
        availableLights = 0;
        trovatoSetected = false;
        if (CameraScript.data.helmet == Helmet.white)
        {
            if (GUI.Button(new Rect((Screen.width / 2 - selected / 2) + availableLights * (elementSize + margin) + difference,
                positionYButtons - selected / 4, selected, selected), white))
            {
                //CameraScript.data.helmet = Helmet.white;
                //audio.PlayOneShot(equipSound);
            }
            trovatoSetected = true;
        }
        else
        {
            if (GUI.Button(new Rect((Screen.width / 2 - elementSize / 2) + availableLights * (elementSize + margin) + (trovatoSetected ? difference * 2 : 0),
                positionYButtons, elementSize, elementSize), white))
            {
                selectElement(Helmet.white);
            }
        }
        availableLights++;

        if (CameraScript.data.lightRed)
        {
            if (CameraScript.data.helmet == Helmet.red)
            {
                if (GUI.Button(new Rect((Screen.width / 2 - selected / 2) + availableLights * (elementSize + margin) + difference,
                    positionYButtons - selected / 4, selected, selected), redligth))
                {
                    //CameraScript.data.helmet = Helmet.red;
                    //audio.PlayOneShot(equipSound);
                }
                trovatoSetected = true;
            }
            else
            {
                if (GUI.Button(new Rect((Screen.width / 2 - elementSize / 2) + margin + availableLights * (elementSize + margin) + (trovatoSetected ? difference * 2 : 0),
                    positionYButtons, elementSize, elementSize), redligth))
                {
                    selectElement(Helmet.red);
                }
            }
            availableLights++;
        }

        if (CameraScript.data.lightBlue)
        {
            if (CameraScript.data.helmet == Helmet.blue)
            {
                if (GUI.Button(new Rect((Screen.width / 2 - selected / 2) + availableLights * (elementSize + margin) + difference,
                    positionYButtons - selected / 4, selected, selected), blueligth))
                {
                    //CameraScript.data.helmet = Helmet.blue;
                    //audio.PlayOneShot(equipSound);
                }
                trovatoSetected = true;
            }
            else
            {
                //print("blue: " + trovatoSetected + "   " + difference);
                if (GUI.Button(new Rect((Screen.width / 2 - elementSize / 2) + margin + availableLights * (elementSize + margin) + (trovatoSetected ? difference * 2 : 0),
                    positionYButtons, elementSize, elementSize), blueligth))
                {
                    selectElement(Helmet.blue);
                }
            }
            availableLights++;
        }

        if (CameraScript.data.lightGreen)
        {
            if (CameraScript.data.helmet == Helmet.green)
            {
                if (GUI.Button(new Rect((Screen.width / 2 - selected / 2) + availableLights * (elementSize + margin) + difference,
                    positionYButtons - selected / 4, selected, selected), greenligth))
                {
                    //CameraScript.data.helmet = Helmet.green;
                    //audio.PlayOneShot(equipSound);
                }
                trovatoSetected = true;
            }
            else
            {
                if (GUI.Button(new Rect((Screen.width / 2 - elementSize / 2) + margin + availableLights * (elementSize + margin) + (trovatoSetected ? difference * 2 : 0),
                    positionYButtons, elementSize, elementSize), greenligth))
                {
                    selectElement(Helmet.green);
                }
            }
            availableLights++;
        }

        if (CameraScript.data.lightPink)
        {
            if (CameraScript.data.helmet == Helmet.pink)
            {
                if (GUI.Button(new Rect((Screen.width / 2 - selected / 2) + availableLights * (elementSize + margin) + difference,
                    positionYButtons - selected / 4, selected, selected), pinkligth))
                {
                    //CameraScript.data.helmet = Helmet.pink;
                    //audio.PlayOneShot(equipSound);
                }
                trovatoSetected = true;
            }
            else
            {
                if (GUI.Button(new Rect((Screen.width / 2 - elementSize / 2) + margin + availableLights * (elementSize + margin) + (trovatoSetected ? difference * 2 : 0),
                    positionYButtons, elementSize, elementSize), pinkligth))
                {
                    selectElement(Helmet.pink);
                }
            }
            availableLights++;
        }

        if (CameraScript.data.lightRainbow)
        {
            if (CameraScript.data.helmet == Helmet.rainbow)
            {
                if (GUI.Button(new Rect((Screen.width / 2 - selected / 2) + availableLights * (elementSize + margin) + difference,
                    positionYButtons - selected / 4, selected, selected), ranbowligth))
                {
                    //CameraScript.data.helmet = Helmet.rainbow;
                    //audio.PlayOneShot(equipSound);
                }
                trovatoSetected = true;
            }
            else
            {
                if (GUI.Button(new Rect((Screen.width / 2 - elementSize / 2) + margin + availableLights * (elementSize + margin) + (trovatoSetected ? difference * 2 : 0),
                    positionYButtons, elementSize, elementSize), ranbowligth))
                {
                    selectElement(Helmet.rainbow);
                }
            }
            availableLights++;
        }

        if (availableLights < 6)
        {
            if (GUI.Button(new Rect((Screen.width / 2 - elementSize / 2) + margin + availableLights * (elementSize + margin) + (trovatoSetected ? difference * 2 : 0),
                    positionYButtons, elementSize, elementSize), buyButton))
            {
                if (impressing)
                    Application.LoadLevel("BuyItems");
            }
        }
        GUI.EndScrollView();
    }

    void selectElement(Helmet e)
    {
        if (impressing)
        {
            CameraScript.data.helmet = e;
            audio.PlayOneShot(equipSound);
        }
    }

    void Update()
    {
#if !UNITY_METRO

        Touch touch = Input.touches[0];
        bool fInsideList = IsTouchInsideList(touch.position);
        if (touch.phase == TouchPhase.Began)
            impressing = true;
        if (touch.phase == TouchPhase.Moved && fInsideList)
        {
            position.x -= touch.deltaPosition.x * scrollparam; //2:768= x:Screen.height
            impressing = false;
        }
#else
        if (CameraScript.IsTouch)
        {
            Touch touch = Input.touches[0];
            bool fInsideList = IsTouchInsideList(touch.position);
            if (touch.phase == TouchPhase.Began)
                impressing = true;
            if (touch.phase == TouchPhase.Moved && fInsideList)
            {
                position.y += touch.deltaPosition.y * scrollparam; //2:768= x:Screen.height
                impressing = false;
            }
        }
#endif
    }

    bool IsTouchInsideList(Vector2 touchPos)
    {
        Vector2 screenPos = new Vector2(touchPos.x, touchPos.y);
        Rect rAdjustedBounds = new Rect(0, UnTerzo / 3, Screen.width, Screen.height - (UnTerzo / 3));

        return rAdjustedBounds.Contains(screenPos);
    }
}
