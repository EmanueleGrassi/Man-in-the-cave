using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using System.Net;
using System.Threading;
using UnityEditor;

public class MenuScript : MonoBehaviour
{
    public GUISkin custom;
    float height;
    float margin;
    static bool play = true;
    bool PlayButtonPressed;
    public Texture PlayButton,PlayButtonP, ScoreButton, ItemsButton, BuyItemsButton, SettingsButton;
    public Texture Title, facebook, twitter, review, celialab;
    public Texture myAppFreeBanner, normalBanner;
    public static event EventHandler GOReviews;
    public AudioClip promotionSound,playSound,buttonSound;


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
    void Start()
    {
        if (CameraScript.data == null)
            CameraScript.LoadData();
        PlayScript.State = PlayScript.PlayState.menu;
        height = Screen.width / 20;
        margin = Screen.width / 60;
        PlayButtonPressed = false;        
        if (!(Application.platform == RuntimePlatform.WP8Player))
        {
            if (PlayerPrefs.GetString("gift1") == "")
            {
                addPoints(1000);
            }
        }
        accel = Input.acceleration;
    }

    float StartPromotionSound;
    bool StartPromotion = false;
    float StayPromotionBannar;
    void Update()
    {
        if (play)
        {
            play = false;
            audio.Play();
        }
        if (StartPromotion && Time.time >= StartPromotionSound)
        {
            audio.PlayOneShot(promotionSound);
            StayPromotionBannar = Time.time + 5;
            StartPromotion = false;
        }

        if (SystemInfo.supportsGyroscope)
        {
            if (start)
            {
                Input.gyro.enabled = true;
                PlayerSettings.accelerometerFrequency = 0;
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
            float DestraSinistra = -90 * accel.x;//si muove a destra e sinistra          

            float Altobasso = (accel.y * 90) + 90;
            if (accel.z >= 0)
                Altobasso *= -1;
            transform.rotation = Quaternion.Euler(Altobasso, DestraSinistra, 0f);
        }
    }
    bool start = true;

    #region GYRO CONTROL
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
    private void RecalculateReferenceRotation()
    {
        referanceRotation = Quaternion.Inverse(baseOrientation) * Quaternion.Inverse(calibration);
    }
    #endregion

    private void addPoints(int p)
    {
        CameraScript.data.Credits += p;
        CameraScript.SaveData();
        PlayerPrefs.SetString("gift1", "done");//  salvataggio su unity
        PlayerPrefs.Save();
        StartPromotionSound = Time.time + 2.113f;
        StartPromotion = true;
    }

    void OnGUI()
    {        
        if (GUI.skin != custom)
            GUI.skin = custom;
        float piccoliBottoniSize = Screen.width / 4.6f;
        float UnTerzo = Screen.height / 3;
        float playSize = UnTerzo - margin;
        float BottoniHeight = UnTerzo * 0.7f - margin;
        float SocialSize = UnTerzo * 0.5f - margin;
        GUI.DrawTexture(new Rect((Screen.width / 2) - (((BottoniHeight * 4 + margin * 3)) / 2),
                                UnTerzo / 2 - (BottoniHeight/2), 
                                ((BottoniHeight * 4 + margin * 3)),
                                BottoniHeight),
                                Title, ScaleMode.ScaleToFit, true);
        if (GUI.Button(new Rect((Screen.width / 2) - playSize / 2, UnTerzo + ((UnTerzo / 2) - playSize / 2), playSize, playSize),PlayButtonPressed? PlayButtonP:PlayButton ))
        {
            PlayButtonPressed = true;
            Application.LoadLevel("main");
            audio.PlayOneShot(playSound);
        }

        float posizioneButton;
        int numBottone = 0;
        if (CameraScript.data.Records.Count != 0)
        {
            posizioneButton = Screen.width / 2 - ((BottoniHeight * 4 + margin * 3)) / 2;
            if (GUI.Button(new Rect(posizioneButton,
                                     UnTerzo * 2 + ((UnTerzo / 2) - BottoniHeight / 2),
                                     BottoniHeight, BottoniHeight), ScoreButton))
            {
                Application.LoadLevel("Scores");
                audio.PlayOneShot(buttonSound);
            }
            numBottone++;
        }
        else
            posizioneButton = Screen.width / 2 - ((BottoniHeight * 3 + margin * 3)) / 2;

        if (GUI.Button(new Rect(posizioneButton + (margin + BottoniHeight) * numBottone,
                               UnTerzo * 2 + ((UnTerzo / 2) - BottoniHeight / 2),
                                 BottoniHeight, BottoniHeight), BuyItemsButton))
        {
            Application.LoadLevel("BuyItems");
            audio.PlayOneShot(buttonSound);
        }
        numBottone++;
        if (GUI.Button(new Rect(posizioneButton + (margin + BottoniHeight) * numBottone,
                                UnTerzo * 2 + ((UnTerzo / 2) - BottoniHeight / 2),
                                BottoniHeight, BottoniHeight), ItemsButton))
        {
            Application.LoadLevel("Items");
            audio.PlayOneShot(buttonSound);
        }
        numBottone++;
        if (GUI.Button(new Rect(posizioneButton + (margin + BottoniHeight) * numBottone,
                               UnTerzo * 2 + ((UnTerzo / 2) - BottoniHeight / 2),
                                 BottoniHeight, BottoniHeight), SettingsButton))
        {
            Application.LoadLevel("Settings");
            audio.PlayOneShot(buttonSound);
        }
        if (GUI.Button(new Rect(Screen.width - (SocialSize + margin), Screen.height - (SocialSize * 3 + margin), SocialSize, SocialSize), facebook))
        {
            Application.OpenURL("https://www.facebook.com/Celialab");//vai su facebook
        }
        if (GUI.Button(new Rect(Screen.width - (SocialSize + margin), Screen.height - (SocialSize * 2 + margin), SocialSize, SocialSize), twitter))
        {
            Application.OpenURL("https://twitter.com/celialabGames");//vai su twitter
        }
        if (GUI.Button(new Rect(Screen.width - (SocialSize + margin), Screen.height - (SocialSize + margin), SocialSize, SocialSize), review))
        {
            if (Application.platform == RuntimePlatform.Android)
                Application.OpenURL("market://details?id=com.celialab.ManInTheCave");
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
                Application.OpenURL("");//vai su review  mistery
            else if (Application.platform == RuntimePlatform.WP8Player || Application.platform == RuntimePlatform.MetroPlayerARM ||
            Application.platform == RuntimePlatform.MetroPlayerX64 || Application.platform == RuntimePlatform.MetroPlayerX86)
            {
                if (GOReviews != null)
                    GOReviews(this, new EventArgs());
            }
        }

        var celialabHeight = ((Screen.width / 5) * 59) / 200;
        if (GUI.Button(new Rect(margin / 2.3f, Screen.height - (celialabHeight + margin / 2.3f), Screen.width / 5, celialabHeight), celialab))
        {
            Application.OpenURL("http://celialab.com/");
        }
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();

        if (Time.time < StayPromotionBannar)
        {
            if (Application.platform == RuntimePlatform.WP8Player)
            {
                if (GUI.Button(new Rect(0, 0, Screen.width, (Screen.width * 156) / 1280), myAppFreeBanner)) //1280: 156= width :x
                {
                    Application.OpenURL("https://www.myAppFree.com");//visualizza bottone myappfree con link al sito  
                }
            }
            else
                GUI.DrawTexture(new Rect(0, 0, Screen.width, (Screen.width * 250) / 2048), normalBanner, ScaleMode.ScaleToFit, true);
        }

    }
}