using UnityEngine;
using System.Collections;
using System;

public class Scores_script : MonoBehaviour
{

    float margin, size, achiveSize, barSize;
    public GUISkin custom;
    Vector2 pos;
    public Texture back, scores, scoresPressed, achivements, achivementsPressed;
    public Texture jumpAchiv, bengalAchiv, moneyAchiv, timeAchiv;
    float scrollparam;
    public Texture2D thumb;
    public AudioClip buttonsound;
    float UnTerzo;
    bool IsScore = true; //se false visualizza achivements

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
        size = Screen.width / 20;      
        margin = Screen.width / 60;
        UnTerzo = Screen.height / 3;
        barSize = (Screen.width * 81 / 1024);
        achiveSize = (Screen.height - barSize) / 2.15f;
        pos = Vector2.zero;
        scrollparam = (Screen.height * 2) / 768;
#if UNITY_METRO
        scrollparam = scrollparam*5;
        custom.verticalScrollbarThumb.normal.background = thumb;
#endif
    }

    
    void OnGUI()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.LoadLevel(0);
            audio.PlayOneShot(buttonsound);
        }
        GUI.skin = custom;
        if (GUI.Button(new Rect(margin, 0 , ((barSize) * 168) / 141, barSize), back))
        {
            Application.LoadLevel(0);
            audio.PlayOneShot(buttonsound);
        }
        if (GUI.Button(new Rect(margin * 2 + ((barSize) * 168) / 141, 0, ((barSize) * 500) / 141, barSize), IsScore ? scoresPressed : scores))
        {
            IsScore = true;
        }
        //if (GUI.Button(new Rect(margin * 3 + ((UnTerzo / 3) * 168) / 141 + ((UnTerzo / 3) * 500) / 141,
        //    margin / 3, ((UnTerzo / 3) * 550) / 141, UnTerzo / 3), IsScore ? achivements : achivementsPressed))
        //{
        //    IsScore = false;
        //}

        if (IsScore)
            DrawScore();
        else
            DrawAchiv();//visualizza gli achiv
    }
    void DrawScore()
    { 
        GUI.skin.label.fontSize = (int)(size * 0.7f);
        if (CameraScript.data.Records.Count != 0)
        {
            int i = 0;
            int n = CameraScript.data.Records.Count;
            pos = GUI.BeginScrollView(new Rect(margin * 3 + size, barSize, Screen.width - (margin * 3 + size), Screen.height - barSize),
                pos, new Rect(0, 0, Screen.width, size * 2 + size * n * 1.5f));
            foreach (var item in CameraScript.data.Records)
            { 
                if (i == 0)
                {
                    GUI.skin.label.normal.textColor = new Color(246, 193, 0);
                    GUI.skin.label.fontSize = (int)(size * 1f);
                }
                else if (i == 1)
                {
                    GUI.skin.label.normal.textColor = new Color(192, 192, 192);
                    GUI.skin.label.fontSize = (int)(size * 1f);
                }
                else if (i == 2)
                {
                    GUI.skin.label.normal.textColor = new Color(205, 127, 50);
                    GUI.skin.label.fontSize = (int)(size * 1f);
                }
                else
                {
                    GUI.skin.label.fontSize = (int)(size * 0.6);
                    GUI.skin.label.normal.textColor = Color.white;
                }
                //GUI.Label(new Rect(0, (i * (size * 1.3f)), size * 8, size * 1.5f), formatScoreSecond(CameraScript.data.Records[i].Seconds));
                GUI.Label(new Rect(0, (i * (size * 1.3f)), size * 12, size * 1.5f), CameraScript.data.Records[i].Points + " points  "
                    + formatScoreSecond(CameraScript.data.Records[i].Seconds));
                GUI.Label(new Rect(size * 12, (i * (size * 1.3f)), size * 10, size * 1.5f), CameraScript.data.Records[i].Day +
                    "/" + CameraScript.data.Records[i].Month + "/" + CameraScript.data.Records[i].Year);
                i++;
            }
            GUI.EndScrollView();
        }
    }

    void DrawAchiv()
    {
        GUILayout.BeginArea(new Rect(margin, barSize+margin, Screen.width, Screen.height - barSize));
        GUILayout.BeginVertical(); 
            GUILayout.BeginHorizontal();           
                GUILayout.Label(bengalAchiv, GUILayout.Width(achiveSize), GUILayout.Height(achiveSize));
                GUILayout.Label(timeAchiv, GUILayout.Width(achiveSize), GUILayout.Height(achiveSize));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
                GUILayout.Label(moneyAchiv, GUILayout.Width(achiveSize), GUILayout.Height(achiveSize));
                GUILayout.Label(jumpAchiv, GUILayout.Width(achiveSize), GUILayout.Height(achiveSize));
            GUILayout.EndHorizontal(); 

        GUILayout.EndVertical();        
        GUILayout.EndArea();

        //GUI.DrawTexture(new Rect(Screen.width / 4 - achiveSize / 2, barSize, achiveSize, achiveSize), bengalAchiv);
        //GUI.DrawTexture(new Rect(Screen.width / 4 - achiveSize / 2, barSize + achiveSize, achiveSize, achiveSize), moneyAchiv);
    }

    void Update()
    {
       #if UNITY_METRO
            if (CameraScript.IsTouch)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.touches[0];
                    bool fInsideList = IsTouchInsideList(touch.position);

                    if (touch.phase == TouchPhase.Moved && fInsideList)
                    {
                        pos.y += touch.deltaPosition.y * scrollparam;
                    }
                }   
            }
#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            bool fInsideList = IsTouchInsideList(touch.position);

            if (touch.phase == TouchPhase.Moved && fInsideList)
            {
                pos.y += touch.deltaPosition.y * scrollparam*2;
            }
        }
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
            float DestraSinistra = -90 * accel.x;//si muove a destra e sinistra          

            float Altobasso = (accel.y * 90) + 90;
            if (accel.z >= 0)
                Altobasso *= -1;
            transform.rotation = Quaternion.Euler(Altobasso, DestraSinistra, 0f);
        }
#endif
     }
    

    public static string formatScoreSecond(float a)
    {
        TimeSpan t = TimeSpan.FromSeconds(a);
        if(t.Minutes>0)
            return (String.Format("{0:0}:{1:00} min", t.Minutes, t.Seconds));
        else
            return (String.Format("{0:#0} sec", t.Seconds));
    }

    bool IsTouchInsideList(Vector2 touchPos)
    {
        Vector2 screenPos = new Vector2(touchPos.x, touchPos.y);
        Rect rAdjustedBounds = new Rect(size * 6, size * 4, size * 18, size * 12);

        return rAdjustedBounds.Contains(screenPos);
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
#if UNITY_METRO
        return Quaternion.Euler(0, 0, 90);
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
    #endregion
}
