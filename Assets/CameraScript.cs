using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;


public class Data
{
    public int points = 10000;
    public Rect[] Records = new Rect[20];
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

    public int NumberPickaxe;
    public int NumberReborn = 2;
    public int numBengala = 3;
    public bool lightRed = false;
    public bool lightBlue = false;
    public bool lightGreen = false;
    public bool lightPink = false;
    public bool lightRainbow = false;
    public Helmet helmet = Helmet.white;


    #region SAVE
    // serialize this class to JSON
    public static implicit operator JSON(Data value)
    {
        JSON js = new JSON();
        JSON jsTransform = new JSON();
        js["transform"] = jsTransform;

        js["points"] = value.points;
        js["pickaxeState"] = value.pickaxeState;
        js["NumberPickaxe"] = value.NumberPickaxe;
        js["NumberReborn"] = value.NumberReborn;
        js["numBengala"] = value.numBengala;
        js["lightRed"] = value.lightRed;
        js["lightBlue"] = value.lightBlue;
        js["lightGreen"] = value.lightGreen;
        js["lightPink"] = value.lightPink;
        js["lightRainbow"] = value.lightRainbow;
        js["helmet"] = (int)value.helmet;
        Debug.Log(" len:" + value.Records.Length);
        js["Record"] = new JSON[] {
			(JSON)(value.Records[0]),
			(JSON)(value.Records[1]),
			(JSON)(value.Records[2]),
			(JSON)(value.Records[3]),
			(JSON)(value.Records[4]),
			(JSON)(value.Records[5]),
			(JSON)(value.Records[6]),
			(JSON)(value.Records[7]),
			(JSON)(value.Records[8]),
			(JSON)(value.Records[9]),
			(JSON)(value.Records[10]),
			(JSON)(value.Records[11]),
			(JSON)(value.Records[12]),
			(JSON)(value.Records[13]),
			(JSON)(value.Records[14]),
			(JSON)(value.Records[15]),
			(JSON)(value.Records[16]),
			(JSON)(value.Records[17]),
			(JSON)(value.Records[18]),
			(JSON)(value.Records[19])
		};
        return js;
    }

