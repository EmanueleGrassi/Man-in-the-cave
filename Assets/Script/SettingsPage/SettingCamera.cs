using UnityEngine;
using System.Collections;

public class SettingCamera : MonoBehaviour {

    float margin, titleWidth, titleHeight, barSize;
    public GUISkin custom;
    public Texture back, title;
    public AudioClip buttonsound;
    Rect labelPosition;
    #if UNITY_METRO
        float istructionWidth,istructionHeight;
        public Texture  istruction;
    #endif
    float UnTerzo;    
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
        {
            Application.LoadLevel(0);
            audio.PlayOneShot(buttonsound);
        }
        GUI.skin = custom;
        if (GUI.Button(new Rect(margin, 0, ((barSize) * 168) / 141, barSize), back))
        {
            Application.LoadLevel(0);
            audio.PlayOneShot(buttonsound);
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

    void Update()
    {
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
            float DestraSinistra = -90 * accel.x;//si muove a destra e sinistra          

            float Altobasso = (accel.y * 90) + 90;
            if (accel.z >= 0)
                Altobasso *= -1;
            transform.rotation = Quaternion.Euler(Altobasso, DestraSinistra, 0f);
        }
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


    bool IsTouchInsideList(Vector2 touchPos)
    {
        Vector2 screenPos = new Vector2(touchPos.x, touchPos.y);
        Rect rAdjustedBounds = new Rect(0, 0, Screen.width, Screen.height);

        return rAdjustedBounds.Contains(screenPos);
    }
    #endregion
}
