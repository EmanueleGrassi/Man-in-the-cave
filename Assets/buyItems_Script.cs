using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class buyItems_Script : MonoBehaviour
{
    public Texture powers, powersP, lights, lightsP, money, moneyP;
    public Texture arcoLight, bengal, bluLight, greenLight, piccone, pinkLight, reborn, redLight, coins;
    float size, margin;
    public GUISkin custom;
    public AudioClip cashsound,noMoney;
    public static event EventHandler plus500, plus1000, plus5000;
    float[] span;
    bool imbuying;
    public Texture back;
    float unmarginino, scrollparam;
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
    private void OnEnable()
    {
        OpenIABEventManager.billingSupportedEvent += billingSupportedEvent;
        OpenIABEventManager.billingNotSupportedEvent += billingNotSupportedEvent;
        OpenIABEventManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
        OpenIABEventManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
        OpenIABEventManager.purchaseSucceededEvent += purchaseSucceededEvent;
        OpenIABEventManager.purchaseFailedEvent += purchaseFailedEvent;
        OpenIABEventManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
        OpenIABEventManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;
    }

    private void consumePurchaseFailedEvent(string obj)
    {
        throw new NotImplementedException();
    }

    private void consumePurchaseSucceededEvent(OnePF.Purchase obj)
    {
        throw new NotImplementedException();
    }

    private void purchaseFailedEvent(string obj)
    {
        throw new NotImplementedException();
    }

    private void purchaseSucceededEvent(OnePF.Purchase obj)
    {
        throw new NotImplementedException();
    }

    private void queryInventoryFailedEvent(string obj)
    {
        throw new NotImplementedException();
    }

    private void queryInventorySucceededEvent(OnePF.Inventory obj)
    {
        throw new NotImplementedException();
    }

    private void billingNotSupportedEvent(string obj)
    {
        throw new NotImplementedException();
    }

    private void billingSupportedEvent()
    {
        throw new NotImplementedException();
    }
    private void OnDisable()
    {
        OpenIABEventManager.billingSupportedEvent -= billingSupportedEvent;
        OpenIABEventManager.billingNotSupportedEvent -= billingNotSupportedEvent;
        OpenIABEventManager.queryInventorySucceededEvent -= queryInventorySucceededEvent;
        OpenIABEventManager.queryInventoryFailedEvent -= queryInventoryFailedEvent;
        OpenIABEventManager.purchaseSucceededEvent -= purchaseSucceededEvent;
        OpenIABEventManager.purchaseFailedEvent -= purchaseFailedEvent;
        OpenIABEventManager.consumePurchaseSucceededEvent -= consumePurchaseSucceededEvent;
        OpenIABEventManager.consumePurchaseFailedEvent -= consumePurchaseFailedEvent;
    }
#endif

    // Use this for initialization
    void Start()
    {
        #region Android inapp
        #if UNITY_ANDROID
            ////com.celialab.ManInTheCave.UnityPlayerNativeActivity
            ////jc = new AndroidJavaClass("com.celialab.ManInTheCave.UnityPlayerNativeActivity");
            //unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            //activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            //activity.Call("init");
        #endif
        #endregion

        #region iOS inapp
        #if UNITY_IPHONE
            /*  OpenIAB.mapSku("SKU", OpenIAB_iOS.STORE, "some.ios.sku");   //scoprire cosa sono "some.ios.sku" forse mentre pubblichiamo lo scopriamo
            OpenIAB.mapSku("SKU", OpenIAB_iOS.STORE, "some.ios.sku");
            OpenIAB.mapSku("SKU", OpenIAB_iOS.STORE, "some.ios.sku");

            OpenIAB.init(new Dictionary<string, string> {
            {OpenIAB_iOS.STORE, "public key"}
            });*/
        #endif
        #endregion

        imbuying = false;
        size = Screen.width / 20;
        margin = Screen.width / 60;
        //#if UNITY_WP8                
        //    span = new float[] {0, 0, size * 2 + margin ,
        //                              margin * 2 + size * 4,
        //                              margin * 3 + size * 6,
        //                              margin * 4 + size * 8};
        //    unmarginino = margin * 11;
        //#else
            span = new float[] {0, 0, size * 2 + 2 * margin ,
								      margin * 4 + size * 4,
								      margin * 6 + size * 6,
								      margin * 8 + size * 8};
            unmarginino = margin * 12;
        //#endif
        scrollparam = (Screen.height * 2) / 768;

        #if UNITY_METRO
            if (CameraScript.IsTouch)
            {
                imbuying = true;
            }
        #endif

            CameraScript.LoadData();
    }

    Vector2 position = Vector2.zero;
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
    //spostare
    float UnTerzo = Screen.height / 3;

    void OnGUI()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.LoadLevel(0);
        if (GUI.skin != custom)
            GUI.skin = custom;
        
        if (GUI.Button(new Rect(margin, margin / 3, ((UnTerzo / 3) * 168) / 141, UnTerzo / 3), back))
            Application.LoadLevel(0);

        custom.label.fontSize = (int)(size);
        if (GUI.Button(new Rect(margin * 2 + ((UnTerzo / 3) * 168) / 141, margin / 3, ((UnTerzo / 3) * 500) / 141, UnTerzo / 3), getStateTextures(0)))
        {
            CurrentState = buyState.Powers;
        }
        if (GUI.Button(new Rect(margin * 3 + ((UnTerzo / 3) * 168) / 141 + ((UnTerzo / 3) * 500) / 141,
            margin / 3, ((UnTerzo / 3) * 550) / 141, UnTerzo / 3), getStateTextures(1)))
        {
            CurrentState = buyState.lights;
        }
        if (GUI.Button(new Rect(margin * 3 + ((UnTerzo / 3) * 168) / 141 + ((UnTerzo / 3) * 500) / 141,
            margin / 3, ((UnTerzo / 3) * 550) / 141, UnTerzo / 3), getStateTextures(2)))
        {
            CurrentState = buyState.money;
        }
        
#if UNITY_METRO
        custom.verticalScrollbarThumb.normal.background = thumb;
#endif
        custom.label.normal.textColor = new Color(255, 255, 255);
        custom.label.fontSize = (int)(size * 0.5f);
       
        GUI.DrawTexture(new Rect(Screen.width-size*3-margin, margin , size, size), coins);
        custom.label.fontSize = (int)(size * 0.7);
        GUI.Label(new Rect(Screen.width - size*2 , margin*0.5f, size * 2, size+margin+0.5f), "" + CameraScript.data.points);
        custom.label.fontSize = (int)(size * 0.5f);

        #region money
        //DA FARE ANCORA
        if (CurrentState == buyState.money)
        {
            //purchases
            custom.button.normal.textColor = new Color(205, 127, 50);
            custom.button.fontSize = (int)(size * 0.7f);
            if (GUI.Button(new Rect(size * 13, margin, size + 2 * margin, size), "+500"))
            {
                if (plus500 != null)
                    plus500(this, new EventArgs());
#if UNITY_ANDROID
                activity.Call("buy", "plus500");
#endif
#if UNITY_IPHONE
            
#endif
            }

            custom.button.normal.textColor = new Color(192, 192, 192);
            custom.button.fontSize = (int)(size * 0.8f);
            if (GUI.Button(new Rect(size * 15 - margin, margin, size + margin * 4, size), "+1000"))
            {
                if (plus1000 != null)
                    plus1000(this, new EventArgs());
#if UNITY_ANDROID
                activity.Call("buy", "plus1000");
#endif
#if UNITY_IPHONE
#endif
            }

            custom.button.normal.textColor = new Color(246, 193, 0);
            custom.button.fontSize = (int)(size * 0.9f);
            if (GUI.Button(new Rect(size * 17, margin, size + 5 * margin, size), "+5000"))
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
        #endregion

        #region powers
        if (CurrentState == buyState.Powers)
        {
            //colonna di sinistra
            if (GUI.Button(new Rect(size*2, size*4, size * 7, size * 7), bengal))
            {
                if (CameraScript.data.points >= 90)
                {
                    CameraScript.data.points -= 90;
                    CameraScript.data.numBengala += 2;
                    CameraScript.SaveData();
                    audio.PlayOneShot(cashsound);
                }
                else
                    audio.PlayOneShot(noMoney);
            }
            if (GUI.Button(new Rect(size*12, size*4 , size * 7, size * 7), reborn))
            {
                if (CameraScript.data.points >= 300)
                {
                    CameraScript.data.points -= 300;
                    CameraScript.data.NumberReborn++;
                    CameraScript.SaveData();
                    audio.PlayOneShot(cashsound);
                }
                else
                    audio.PlayOneShot(noMoney);
            }
        }
        #endregion

        #region lights
        if (CurrentState == buyState.lights)
        {
            int elem = 0;
            int n = helmetToBuy();
            position = GUI.BeginScrollView(new Rect(size * 10, margin * 7, size * 10, Screen.height - (margin * 7)), position,
                                           new Rect(0, 0, size * 10, size * n));
            if (!CameraScript.data.lightRed)
            {
                elem++;
                if (GUI.Button(new Rect(0, span[elem], size * 9, size * 3), redLight))
                {
                    if (CameraScript.data.points >= 500 && imbuying)
                    {
                        CameraScript.data.points -= 500;
                        CameraScript.data.lightRed = true;
                        CameraScript.data.helmet = Helmet.red;
                        CameraScript.SaveData();
                        elem++;
                        //SUONA CASSA
                        audio.PlayOneShot(cashsound);
                    }
                    else
                        audio.PlayOneShot(noMoney);
                }
            }
            if (!CameraScript.data.lightBlue)
            {
                elem++;
                if (GUI.Button(new Rect(0, span[elem], size * 9, size * 3), bluLight))
                {
                    if (CameraScript.data.points >= 550 && imbuying)
                    {
                        CameraScript.data.points -= 550;
                        CameraScript.data.lightBlue = true;
                        CameraScript.data.helmet = Helmet.blue;
                        CameraScript.SaveData();
                        //SUONA CASSA
                        audio.PlayOneShot(cashsound);
                    }
                    else
                        audio.PlayOneShot(noMoney);
                }
            }
            if (!CameraScript.data.lightGreen)
            {
                elem++;
                if (GUI.Button(new Rect(0, span[elem], size * 9, size * 3), greenLight))
                {
                    if (CameraScript.data.points >= 750 && imbuying)
                    {
                        CameraScript.data.points -= 750;
                        CameraScript.data.lightGreen = true;
                        CameraScript.data.helmet = Helmet.green;
                        CameraScript.SaveData();
                        //SUONA CASSA
                        audio.PlayOneShot(cashsound);
                    }
                    else
                        audio.PlayOneShot(noMoney);
                }
            }
            if (!CameraScript.data.lightPink)
            {
                elem++;
                if (GUI.Button(new Rect(0, span[elem], size * 9, size * 3), pinkLight))
                {
                    if (CameraScript.data.points >= 800 && imbuying)
                    {
                        CameraScript.data.points -= 800;
                        CameraScript.data.lightPink = true;
                        CameraScript.data.helmet = Helmet.pink;
                        CameraScript.SaveData();
                        //SUONA CASSA
                        audio.PlayOneShot(cashsound);
                    }
                    else
                        audio.PlayOneShot(noMoney);
                }
            }
            if (!CameraScript.data.lightRainbow)
            {
                elem++;
                if (GUI.Button(new Rect(0, span[elem], size * 9, size * 3), arcoLight))
                {
                    if (CameraScript.data.points >= 6000 && imbuying)
                    {
                        CameraScript.data.points -= 6000;
                        CameraScript.data.lightRainbow = true;
                        CameraScript.data.helmet = Helmet.rainbow;
                        CameraScript.SaveData();
                        //SUONA CASSA
                        audio.PlayOneShot(cashsound);
                    }
                    else
                        audio.PlayOneShot(noMoney);
                }
            }
            GUI.EndScrollView();
        }
        #endregion
    }

    private int helmetToBuy()
    {
        int ret = 14;
        if (CameraScript.data.lightBlue)
            ret -= 3;
        if (CameraScript.data.lightGreen)
            ret -= 3;
        if (CameraScript.data.lightPink)
            ret -= 3;
        if (CameraScript.data.lightRainbow)
            ret -= 3;
        if (CameraScript.data.lightRed)
            ret -= 3;
        return ret;
    }

    void Update()
    {
        #if !UNITY_METRO
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            bool fInsideList = IsTouchInsideList(touch.position);
            if (touch.phase == TouchPhase.Began)
                imbuying = true;
            if (touch.phase == TouchPhase.Moved && fInsideList)
            {
                position.y += touch.deltaPosition.y * scrollparam; //2:768= x:Screen.height
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
                        position.y += touch.deltaPosition.y * scrollparam; //2:768= x:Screen.height
                        imbuying = false;
                    }
                }
            }
#endif
    }

    bool IsTouchInsideList(Vector2 touchPos)
    {
        Vector2 screenPos = new Vector2(touchPos.x, touchPos.y);
        Rect rAdjustedBounds = new Rect(size * 10, margin * 7, size * 10, size * 16);

        return rAdjustedBounds.Contains(screenPos);
    }

    void add500(String ciao)
    {
        CameraScript.data.points += 500;
        CameraScript.SaveData();
    }

    void add1000(String ciao)
    {
        CameraScript.data.points += 1000;
        CameraScript.SaveData();
    }

    void add5k(String ciao)
    {
        CameraScript.data.points += 5000;
        CameraScript.SaveData();
    }
}