    // JSON to class conversion
    public static explicit operator Data(JSON value)
    {
        checked
        {
            JSON jsTransform = value.ToJSON("transform");
            var deserislizedClass = new Data();
            deserislizedClass.points = value.ToInt("points");
            deserislizedClass.pickaxeState = value.ToInt("pickaxeState");
            deserislizedClass.NumberPickaxe = value.ToInt("NumberPickaxe");
            deserislizedClass.NumberReborn = value.ToInt("NumberReborn");
            deserislizedClass.numBengala = value.ToInt("numBengala");
            deserislizedClass.lightRed = value.ToBoolean("lightRed");
            deserislizedClass.lightBlue = value.ToBoolean("lightBlue");
            deserislizedClass.lightGreen = value.ToBoolean("lightGreen");
            deserislizedClass.lightPink = value.ToBoolean("lightPink");
            deserislizedClass.lightRainbow = value.ToBoolean("lightRainbow");
            deserislizedClass.helmet = (Helmet)value.ToInt("helmet");
            deserislizedClass.Records = value.ToArray<Rect>("Record");
            return deserislizedClass;
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
public class Record
{
    public int Seconds;
    public DateTime When;
}

public class CameraScript : MonoBehaviour
{
    public GUISkin custom;
    float nexshot;
    public Transform playerPG;
    public Transform _20bis;
    public AudioClip rockSound,gameoverSound;
    public AudioClip background;
    float smoothTime;
    public float Volume;
    Vector2 velocity = new Vector2();
    public Texture coin, pause, quit;
    public bool isDebuging = true;
    /*CLASSE SALVATAGGIO*/
    public static Data data; /*CLASSE SALVATAGGIO*/
    public static float PlayTime;
    public static bool replay;
    bool rebornUsed, playgameover  = true;
    //munu
    float height;
    float margin, margin2;
    public Texture PlayButton, ScoreButton, ItemsButton, BuyItemsButton, homeButton, playAgainButton, likebtn;
    public Texture Title, facebook, twitter, review, celialab;
    public Texture useReborn, OKbutton, CancelButton;
    public static GameObject bengalaButton, movementButton, jumpButton;
    public static bool replayGame;
    public static bool goBack;
    public static event EventHandler saveEvent, loadEvent, shareEvent;
    public static event EventHandler GOReviews;

    public static void SaveData()
    {
        if (Application.platform == RuntimePlatform.WP8Player)
        {
            if (saveEvent != null)
                saveEvent(saveEvent, new EventArgs());
        }
        else
        {
            JSON js = new JSON();
            js["salvataggio"] = (JSON)data;
            PlayerPrefs.SetString("salvataggio", js.serialized);//  salvataggio su unity
            PlayerPrefs.Save();
        }

    }
    public static void LoadData()
    {
        if (Application.platform == RuntimePlatform.WP8Player)
        {
            if (loadEvent != null)
                loadEvent(loadEvent, new EventArgs());
        }
        else
        {

            if (PlayerPrefs.GetString("salvataggio") != "")
            {
                JSON js = new JSON();
                js.serialized = PlayerPrefs.GetString("salvataggio"); //devo prendere quella dei settings 
                data = (Data)js.ToJSON("salvataggio");
            }
            else
            {
                data = new Data();
				for (int i =0; i<20; i++)
					data.Records[i]= new Rect(0,0,0,0);
                SaveData();
            }

        }
    }

    void Start()
    {
        goBack = false;
        //carica i salvataggi
        LoadData();
        //data = new Data();
        #region test Records
        //data = new Data();
        //data.Records[0] = new Rect(8,9,34,3432);
        //print("creato");
        //SaveData();
        //print("salvato");
        //LoadData();
        //print("width 0: "+data.Records[0].width );
        #endregion

        bengalaButton = GameObject.Find("BengalaButton");
        movementButton = GameObject.Find("LeftTouchPad");
        jumpButton = GameObject.Find("RightTouchPad");

        rebornUsed = false;
        nexshot = 0.0f;
        smoothTime = 0.3f;
        Volume = 0.2f;
        PlayTime = 0;
        //if (Application.platform == RuntimePlatform.WP8Player)
        //    visualizePause = false;

        height = Screen.width / 20;
        margin = Screen.width / 60;
        margin2 = 0;// Screen.width / 70;
        PlayScript.State = PlayScript.PlayState.play;
    }

    // Update is called once per frame    
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            PlayScript.State = PlayScript.PlayState.pause;
        if (replayGame)
        {
            PlayScript.State = PlayScript.PlayState.play;
            replayGame = false;
        }
        PlayTime += Time.deltaTime;
        if (!audio.isPlaying)
        {
            audio.clip = background;
            audio.Play();
            audio.volume = Volume;
        }

        if (RockBehaviour.Play)
        {
            AudioSource.PlayClipAtPoint(rockSound, RockBehaviour.deathP);
            RockBehaviour.Play = false;
        }
        if (PlayScript.State == PlayScript.PlayState.play)
        {
            // SEGUE IL PERSONAGGIO CON LA TELECAMERA
            float playerPGxPOW = (playerPG.position.x * playerPG.position.x);
            /*ellisse 
             * semiassi:
             * z=6
             * x=
             */
            transform.position = new Vector3(playerPG.position.x,
                            Mathf.Sqrt((1 - (playerPGxPOW / 3600)) * 25f),
                -1 * Mathf.Sqrt((1 - (playerPGxPOW / 1550)) * 25f));


            transform.rotation = Quaternion.Euler(2.9f,
                Mathf.Tan(-((2 * playerPG.position.x) / (5 * Mathf.Sqrt(2500 - playerPGxPOW)))) * 180 / Mathf.PI,
                0);
            nexshot = Time.time + 0.01f;
        }
    }
    public static void ManageButton(bool visibility)
    {
        bengalaButton.guiTexture.active = visibility;
        movementButton.guiTexture.active = visibility;
        jumpButton.guiTexture.active = visibility;
    }
    void OnGUI()
    {
        GUI.skin = custom;
        if (isDebuging)
            if (GUI.Button(new Rect(Screen.width / 2 - height / 2, 0, height, height), quit))
                Application.Quit();

        if (PlayScript.State == PlayScript.PlayState.play)
        {
            ManageButton(true);
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
        if (GUI.Button(new Rect(height * 5, height * 4, height * 3, height * 3), PlayButton))
            PlayScript.State = PlayScript.PlayState.play;
        if (GUI.Button(new Rect(height * 9, height * 4, height * 3, height * 3), playAgainButton))
        {
            Application.LoadLevel(1);
        }
        if (GUI.Button(new Rect(height * 13, height * 4, height * 3, height * 3), homeButton))
        {
            PlayScript.State = PlayScript.PlayState.menu;
            Application.LoadLevel(0);
        }
        if (Input.GetKey(KeyCode.Escape))
            PlayScript.State = PlayScript.PlayState.play;

    }

    private void drawResult()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.LoadLevel(0);

        bool? vis = false;
        if (data.NumberReborn > 0 && !rebornUsed)
            vis = null;

        if (vis == null)
        {
            //vis = false;
            GUI.DrawTexture(new Rect(height * 2 - 6 * margin, height * 2, height * 20, height * 5), useReborn, ScaleMode.ScaleToFit, true);
            if (GUI.Button(new Rect(height * 5 + margin * 5, height * 8, height * 3, height * 3), OKbutton))
            {

                data.NumberReborn--;
                //SaveData();
                //torna alla partita, da dove stavi
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
            if (GUI.Button(new Rect(height * 9 + margin * 5, height * 8, height * 3, height * 3), CancelButton))
            {
                vis = false;
                rebornUsed = true;
            }
        }
        else if (vis == false)
        {
            if (playgameover)
            {
                audio.PlayOneShot(gameoverSound);
                playgameover = false;
            }
            Rect record = new Rect(PlayTime, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
            salvaRecord(record);
            GUI.skin.label.fontSize = (int)(height * 1.3f);
            GUI.Label(new Rect(height, height * 2, height * 20, height * 2), "You survived inside the cave for:");
            TimeSpan t = TimeSpan.FromSeconds(CameraScript.PlayTime);
            GUI.Label(new Rect(height * 8 + 2 * margin, height * 4.5f, height * 20, height * 2), String.Format("{0:00}:{1:00}", t.Minutes, t.Seconds));
            //HAI GUADAGNATO TOT MONETE
            if (GUI.Button(new Rect(height * 3, height * 8, height * 5, height * 3 + margin), playAgainButton))
            {
                CameraScript.data.points += PlayScript.gamePoints;
                SaveData();
                Application.LoadLevel(1);
            }
            if (GUI.Button(new Rect(height * 13, height * 8, height * 5, height * 3 + margin), homeButton))
            {
                CameraScript.data.points += PlayScript.gamePoints;
                SaveData();
                Application.LoadLevel(0);
            }
            if (Application.platform == RuntimePlatform.WP8Player)
            {
                if (GUI.Button(new Rect(height * 8, height * 8, height * 5, height * 3 + margin), likebtn))
                {
                    if (shareEvent != null)
                        shareEvent(t, new EventArgs());
                }
            }
        }
    }

    private void salvaRecord(Rect rec)
    {
        List<Rect> t = new List<Rect>();
        foreach (var item in data.Records)
        {
            t.Add(item);
        }
        int i = 0;
        foreach (var item in t)
        {
            if (rec.x > item.x)
            {
                t.Insert(i, rec);
                break;
            }
            i++;
        }
        i = 0;
        foreach (var item in data.Records)
        {
            data.Records[i]= t[i];
            i++;
        }
        //for (int i = 0; i < data.Records.Length; i++)
        //{
        //    if (rec.x > data.Records[i].x)
        //    {
        //        scambiaDa(i, rec);
        //        return;
        //    }
        //}
    }

    private void scambiaDa(int i, Rect rec)
    {
        Rect temp = data.Records[i];
        data.Records[i] = rec;
        for (int j = i + 1; j < data.Records.Length; j++)
        {
            Rect temp1 = data.Records[j];
            data.Records[j] = temp;
            temp = temp1;
        }
    }
    #region EX MENU
    // PlayButton, ScoreButton, ItemsButton, BuyItemsButton;
    //void drawMenu()
    //{
    //    float piccoliBottoniSize =Screen.width/4.6f - margin2;
    //    float titleHeight=((Screen.width/2)*305)/1094;
    //    float playSize=Screen.width/8;
    //    float BuyHeight= (((piccoliBottoniSize*2)*160)/740);
    //    float SocialSize=Screen.width/13;
    //        GUI.DrawTexture(new Rect ((Screen.width/2)-Screen.width/4, 0, Screen.width/2, titleHeight), 
    //                                                    Title, ScaleMode.ScaleToFit, true);	
    //        if (GUI.Button(new Rect((Screen.width/2)-playSize/2, titleHeight+margin*2, playSize, playSize) ,PlayButton)) 
    //        {
    //            PlayScript.State = PlayScript.PlayState.play; 
    //        }

    //            float piccoliBottoniHight= (((piccoliBottoniSize)*169)/400);
    //            if (GUI.Button( new Rect((Screen.width-((piccoliBottoniSize*2)+margin2))/2,
    //                                    titleHeight + playSize + margin*2,
    //                                    piccoliBottoniSize,piccoliBottoniHight), ScoreButton))
    //            {
    //                Application.LoadLevel(4);
    //            }
    //            if (GUI.Button( new Rect(((Screen.width-((piccoliBottoniSize*2)+margin2))/2) +(piccoliBottoniSize),
    //                                        (margin2*3)+ titleHeight + playSize+ margin*2,
    //                                        piccoliBottoniSize ,piccoliBottoniHight), ItemsButton))
    //            {
    //                Application.LoadLevel(3);//vai nella pagina Items
    //            }			


    //        if (GUI.Button( new Rect((Screen.width/2)-((piccoliBottoniSize*2)/2),
    //                            (margin2*4)+ titleHeight+ playSize+ piccoliBottoniHight+margin*2,
    //                                piccoliBottoniSize*2,BuyHeight), BuyItemsButton))
    //        {
    //            Application.LoadLevel(2);//vai nella pagina BuyItems
    //        }

    //    if (GUI.Button(new Rect(Screen.width-SocialSize, Screen.height-SocialSize*3, SocialSize, SocialSize) ,facebook)) 
    //        {
    //                Application.OpenURL("https://www.facebook.com/Celialab");//vai su facebook
    //        }
    //    if (GUI.Button(new Rect(Screen.width-SocialSize, Screen.height-SocialSize*2, SocialSize, SocialSize) ,twitter)) 
    //        {
    //                Application.OpenURL("https://twitter.com/celialabGames");//vai su twitter
    //        }
    //    if (GUI.Button(new Rect(Screen.width-SocialSize, Screen.height-SocialSize, SocialSize, SocialSize) ,review)) 
    //        {
    //        if(Application.platform == RuntimePlatform.Android)
    //                Application.OpenURL("");//vai su review
    //        else if (Application.platform == RuntimePlatform.IPhonePlayer)
    //            Application.OpenURL("");//vai su review
    //        else if(Application.platform == RuntimePlatform.WP8Player || Application.platform == RuntimePlatform.WindowsPlayer)
    //            if(GOReviews!=null)
    //                GOReviews(this, new EventArgs());

    //        }

    //    var celialabHeight=((Screen.width/5)*59)/200;
    //    if(GUI.Button(new Rect(margin,Screen.height-(celialabHeight+margin), Screen.width/5,celialabHeight), celialab))
    //    {
    //        Application.OpenURL("http://celialab.com/");
    //    }
    //    if(Input.GetKey(KeyCode.Escape))
    //        Application.Quit();
    //}
    #endregion

    bool visualizeRightCoin, startVisualizeRightCoin;
    void drawPlay()
    {
        // Visualizza punti. Lo script si adatta atutte le risoluzioni
        GUI.DrawTexture(new Rect(margin, margin, height, height), coin, ScaleMode.ScaleToFit, true);
        GUI.skin.label.fontSize = (int)height;
        GUI.Label(new Rect(height + (margin * 2), margin / 8f, 600f, 300f),
            "" + PlayScript.gamePoints);

        // se non si gioca su android o wp allora visualizza pausa		
        if (GUI.Button(new Rect(Screen.width - (margin + height), margin, height, height), pause))
        {
            PlayScript.State = PlayScript.PlayState.pause;
        }

        //Visualizza il tempo
        //GUI.DrawTexture(new Rect (margin,margin,height,height), clock, ScaleMode.ScaleToFit, true);	
        TimeSpan t = (TimeSpan.FromSeconds(PlayTime));
        GUI.Label(new Rect((float)Screen.width - (height * 3.0f + 2f * margin),
                            margin / 8f,
                            height * 3.0f, /*moltiplicare la metà delle cifre tempo per height*/

                            300.0f), string.Format("{0}:{1:00}", t.Minutes, t.Seconds));

        if (isDebuging)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - height / 2, 0, height, height), quit))
                Application.Quit();
        }
    }
}




