using UnityEngine;
using System.Collections;
using System;

public class Items_Script : MonoBehaviour
{
    float size, margin, barraHeight;
    public Texture blueligth, pinkligth, redligth, greenligth, ranbowligth, white, buyButton;
    public GUISkin custom;
    int availableLights;
    bool draw, impressing;
    public AudioClip equipSound, buttonsound;
    public Texture back;
    float elementSize, positionYButtons;
    float UnTerzo;
    Vector2 position = Vector2.zero;
    float scrollparam;
    int n;

    //camera
    private Quaternion cameraBase = Quaternion.identity;
    private Quaternion calibration = Quaternion.identity;
    private Quaternion baseOrientation = Quaternion.Euler(90, 0, 0);
    private Quaternion baseOrientationRotationFix = Quaternion.identity;
    private Quaternion referanceRotation = Quaternion.identity;
    private readonly Quaternion baseIdentity = Quaternion.Euler(90, 0, 0);
    private readonly Quaternion landscapeRight = Quaternion.Euler(0, 0, 90);
    private readonly Quaternion landscapeLeft = Quaternion.Euler(0, 0, -90);
    private readonly Quaternion upsideDown = Quaternion.Euler(0, 0, 180);
    Vector3 accel;
    float filter = 5.0f;

    // Use this for initialization
    void Start()
    {
        CameraScript.LoadData();
        size = Screen.width / 20;
        margin = Screen.width / 60;
        UnTerzo = Screen.height / 3;
        elementSize = UnTerzo;
        positionYButtons = UnTerzo;
        barraHeight = Screen.width * 81 / 1024;
        scrollparam = (Screen.width * 2) / 768;
#if UNITY_METRO
        scrollparam = scrollparam*5;
#endif
        impressing = false;

        availableLights = 0;
        custom.label.fontSize = (int)(size);
        custom.button.fontSize = (int)(size / 2);
        custom.button.normal.textColor = Color.white;
        switch (CameraScript.data.Helmet)
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

        n = helmetAvailable();
        //test
        //CameraScript.LoadData();
        //CameraScript.data.lightRed = CameraScript.data.lightBlue = CameraScript.data.lightGreen = CameraScript.data.lightPink = CameraScript.data.lightRainbow = true;
    }

    private int helmetAvailable()
    {
        int ret = 1;
        if (CameraScript.data.lightBlue)
            ret++;
        if (CameraScript.data.lightGreen)
            ret++;
        if (CameraScript.data.lightPink)
            ret++;
        if (CameraScript.data.lightRainbow)
            ret++;
        if (CameraScript.data.lightRed)
            ret++;
        return ret;
    }

    void OnGUI()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            CameraScript.SaveData();
            Application.LoadLevel(0);
            audio.PlayOneShot(buttonsound);
        }

        if (GUI.skin != custom)
            GUI.skin = custom;

        if (GUI.Button(new Rect(margin, 0, ((barraHeight) * 168) / 141, barraHeight), back))
        {
            CameraScript.SaveData();
            Application.LoadLevel(0);
            audio.PlayOneShot(buttonsound);
        }
        Rect labelPosition = GUILayoutUtility.GetRect(new GUIContent("Items"), GUI.skin.label);
        GUI.Label(new Rect(margin * 2 + ((barraHeight) * 168) / 141, barraHeight/2 - labelPosition.height / 2, labelPosition.width, labelPosition.height), "Items");

        drawElements();
    }

    bool trovatoSetected = false;

    void drawElements()
    {
        float selected = elementSize * 1.5f;
        float difference = (selected - elementSize);

        //scrollview
        int elem = 0;

        position = GUI.BeginScrollView(new Rect(0, barraHeight, Screen.width, Screen.height - barraHeight), position,
                                       new Rect(0, 0, (n * elementSize) + (margin * 7) + (Screen.width / 2 - elementSize / 2) * 2,
                                           Screen.height - barraHeight), true, false);
        availableLights = 0;
        trovatoSetected = false;
        if (CameraScript.data.Helmet == Helmet.white)
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
            if (CameraScript.data.Helmet == Helmet.red)
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
            if (CameraScript.data.Helmet == Helmet.blue)
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
            if (CameraScript.data.Helmet == Helmet.green)
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
            if (CameraScript.data.Helmet == Helmet.pink)
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
            if (CameraScript.data.Helmet == Helmet.rainbow)
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
            CameraScript.data.Helmet = e;
            audio.PlayOneShot(equipSound);
        }
    }

    void Update()
    {
#if !UNITY_METRO
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            bool fInsideList = IsTouchInsideList(touch.position);
            if (touch.phase == TouchPhase.Began)
                impressing = true;
            if (touch.phase == TouchPhase.Moved && fInsideList)
            {
                position.x -= touch.deltaPosition.x * scrollparam; //2:768= x:Screen.height
                impressing = false;
            }
        }
