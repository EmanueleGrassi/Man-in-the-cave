using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Xml;


public class Data
{
    public int Credits = 0;
    public List<Record> Records = new List<Record>();
    int pickaxeState;/*50 max*/
    public int PickaxeState
    {
        get { return pickaxeState; }
        set
        {
            if (value == 0 && NumberPickaxe > 0)
            {
                NumberPickaxe--;
                pickaxeState = 50;
            }
            else
                pickaxeState = value;
        }
    }

    public int NumberPickaxe = 0;
    public int NumberReborn = 2;
    public int NumBengala = 3;
    public bool lightRed = false;
    public bool lightBlue = false;
    public bool lightGreen = false;
    public bool lightPink = false;
    public bool lightRainbow = false;
    public Helmet Helmet = Helmet.white;

#region SAVE LOAD
    public void Save()
    {
        string path = Path.Combine(Application.persistentDataPath, "data.xml");
        var serializer = new XmlSerializer(typeof(Data));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }

    public static void Load()
    {
        string path = Path.Combine(Application.persistentDataPath, "data.xml");
        var serializer = new XmlSerializer(typeof(Data));

        try
        {
            using (var stream = new FileStream(path, FileMode.Open))
            {
                CameraScript.data = serializer.Deserialize(stream) as Data;
            }
        }
#if UNITY_WP8
        catch (System.IO.FileNotFoundException e)
#else
        catch(System.IO.IsolatedStorage.IsolatedStorageException e)
#endif
        {
            CameraScript.data = new Data();
            CameraScript.data.Save();
        }
    }
#endregion

}
public enum Helmet
{
    white = 0,
    red = 1,
    blue = 2,
    green = 3,
    pink = 4,
    rainbow = 5
}

public class Record : IComparable<Record>
{
    public int Seconds;
    public int Credits, Points;
    public int Day, Month, Year;

    /// <summary>
    /// NON CHIAMARE QUESTO COSTRUTTORE, USA I PARAMETRI
    /// </summary>
    public Record()
    {   }
    public Record(int seconds, int credits, int points, int day, int month, int year)
    {
        Seconds = seconds;
        Credits = credits;
        Points = points;
        Day = day;
        Month = month;
        Year = year;
    }

    public int CompareTo(Record b)
    {
        // Alphabetic sort name[A to Z]
        return -this.Seconds.CompareTo(b.Seconds);
    }
}







public class CameraScript : MonoBehaviour
{
    #region variabili
    #region private
    float height;
    float margin, UnTerzo;
    bool rebornUsed, playgameover = true;
    float BottoniSize;
    #endregion

    #region pubbliche
    /*CLASSE SALVATAGGIO*/
    public static Data data;
    /*CLASSE SALVATAGGIO*/
    public GUISkin custom;
    public Transform playerPG;
    public Transform _20bis;
    public AudioClip[] rockSound;
    public AudioClip background, gameoverSound;
    public float Volume;

    public static float PlayTime;
    public static float TempoRecord = 0;
    public static bool replay;
    //Gui Pause e result
    public Texture PlayButton, ScoreButton, ItemsButton, BuyItemsButton, homeButton, playAgainButton, likebtn;
    //Gui Play
    public Texture coin, pause;
    public Texture Infobox_Texture;
    public Texture useReborn_Texture, OKbutton_Texture, CancelButton_Texture;
    public Texture Vignette_Texture;

    public static GameObject bengalaButton, movementButton, jumpButton;
    public static bool replayGame;

    public Texture2D backclock, leftClock, holdingClock;

    #endregion

#if UNITY_METRO 
    private Boolean VisualizeButtonsOnW8=false;
	public Texture Istruction;
    float instrTime;
    bool showInstru;
    public static bool IsTouch = false;
    public static event EventHandler saveEvent, loadEvent;
#endif
#if UNITY_METRO || UNITY_WP8
    public static event EventHandler shareEvent;
    public static event EventHandler GOReviews;
#endif
    #endregion