/*
 * 2013 WyrmTale Games | MIT License
 *
 * Based on   MiniJSON.cs by Calvin Rien | https://gist.github.com/darktable/1411710
 * that was Based on the JSON parser by Patrick van Bergen | http://techblog.procurios.nl/k/618/news/view/14605/14863/How-do-I-write-my-own-parser-for-JSON.html
 *
 * Extended it so it includes/returns a JSON object that can be accessed using 
 * indexers. also easy custom class to JSON object mapping by implecit and explicit asignment  overloading
 *
 * Permission is hereby granted, free of charge, to any person obtaining
 * a copy of this software and associated documentation files (the
 * "Software"), to deal in the Software without restriction, including
 * without limitation the rights to use, copy, modify, merge, publish,
 * distribute, sublicense, and/or sell copies of the Software, and to
 * permit persons to whom the Software is furnished to do so, subject to
 * the following conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
 * CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
 * TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */



public class JSON
{

    public Dictionary<string, object> fields = new Dictionary<string, object>();

    // Indexer to provide read/write access to the fields
    public object this[string fieldName]
    {
        // Read one byte at offset index and return it.
        get
        {
            if (fields.ContainsKey(fieldName))
                return (fields[fieldName]);
            return null;
        }
        // Write one byte at offset index and return it.
        set
        {
            if (fields.ContainsKey(fieldName))
                fields[fieldName] = value;
            else
                fields.Add(fieldName, value);
        }
    }