#else
        if (CameraScript.IsTouch) {
            if (Input.touchCount >0)
            {
                Touch touch = Input.touches[0];
                bool fInsideList = IsTouchInsideList(touch.position);
                if (touch.phase == TouchPhase.Began)
                    impressing = true;
                if (touch.phase == TouchPhase.Moved && fInsideList)
                {
                    position.x -= touch.deltaPosition.x * scrollparam; //2:768= x:Screen.height
                    impressing = false;
                }
            }
        }
#endif
        if (SystemInfo.supportsGyroscope)
        {
            if (start)
            {
                Input.gyro.enabled = true;
                AttachGyro();
                start = false;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation,
            cameraBase * (ConvertRotation(referanceRotation * Input.gyro.attitude) * GetRotFix()), 0.2f);
        }
        else if (SystemInfo.supportsAccelerometer)
        {
            // filter the jerky acceleration in the variable accel:
            accel = Vector3.Lerp(accel, Input.acceleration, filter * Time.deltaTime);
            float x = -((accel.y * 100)); //si muove in alto e basso
            //if(x<-5)
            //    x=-5;
            //else if(x>+33)
            //    x=33;

            float DestraSinistra = -90 * accel.x;//si muove a destra e sinistra
            //if (y < -55)
            //    y = -55;
            //else if (x > +55)
            //    y = 55;

            float Altobasso = (accel.y * 90) + 90;
            if (accel.z >= 0)
                Altobasso *= -1;
            //print(Altobasso);
            transform.rotation = Quaternion.Euler(Altobasso, DestraSinistra, 0f);
        }
    }
    bool start = true;
    private void AttachGyro()
    {
        ResetBaseOrientation();
        UpdateCalibration(true);
        UpdateCameraBaseRotation(true);
        RecalculateReferenceRotation();
    }
    private void UpdateCameraBaseRotation(bool onlyHorizontal)
    {
        if (onlyHorizontal)
        {
            var fw = transform.forward;
            fw.y = 0;
            if (fw == Vector3.zero)
            {
                cameraBase = Quaternion.identity;
            }
            else
            {
                cameraBase = Quaternion.FromToRotation(Vector3.forward, fw);
            }
        }
        else
        {
            cameraBase = transform.rotation;
        }
    }
    private void UpdateCalibration(bool onlyHorizontal)
    {
        if (onlyHorizontal)
        {
            var fw = (Input.gyro.attitude) * (-Vector3.forward);
            fw.z = 0;
            if (fw == Vector3.zero)
            {
                calibration = Quaternion.identity;
            }
            else
            {
                calibration = (Quaternion.FromToRotation(baseOrientationRotationFix * Vector3.up, fw));
            }
        }
        else
        {
            calibration = Input.gyro.attitude;
        }
    }
    private static Quaternion ConvertRotation(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
    private Quaternion GetRotFix()
    {
#if UNITY_3_5
		if (Screen.orientation == ScreenOrientation.Portrait)
			return Quaternion.identity;
		
		if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.Landscape)
			return landscapeLeft;
				
		if (Screen.orientation == ScreenOrientation.LandscapeRight)
			return landscapeRight;
				
		if (Screen.orientation == ScreenOrientation.PortraitUpsideDown)
			return upsideDown;
		return Quaternion.identity;
#else
        return Quaternion.identity;
#endif
    }
    private void ResetBaseOrientation()
    {
        baseOrientationRotationFix = GetRotFix();
        baseOrientation = baseOrientationRotationFix * baseIdentity;
    }

    /// <summary>
    /// Recalculates reference rotation.
    /// </summary>
    private void RecalculateReferenceRotation()
    {
        referanceRotation = Quaternion.Inverse(baseOrientation) * Quaternion.Inverse(calibration);
    }

    bool IsTouchInsideList(Vector2 touchPos)
    {
        Vector2 screenPos = new Vector2(touchPos.x, touchPos.y);
        Rect rAdjustedBounds = new Rect(0, UnTerzo / 3, Screen.width, Screen.height - (UnTerzo / 3));

        return rAdjustedBounds.Contains(screenPos);
    }
}