    #region Save and Load
    public static void SaveData()
    {
#if UNITY_METRO
            if (saveEvent != null)
                saveEvent(new object(), new EventArgs());
#else
        data.Save();
#endif

        //#else

        //            foreach(var item in data.Records)
        //            {
        //                print("day rec: "+item.y);
        //            }
        //            JSON js = new JSON();
        //            print (((JSON)data).serialized);
        //            js["salvataggio"] = ((JSON)data);
        //            PlayerPrefs.SetString("salvataggio", js.serialized);//  salvataggio su unity
        //            PlayerPrefs.Save();
        //#endif
    }
    public static void LoadData()
    {
#if UNITY_METRO
            if (loadEvent != null)
                loadEvent(new object(), new EventArgs());
#else
        Data.Load();
#endif
        //#else
        //        if (PlayerPrefs.GetString("salvataggio") != "")
        //        {
        //            JSON js = new JSON();
        //            js.serialized = PlayerPrefs.GetString("salvataggio"); //devo prendere quella dei settings 
        //            data = (Data)js.ToJSON("salvataggio");
        //        }
        //        else
        //        {
        //            data = new Data();
        //            for (int i =0; i<20; i++)
        //                data.Records[i]= new Rect(0,0,0,0);
        //            SaveData();
        //        }
        //#endif
    }
    #endregion