    public string ToString(string fieldName)
    {
        if (fields.ContainsKey(fieldName))
            return System.Convert.ToString(fields[fieldName]);
        else
            return "";
    }
    public int ToInt(string fieldName)
    {
        if (fields.ContainsKey(fieldName))
            return System.Convert.ToInt32(fields[fieldName]);
        else
            return 0;
    }
    public float ToFloat(string fieldName)
    {
        if (fields.ContainsKey(fieldName))
            return System.Convert.ToSingle(fields[fieldName]);
        else
            return 0.0f;
    }
    public bool ToBoolean(string fieldName)
    {
        if (fields.ContainsKey(fieldName))
            return System.Convert.ToBoolean(fields[fieldName]);
        else
            return false;
    }

    public string serialized
    {
        get
        {
            return _JSON.Serialize(this);
        }
        set
        {
            JSON o = _JSON.Deserialize(value);
            if (o != null)
                fields = o.fields;
        }
    }

    public JSON ToJSON(string fieldName)
    {
        if (!fields.ContainsKey(fieldName))
            fields.Add(fieldName, new JSON());

        return (JSON)this[fieldName];
    }

    // to serialize/deserialize a Vector2
    public static implicit operator Vector2(JSON value)
    {
        return new Vector3(
             System.Convert.ToSingle(value["x"]),
             System.Convert.ToSingle(value["y"]));
    }
    public static explicit operator JSON(Vector2 value)
    {
        checked
        {
            JSON o = new JSON();
            o["x"] = value.x;
            o["y"] = value.y;
            return o;
        }

    }


