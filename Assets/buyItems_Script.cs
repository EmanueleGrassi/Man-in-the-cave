using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class buyItems_Script : MonoBehaviour
{
    public Texture powers, powersP, lights, lightsP, money, moneyP;
    public Texture Money500_texture, Money1000_texture, Money5000_texture;
    public Texture arcoLight, bengal, bluLight, greenLight, piccone, pinkLight, reborn, redLight, coins;
    float size, margin, barraHeight, moneyTopMargin;
    public GUISkin custom;
    public AudioClip cashsound, noMoney, buttonsound;
    public static event EventHandler plus500, plus1000, plus5000;
    float[] span;
    bool imbuying;
    public Texture back;
    float unmarginino, scrollparam, elemSize, UnTerzo;
    public Texture2D thumb;
    buyState CurrentState = buyState.Powers;
    enum buyState
    {
        Powers,
        lights,
        money
    }
#if UNITY_ANDROID
    AndroidJavaClass unityPlayer;
    AndroidJavaObject activity;
#endif

#if UNITY_IPHONE
    
#endif

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
    Texture getStateTextures(int ButtonNumb)
    {
        if (ButtonNumb == 0)
        {
            if (CurrentState == buyState.Powers)
                return powersP;
            else
                return powers;
        }
        else if (ButtonNumb == 1)
        {
            if (CurrentState == buyState.lights)
                return lightsP;
            else
                return lights;
        }
        else
        {
            if (CurrentState == buyState.money)
                return moneyP;
            else
                return money;
        }
    }
    // Use this for initialization
    void Start()
    {
        if (CameraScript.data == null)
            CameraScript.LoadData();
        #region Android inapp
#if UNITY_ANDROID
            ////com.celialab.ManInTheCave.UnityPlayerNativeActivity
            ////jc = new AndroidJavaClass("com.celialab.ManInTheCave.UnityPlayerNativeActivity");
        unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            //activity.Call("init");
#endif
        #endregion

        #region iOS inapp
#if UNITY_IPHONE
            
#endif
        #endregion


        imbuying = false;
        size = Screen.width / 20;
        margin = Screen.width / 60;
        UnTerzo = Screen.height / 3;
        barraHeight = Screen.width * 81 / 1024;
        moneyTopMargin = barraHeight + (Screen.height) / 2 - (size * 6) / 2;
        span = new float[] {0, 0, size * 2 + 2 * margin ,
								      margin * 4 + size * 4,
								      margin * 6 + size * 6,
								      margin * 8 + size * 8};
        unmarginino = margin * 12;
        scrollparam = (Screen.height * 2) / 768;

        elemSize = size * 5;

#if UNITY_METRO
            if (CameraScript.IsTouch)
            {
                imbuying = true;
            }
#endif

        CameraScript.LoadData();
    }

    void Update()
    {
#if !UNITY_METRO
        if (Input.touchCount > 0 && CurrentState == buyState.lights)
        {
            Touch touch = Input.touches[0];
            bool fInsideList = IsTouchInsideList(touch.position);
            if (touch.phase == TouchPhase.Began)
                imbuying = true;
            if (touch.phase == TouchPhase.Moved && fInsideList)
            {
                position.x -= touch.deltaPosition.x * scrollparam;
                imbuying = false;
            }
        }
#else
        if (CameraScript.IsTouch)
        {
            if (Input.touchCount > 0)
                {
                Touch touch = Input.touches[0];
                bool fInsideList = IsTouchInsideList(touch.position);
                if (touch.phase == TouchPhase.Began)
                    imbuying = true;
                if (touch.phase == TouchPhase.Moved && fInsideList)
                {
                    position.y += touch.deltaPosition.y * scrollparam;
                    imbuying = false;
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

            accel = Vector3.Lerp(accel, Input.acceleration, filter * Time.deltaTime);
            float x = -((accel.y * 100)); //si muove in alto e basso
            float DestraSinistra = -90 * accel.x;//si muove a destra e sinistra
            float Altobasso = (accel.y * 90) + 90;
            if (accel.z >= 0)
                Altobasso *= -1;
            transform.rotation = Quaternion.Euler(Altobasso, DestraSinistra, 0f);
        }
    }

    Vector2 position = Vector2.zero;
    

    void OnGUI()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.LoadLevel(0);
            audio.PlayOneShot(buttonsound);
        }
        if (GUI.skin != custom)
            GUI.skin = custom;
        float buttonsMarginLeft = margin;
        if (GUI.Button(new Rect(buttonsMarginLeft, 0, ((barraHeight) * 168) / 141, barraHeight), back))
        {
            Application.LoadLevel(0);
            audio.PlayOneShot(buttonsound);
        }
        buttonsMarginLeft += margin + ((barraHeight) * 168) / 141;
        if (GUI.Button(new Rect(buttonsMarginLeft, 0, ((barraHeight) * 351) / 141, barraHeight), getStateTextures(0)))
        {
            CurrentState = buyState.Powers;
        }
        buttonsMarginLeft += margin + ((barraHeight) * 351) / 141;
        if (GUI.Button(new Rect(buttonsMarginLeft,
            0, ((barraHeight) * 297) / 141, barraHeight), getStateTextures(1)))
        {
            CurrentState = buyState.lights;
        }
        buttonsMarginLeft += margin + ((barraHeight) * 297) / 141;
        if (GUI.Button(new Rect(buttonsMarginLeft,
            0, ((barraHeight) * 321) / 141, barraHeight), getStateTextures(2)))
        {
            CurrentState = buyState.money;
        }

#if UNITY_METRO
        custom.verticalScrollbarThumb.normal.background = thumb;
#endif


        
        GUI.skin.label.fontSize = (int)(size * 0.7);
        Rect labelPosition = GUILayoutUtility.GetRect(new GUIContent(CameraScript.data.Credits.ToString()), GUI.skin.label);
        GUI.Label(new Rect(Screen.width - (labelPosition.width + margin), barraHeight / 2 - labelPosition.height / 2, labelPosition.width, labelPosition.height), CameraScript.data.Credits.ToString());
        GUI.DrawTexture(new Rect(Screen.width - (labelPosition.width + margin * 2 + barraHeight * 0.8f),
            barraHeight / 2f - barraHeight * 0.8f / 2f, barraHeight * 0.8f, barraHeight * 0.8f), coins);
        if (CurrentState == buyState.money)
        {
            DrawMoney();
        }
        else if (CurrentState == buyState.Powers)
        {
            DrawPowers();
        }
        else if (CurrentState == buyState.lights)
        {
            DrawLights();
        }
    }
    #region METODI GUI
    void DrawMoney()
    {
        //purchases
        if (GUI.Button(new Rect(margin, moneyTopMargin, size * 6, size * 6), Money500_texture))
        {
            if (plus500 != null)
                plus500(this, new EventArgs());
            #if UNITY_ANDROID
                            activity.Call("buy", "plus500");
            #endif
            #if UNITY_IPHONE
            
            #endif
        }
        if (GUI.Button(new Rect(Screen.width / 2 - size * 6/2, moneyTopMargin, size * 6, size * 6), Money1000_texture))
        {
            if (plus1000 != null)
                plus1000(this, new EventArgs());
            #if UNITY_ANDROID
                            activity.Call("buy", "plus1000");
            #endif
            #if UNITY_IPHONE
            #endif
        }
        if (GUI.Button(new Rect(Screen.width-(margin+ size * 6), moneyTopMargin, size * 6, size * 6), Money5000_texture))
        {
            if (plus5000 != null)
                plus5000(this, new EventArgs());
#if UNITY_ANDROID
                activity.Call("buy", "plus5k");
#endif
#if UNITY_IPHONE
#endif
        }
    }
    void DrawPowers()
    {
        if (GUI.Button(new Rect(size * 2, barraHeight +(Screen.height) / 2 - (size * 7) / 2, size * 7, size * 7), bengal))
        {
            compra("Bengala", 90);
        }
        if (GUI.Button(new Rect(size * 12, barraHeight + (Screen.height) / 2 - (size * 7) / 2, size * 7, size * 7), reborn))
        {
            compra("Reborn", 300);
        }
    }
    void compra(string typeAcquisto, int costo)
    {
        if (CameraScript.data.Credits >= costo)
        {
            CameraScript.data.Credits -= costo;
            switch (typeAcquisto)
            {
                case "Reborn": CameraScript.data.NumberReborn++;
                    break;
                case "Bengala": CameraScript.data.NumBengala += 2;
                    break;
            }

            CameraScript.SaveData();
            audio.PlayOneShot(cashsound);
        }
        else
            audio.PlayOneShot(noMoney);
    }
    void DrawLights()
    {
        int elem = -1;
        int n = helmetToBuy();
        position = GUI.BeginScrollView(new Rect(0, barraHeight + (Screen.height / 2 - elemSize / 2), Screen.width, Screen.height - (barraHeight + (Screen.height / 2 - elemSize / 2))),
                                        position, new Rect(0, 0, size * n, Screen.height - barraHeight));
        if (!CameraScript.data.lightRed)
        {
            elem++;
            if (GUI.Button(new Rect(elemSize * elem, 0, elemSize, elemSize), redLight))
            {
                compraLuce(Helmet.red, 500);
            }
        }
        if (!CameraScript.data.lightBlue)
        {
            elem++;
            if (GUI.Button(new Rect(elemSize * elem, 0, elemSize, elemSize), bluLight))
            {
                compraLuce(Helmet.blue, 550);
            }
        }
        if (!CameraScript.data.lightGreen)
        {
            elem++;
            if (GUI.Button(new Rect(elemSize * elem, 0, elemSize, elemSize), greenLight))
            {
                compraLuce(Helmet.green, 750);
            }
        }
        if (!CameraScript.data.lightPink)
        {
            elem++;
            if (GUI.Button(new Rect(elemSize * elem, 0, elemSize, elemSize), pinkLight))
            {
                compraLuce(Helmet.pink, 800);
            }
        }
        if (!CameraScript.data.lightRainbow)
        {
            elem++;
            if (GUI.Button(new Rect(elemSize * elem, 0, elemSize, elemSize), arcoLight))
            {
                compraLuce(Helmet.rainbow, 6000);
            }
        }
        GUI.EndScrollView();
    }
    void compraLuce(Helmet typeAcquisto, int costo)
    {
        if (CameraScript.data.Credits >= costo && imbuying)
        {
            CameraScript.data.Credits -= costo;
            switch (typeAcquisto)
            {
                case Helmet.red: CameraScript.data.lightRed = true;
                    break;
                case Helmet.blue: CameraScript.data.lightBlue = true;
                    break;
                case Helmet.green: CameraScript.data.lightGreen = true;
                    break;
                case Helmet.pink: CameraScript.data.lightPink = true;
                    break;
                case Helmet.rainbow: CameraScript.data.lightRainbow = true;
                    break;
            }
            CameraScript.data.Helmet = Helmet.rainbow;
            CameraScript.SaveData();
            audio.PlayOneShot(cashsound);//SUONA CASSA
        }
        else
            audio.PlayOneShot(noMoney);
    }
    #endregion

    private int helmetToBuy()
    {
        int ret = 0;
        if (!CameraScript.data.lightBlue)
            ret += 5;
        if (!CameraScript.data.lightGreen)
            ret += 5;
        if (!CameraScript.data.lightPink)
            ret += 5;
        if (!CameraScript.data.lightRainbow)
            ret += 5;
        if (!CameraScript.data.lightRed)
            ret += 5;
        return ret;
    }

    

    #region GYRO CONTROL
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
        Rect rAdjustedBounds = new Rect(0, 0, Screen.width, Screen.height);

        return rAdjustedBounds.Contains(screenPos);
    }
    #endregion

    void add500(String ciao)
    {
        CameraScript.data.Credits += 500;
        CameraScript.SaveData();
    }

    void add1000(String ciao)
    {
        CameraScript.data.Credits += 1000;
        CameraScript.SaveData();
    }

    void add5k(String ciao)
    {
        CameraScript.data.Credits += 5000;
        CameraScript.SaveData();
    }
}