    void Start()
    {
        //carica i salvataggi
        LoadData();

        bengalaButton = GameObject.Find("BengalaButton");
        movementButton = GameObject.Find("LeftTouchPad");
        jumpButton = GameObject.Find("RightTouchPad");

        rebornUsed = false;
        Volume = 0.2f;
        PlayTime = 0;
        height = Screen.width / 20;
        margin = Screen.width / 60;
        UnTerzo = Screen.height / 3;
        BottoniSize = UnTerzo * 0.7f - margin;


#if UNITY_METRO
        if (IsTouch)
        {
            VisualizeButtonsOnW8 = true;
            //visualizza immmagine istruzioni
        }
        instrTime = CameraScript.PlayTime + 6;
        showInstru = true;
        if (PlayerPrefs.GetInt("instru") == 10)
        {
            showInstru = false;
        }
#endif
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = false;
        }
        PlayScript.State = PlayScript.PlayState.play;
    }


    void Update()
    {
#if UNITY_METRO
        if (PlayTime > instrTime)
        {
            showInstru = false;
            PlayerPrefs.SetInt("instru", 10);
            PlayerPrefs.Save();
        }
#endif

        if (replayGame)
        {
            PlayScript.State = PlayScript.PlayState.play;
            replayGame = false;
        }

        if (!audio.isPlaying)
        {
            audio.clip = background;
            audio.Play();
            audio.volume = Volume;
        }

        if (RockBehaviour.Play)
        {
            AudioSource.PlayClipAtPoint(rockSound[UnityEngine.Random.Range(0, rockSound.Length)], RockBehaviour.deathP);
            RockBehaviour.Play = false;
        }
        if (PlayScript.State == PlayScript.PlayState.play)
        {
            // SEGUE IL PERSONAGGIO CON LA TELECAMERA
            float playerPGxPOW = (playerPG.position.x * playerPG.position.x);

            transform.position = new Vector3(playerPG.position.x,
                                             Mathf.Sqrt((1 - (playerPGxPOW / 6400)) * 25f),
                                             -1 * Mathf.Sqrt((1 - (playerPGxPOW / 4900)) * 25f));

            transform.rotation = Quaternion.Euler(2.9f,
                                                  Mathf.Tan(-(playerPG.position.x / (350 * Mathf.Sqrt(9800 - 2 * playerPGxPOW)))) * 180 / Mathf.PI,
                                                  0);

            /* vecchio transform.rotation = Quaternion.Euler(2.9f,
                Mathf.Tan(-((2 * playerPG.position.x) / (5 * Mathf.Sqrt(2500 - playerPGxPOW)))) * 180 / Mathf.PI,
                0);*/
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            if (PlayScript.State == PlayScript.PlayState.play)
                PlayScript.State = PlayScript.PlayState.pause;
            else if (PlayScript.State == PlayScript.PlayState.pause)
                PlayScript.State = PlayScript.PlayState.play;
        }
        PlayTime += Time.deltaTime;
    }
    public static void ManageButton(bool visibility)
    {
        bengalaButton.SetActive(visibility);
        movementButton.SetActive(visibility);
        jumpButton.SetActive(visibility);
    }

    #region OnGUI
    void OnGUI()
    {
        if (GUI.skin != custom)
            GUI.skin = custom;

        GUI.depth = 1;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Vignette_Texture);
        if (PlayScript.State == PlayScript.PlayState.play)
        {
#if UNITY_METRO
            ManageButton(VisualizeButtonsOnW8);
            if (showInstru)
                GUI.DrawTexture(new Rect((Screen.width / 2) - Screen.width / 4, 0, Screen.width / 2, (Screen.width / 2) * 115 / 488), Istruction);
#endif
#if !UNITY_METRO
            ManageButton(true);
#endif

            drawPlay();
        }
        else if (PlayScript.State == PlayScript.PlayState.pause)
        {
            ManageButton(false);
            drawPause();
        }
        else if (PlayScript.State == PlayScript.PlayState.result)
        {
            ManageButton(false);
            drawResult();
        }
    }

    private void drawPause()
    {
        float middle = Screen.height / 2 - (BottoniSize / 2);
        if (GUI.Button(new Rect((Screen.width / 4) - (BottoniSize / 2), middle, BottoniSize, BottoniSize), PlayButton))
        {
            PlayScript.State = PlayScript.PlayState.play;
        }
        if (GUI.Button(new Rect((Screen.width * 2 / 4) - (BottoniSize / 2), middle, BottoniSize, BottoniSize), playAgainButton))
        {
            Application.LoadLevel(1);
        }
        if (GUI.Button(new Rect((Screen.width * 3 / 4) - (BottoniSize / 2), middle, BottoniSize, BottoniSize), homeButton))
        {
            PlayScript.State = PlayScript.PlayState.menu;
            Application.LoadLevel(0);
        }
    }

    private void drawResult()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.LoadLevel(0);

        bool visualizeReborn = false;
        if (data.NumberReborn > 0 && !rebornUsed)
            visualizeReborn = true;

        if (visualizeReborn == true)//chiede su vui usare il reborn
        {
            float rebornWidth= Screen.width*2/4;
            float rebornHeight = rebornWidth * 210 / 512;
            GUI.DrawTexture(new Rect(Screen.width / 2 - rebornWidth/2, (Screen.height / 2) - margin - (rebornHeight), rebornWidth, rebornHeight),
                useReborn_Texture, ScaleMode.ScaleToFit, true);
            if (GUI.Button(new Rect(Screen.width / 3 - BottoniSize / 2, Screen.height / 2 + margin, BottoniSize, BottoniSize), OKbutton_Texture))
            {
                data.NumberReborn--;
                if (playerPG != null)
                {
                    Vector3 pos = playerPG.transform.position;
                    playerPG.transform.position = new Vector3(pos.x, pos.y + 10, pos.z);
                    playerPG.gameObject.SetActive(true);
                    _20bis.gameObject.SetActive(true);
                    ManageButton(true);
                }
                rebornUsed = true;
                GameManager_script.PGdead = false;
                replayGame = true;
            }
            if (GUI.Button(new Rect(Screen.width * 2 / 3 - BottoniSize/2, Screen.height/2 + margin, BottoniSize, BottoniSize), CancelButton_Texture))
            {
                visualizeReborn = false;
                rebornUsed = true;
            }
        }
        else if (visualizeReborn == false) //visualizza i result
        {
            if (playgameover)
            {

                audio.volume = 0.75f;
                audio.PlayOneShot(gameoverSound);
                playgameover = false;
            }
            GUI.skin.label.fontSize = (int)(height * 1.7);
            GUI.Label(new Rect(height*6-4*margin, margin, height*11, height * 3+margin), "GAME OVER");
            GUI.skin.label.fontSize = (int)(height * 1.3);
            TimeSpan t = TimeSpan.FromSeconds(CameraScript.PlayTime);
            int totalPoints = (int)((CameraScript.PlayTime + PlayScript.GameCredits) * 1.7); //tempo*1.7 + gamecredits * 5
            GUI.Label(new Rect(height * 7 - margin, height * 4 - margin, height * 8, height * 2), String.Format("Time {0:00}:{1:00}", t.Minutes, t.Seconds));
            GUI.Label(new Rect(height * 7 - margin, height * 5, height * 8, height * 2), String.Format("Credits " + PlayScript.GameCredits.ToString()));
            GUI.Label(new Rect(height * 7 - margin, height * 6 + margin, height * 8, height * 2), String.Format("Points " + totalPoints.ToString()));
                
            

            if (GUI.Button(new Rect((Screen.width / 4) - (BottoniSize / 2), height * 8, BottoniSize, BottoniSize), playAgainButton))
            {
                CameraScript.data.Credits += PlayScript.GameCredits;                
                salvaRecord(new Record((int)PlayTime, PlayScript.GameCredits, totalPoints, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));
                SaveData();
                Application.LoadLevel(1);
            }
            if (GUI.Button(new Rect((Screen.width * 3 / 4) - (BottoniSize / 2), height * 8, BottoniSize, BottoniSize), homeButton))
            {
                CameraScript.data.Credits += PlayScript.GameCredits;
                salvaRecord(new Record((int)PlayTime, PlayScript.GameCredits, totalPoints, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));
                SaveData();
                Application.LoadLevel(0);
            }
#if (UNITY_WP8 || UNITY_METRO)
            TempoRecord = CameraScript.PlayTime;
            if (GUI.Button(new Rect((Screen.width * 2 / 4) - (BottoniSize / 2), height * 8, BottoniSize, BottoniSize), likebtn))
            {
                if (shareEvent != null)
                    shareEvent(this, new EventArgs());
            }
#endif
        }
    }


    private void salvaRecord(Record rec)
    {     
        data.Records.Add(rec);
        data.Records.Sort();

        if(data.Records.Count<=20)
        {
            //taglia dopo i venti
        }
    }

    void drawPlay()
    {
        var multime = 1;
        var rot = (CameraScript.PlayTime) * 6;
        var height2 = Screen.width / 6;
        Matrix4x4 startMatrix = GUI.matrix;
        Rect ClockRect = new Rect(margin, margin, height2, height2);
        Vector2 Centerpoint = new Vector2(margin + (height2 / 2), margin + (height2 / 2));
        //inizio orologio
        GUI.DrawTexture(ClockRect, backclock, ScaleMode.ScaleToFit, true);
        if ((CameraScript.PlayTime % 60) > 30 * multime)
        {
            GUIUtility.RotateAroundPivot(rot + 180, Centerpoint);
            GUI.DrawTexture(ClockRect, leftClock, ScaleMode.ScaleToFit, true);
            GUI.matrix = startMatrix;
            GUI.DrawTexture(ClockRect, holdingClock, ScaleMode.ScaleToFit, true);
            multime = multime * 3;
        }
        else
        {
            GUIUtility.RotateAroundPivot(rot - 180, Centerpoint);
            GUI.DrawTexture(ClockRect, leftClock, ScaleMode.ScaleToFit, true);
            GUI.matrix = startMatrix;
            GUI.DrawTexture(ClockRect, leftClock, ScaleMode.ScaleToFit, true);
        }
        //fine orologio
        GUI.DrawTexture(new Rect(margin, margin, height2, height2 * 700 / 600), Infobox_Texture, ScaleMode.ScaleToFit, true);
        TimeSpan t = (TimeSpan.FromSeconds(PlayTime));
        string TimeText;
        if (t.Minutes >= 1)
            TimeText = string.Format("{0}:{1:00}", t.Minutes, t.Seconds);
        else
            TimeText = string.Format("{0:#0}", t.Seconds);
        GUI.skin.label.fontSize = (int)(height * 1.2);
        Rect labelPosition = GUILayoutUtility.GetRect(new GUIContent(TimeText), custom.label);
        GUI.Label(new Rect(margin + ((height2 - labelPosition.width) / 2), ((height2 * 0.8f - labelPosition.height) / 2),
            labelPosition.width, labelPosition.height), TimeText);

        if (GUI.Button(new Rect(Screen.width - (margin + height2 / 2), margin, height2 / 2, height2 / 2), pause))
        {
            PlayScript.State = PlayScript.PlayState.pause;
        }
    }
    #endregion
}