    // to serialize/deserialize a Vector3
    public static implicit operator Vector3(JSON value)
    {
        return new Vector3(
             System.Convert.ToSingle(value["x"]),
             System.Convert.ToSingle(value["y"]),
             System.Convert.ToSingle(value["z"]));
    }

    public static explicit operator JSON(Vector3 value)
    {
        checked
        {
            JSON o = new JSON();
            o["x"] = value.x;
            o["y"] = value.y;
            o["z"] = value.z;
            return o;
        }
    }

    // to serialize/deserialize a Quaternion
    public static implicit operator Quaternion(JSON value)
    {
        return new Quaternion(
             System.Convert.ToSingle(value["x"]),
             System.Convert.ToSingle(value["y"]),
             System.Convert.ToSingle(value["z"]),
             System.Convert.ToSingle(value["w"])
             );
    }
    public static explicit operator JSON(Quaternion value)
    {
        checked
        {
            JSON o = new JSON();
            o["x"] = value.x;
            o["y"] = value.y;
            o["z"] = value.z;
            o["w"] = value.w;
            return o;
        }
    }

    // to serialize/deserialize a Color
    public static implicit operator Color(JSON value)
    {
        return new Color(
             System.Convert.ToSingle(value["r"]),
             System.Convert.ToSingle(value["g"]),
             System.Convert.ToSingle(value["b"]),
             System.Convert.ToSingle(value["a"])
             );
    }
    public static explicit operator JSON(Color value)
    {
        checked
        {
            JSON o = new JSON();
            o["r"] = value.r;
            o["g"] = value.g;
            o["b"] = value.b;
            o["a"] = value.a;
            return o;
        }
    }

    // to serialize/deserialize a Color32
    public static implicit operator Color32(JSON value)
    {
        return new Color32(
             System.Convert.ToByte(value["r"]),
             System.Convert.ToByte(value["g"]),
             System.Convert.ToByte(value["b"]),
             System.Convert.ToByte(value["a"])
             );
    }
    public static explicit operator JSON(Color32 value)
    {
        checked
        {
            JSON o = new JSON();
            o["r"] = value.r;
            o["g"] = value.g;
            o["b"] = value.b;
            o["a"] = value.a;
            return o;
        }
    }

    // to serialize/deserialize a Rect
    public static implicit operator Rect(JSON value)
    {
        return new Rect(
             System.Convert.ToInt32(value["left"]),
         System.Convert.ToInt32(value["top"]),
         System.Convert.ToInt32(value["width"]),
         System.Convert.ToInt32(value["height"])
         );
    }
    public static explicit operator JSON(Rect value)
    {
        checked
        {
            JSON o = new JSON();
            o["left"] = value.xMin;
            o["top"] = value.yMax;
            o["width"] = value.width;
            o["height"] = value.height;
            return o;
        }
    }


    // get typed array out of the object as object[] 
    public T[] ToArray<T>(string fieldName)
    {
        if (fields.ContainsKey(fieldName))
        {
            if (fields[fieldName] is IEnumerable)
            {
                List<T> l = new List<T>();
                foreach (object o in (fields[fieldName] as IEnumerable))
                {
                    if (l is List<string>)
                        (l as List<string>).Add(System.Convert.ToString(o));
                    else
                        if (l is List<int>)
                            (l as List<int>).Add(System.Convert.ToInt32(o));
                        else
                            if (l is List<float>)
                                (l as List<float>).Add(System.Convert.ToSingle(o));
                            else
                                if (l is List<bool>)
                                    (l as List<bool>).Add(System.Convert.ToBoolean(o));
                                else
                                    if (l is List<Vector2>)
                                        (l as List<Vector2>).Add((Vector2)((JSON)o));
                                    else
                                        if (l is List<Vector3>)
                                            (l as List<Vector3>).Add((Vector3)((JSON)o));
                                        else
                                            if (l is List<Rect>)
                                                (l as List<Rect>).Add((Rect)((JSON)o));
                                            else
                                                if (l is List<Color>)
                                                    (l as List<Color>).Add((Color)((JSON)o));
                                                else
                                                    if (l is List<Color32>)
                                                        (l as List<Color32>).Add((Color32)((JSON)o));
                                                    else
                                                        if (l is List<Quaternion>)
                                                            (l as List<Quaternion>).Add((Quaternion)((JSON)o));
                                                        else
                                                            if (l is List<JSON>)
                                                                (l as List<JSON>).Add((JSON)o);
                }
                return l.ToArray();
            }
        }
        return new T[] { };
    }



    /// <summary>
    /// This class encodes and decodes JSON strings.
    /// Spec. details, see http://www.json.org/
    ///
    /// JSON uses Arrays and Objects. These correspond here to the datatypes IList and IDictionary.
    /// All numbers are parsed to doubles.
    /// </summary>
    sealed class _JSON
    {
        /// <summary>
        /// Parses the string json into a value
        /// </summary>
        /// <param name="json">A JSON string.</param>
        /// <returns>An List&lt;object&gt;, a Dictionary&lt;string, object&gt;, a double, an integer,a string, null, true, or false</returns>
        public static JSON Deserialize(string json)
        {
            // save the string for debug information
            if (json == null)
            {
                return null;
            }

            return Parser.Parse(json);
        }

        sealed class Parser : IDisposable
        {
            const string WHITE_SPACE = " \t\n\r";
            const string WORD_BREAK = " \t\n\r{}[],:\"";

            enum TOKEN
            {
                NONE,
                CURLY_OPEN,
                CURLY_CLOSE,
                SQUARED_OPEN,
                SQUARED_CLOSE,
                COLON,
                COMMA,
                STRING,
                NUMBER,
                TRUE,
                FALSE,
                NULL
            };

            StringReader json;

            Parser(string jsonString)
            {
                json = new StringReader(jsonString);
            }

            public static JSON Parse(string jsonString)
            {
                using (var instance = new Parser(jsonString))
                {
                    return (instance.ParseValue() as JSON);
                }
            }

            public void Dispose()
            {
                json.Dispose();
                json = null;
            }

            JSON ParseObject()
            {
                Dictionary<string, object> table = new Dictionary<string, object>();
                JSON obj = new JSON();
                obj.fields = table;

                // ditch opening brace
                json.Read();

                // {
                while (true)
                {
                    switch (NextToken)
                    {
                        case TOKEN.NONE:
                            return null;
                        case TOKEN.COMMA:
                            continue;
                        case TOKEN.CURLY_CLOSE:
                            return obj;
                        default:
                            // name
                            string name = ParseString();
                            if (name == null)
                            {
                                return null;
                            }

                            // :
                            if (NextToken != TOKEN.COLON)
                            {
                                return null;
                            }
                            // ditch the colon
                            json.Read();

                            // value
                            table[name] = ParseValue();
                            break;
                    }
                }
            }

            List<object> ParseArray()
            {
                List<object> array = new List<object>();

                // ditch opening bracket
                json.Read();

                // [
                var parsing = true;
                while (parsing)
                {
                    TOKEN nextToken = NextToken;

                    switch (nextToken)
                    {
                        case TOKEN.NONE:
                            return null;
                        case TOKEN.COMMA:
                            continue;
                        case TOKEN.SQUARED_CLOSE:
                            parsing = false;
                            break;
                        default:
                            object value = ParseByToken(nextToken);

                            array.Add(value);
                            break;
                    }
                }

                return array;
            }

            object ParseValue()
            {
                TOKEN nextToken = NextToken;
                return ParseByToken(nextToken);
            }

            object ParseByToken(TOKEN token)
            {
                switch (token)
                {
                    case TOKEN.STRING:
                        return ParseString();
                    case TOKEN.NUMBER:
                        return ParseNumber();
                    case TOKEN.CURLY_OPEN:
                        return ParseObject();
                    case TOKEN.SQUARED_OPEN:
                        return ParseArray();
                    case TOKEN.TRUE:
                        return true;
                    case TOKEN.FALSE:
                        return false;
                    case TOKEN.NULL:
                        return null;
                    default:
                        return null;
                }
            }

            string ParseString()
            {
                StringBuilder s = new StringBuilder();
                char c;

                // ditch opening quote
                json.Read();

                bool parsing = true;
                while (parsing)
                {

                    if (json.Peek() == -1)
                    {
                        parsing = false;
                        break;
                    }

                    c = NextChar;
                    switch (c)
                    {
                        case '"':
                            parsing = false;
                            break;
                        case '\\':
                            if (json.Peek() == -1)
                            {
                                parsing = false;
                                break;
                            }

                            c = NextChar;
                            switch (c)
                            {
                                case '"':
                                case '\\':
                                case '/':
                                    s.Append(c);
                                    break;
                                case 'b':
                                    s.Append('\b');
                                    break;
                                case 'f':
                                    s.Append('\f');
                                    break;
                                case 'n':
                                    s.Append('\n');
                                    break;
                                case 'r':
                                    s.Append('\r');
                                    break;
                                case 't':
                                    s.Append('\t');
                                    break;
                                case 'u':
                                    var hex = new StringBuilder();

                                    for (int i = 0; i < 4; i++)
                                    {
                                        hex.Append(NextChar);
                                    }

                                    s.Append((char)Convert.ToInt32(hex.ToString(), 16));
                                    break;
                            }
                            break;
                        default:
                            s.Append(c);
                            break;
                    }
                }

                return s.ToString();
            }

            object ParseNumber()
            {
                string number = NextWord;

                if (number.IndexOf('.') == -1)
                {
                    long parsedInt;
                    Int64.TryParse(number, out parsedInt);
                    return parsedInt;
                }

                double parsedDouble;
                Double.TryParse(number, out parsedDouble);
                return parsedDouble;
            }

            void EatWhitespace()
            {
                while (WHITE_SPACE.IndexOf(PeekChar) != -1)
                {
                    json.Read();

                    if (json.Peek() == -1)
                    {
                        break;
                    }
                }
            }

            char PeekChar
            {
                get
                {
                    return Convert.ToChar(json.Peek());
                }
            }

            char NextChar
            {
                get
                {
                    return Convert.ToChar(json.Read());
                }
            }

            string NextWord
            {
                get
                {
                    StringBuilder word = new StringBuilder();

                    while (WORD_BREAK.IndexOf(PeekChar) == -1)
                    {
                        word.Append(NextChar);

                        if (json.Peek() == -1)
                        {
                            break;
                        }
                    }

                    return word.ToString();
                }
            }

            TOKEN NextToken
            {
                get
                {
                    EatWhitespace();

                    if (json.Peek() == -1)
                    {
                        return TOKEN.NONE;
                    }

                    char c = PeekChar;
                    switch (c)
                    {
                        case '{':
                            return TOKEN.CURLY_OPEN;
                        case '}':
                            json.Read();
                            return TOKEN.CURLY_CLOSE;
                        case '[':
                            return TOKEN.SQUARED_OPEN;
                        case ']':
                            json.Read();
                            return TOKEN.SQUARED_CLOSE;
                        case ',':
                            json.Read();
                            return TOKEN.COMMA;
                        case '"':
                            return TOKEN.STRING;
                        case ':':
                            return TOKEN.COLON;
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                        case '-':
                            return TOKEN.NUMBER;
                    }

                    string word = NextWord;

                    switch (word)
                    {
                        case "false":
                            return TOKEN.FALSE;
                        case "true":
                            return TOKEN.TRUE;
                        case "null":
                            return TOKEN.NULL;
                    }

                    return TOKEN.NONE;
                }
            }
        }

        /// <summary>
        /// Converts a IDictionary / IList object or a simple type (string, int, etc.) into a JSON string
        /// </summary>
        /// <param name="json">A Dictionary&lt;string, object&gt; / List&lt;object&gt;</param>
        /// <returns>A JSON encoded string, or null if object 'json' is not serializable</returns>
        public static string Serialize(JSON obj)
        {
            return Serializer.Serialize(obj);
        }

        sealed class Serializer
        {
            StringBuilder builder;

            Serializer()
            {
                builder = new StringBuilder();
            }

            public static string Serialize(JSON obj)
            {
                var instance = new Serializer();

                instance.SerializeValue(obj);

                return instance.builder.ToString();
            }

            void SerializeValue(object value)
            {
                if (value == null)
                {
                    builder.Append("null");
                }
                else if (value as string != null)
                {
                    SerializeString(value as string);
                }
                else if (value is bool)
                {
                    builder.Append(value.ToString().ToLower());
                }
                else if (value as JSON != null)
                {
                    SerializeObject(value as JSON);
                }
                else if (value as IDictionary != null)
                {
                    SerializeDictionary(value as IDictionary);
                }
                else if (value as IList != null)
                {
                    SerializeArray(value as IList);
                }
                else if (value is char)
                {
                    SerializeString(value.ToString());
                }
                else
                {
                    SerializeOther(value);
                }
            }

            void SerializeObject(JSON obj)
            {
                SerializeDictionary(obj.fields);
            }

            void SerializeDictionary(IDictionary obj)
            {
                bool first = true;

                builder.Append('{');

                foreach (object e in obj.Keys)
                {
                    if (!first)
                    {
                        builder.Append(',');
                    }

                    SerializeString(e.ToString());
                    builder.Append(':');

                    SerializeValue(obj[e]);

                    first = false;
                }

                builder.Append('}');
            }

            void SerializeArray(IList anArray)
            {
                builder.Append('[');

                bool first = true;

                foreach (object obj in anArray)
                {
                    if (!first)
                    {
                        builder.Append(',');
                    }

                    SerializeValue(obj);

                    first = false;
                }

                builder.Append(']');
            }

            void SerializeString(string str)
            {
                builder.Append('\"');

                char[] charArray = str.ToCharArray();
                foreach (var c in charArray)
                {
                    switch (c)
                    {
                        case '"':
                            builder.Append("\\\"");
                            break;
                        case '\\':
                            builder.Append("\\\\");
                            break;
                        case '\b':
                            builder.Append("\\b");
                            break;
                        case '\f':
                            builder.Append("\\f");
                            break;
                        case '\n':
                            builder.Append("\\n");
                            break;
                        case '\r':
                            builder.Append("\\r");
                            break;
                        case '\t':
                            builder.Append("\\t");
                            break;
                        default:
                            int codepoint = Convert.ToInt32(c);
                            if ((codepoint >= 32) && (codepoint <= 126))
                            {
                                builder.Append(c);
                            }
                            else
                            {
                                builder.Append("\\u" + Convert.ToString(codepoint, 16).PadLeft(4, '0'));
                            }
                            break;
                    }
                }

                builder.Append('\"');
            }

            void SerializeOther(object value)
            {
                if (value is float
                    || value is int
                    || value is uint
                    || value is long
                    || value is double
                    || value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is ulong
                    || value is decimal)
                {
                    builder.Append(value.ToString());
                }
                else
                {
                    SerializeString(value.ToString());
                }
            }
        }
    }
}
