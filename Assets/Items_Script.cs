using UnityEngine;
using System.Collections;

public class Items_Script : MonoBehaviour {

    Vector2 position;
	float size, margin;
	int numbengala,reborns;
	public Texture piccone,bengala,coin,blueligth,pinkligth,redligth,greenligth,ranbowligth,reborn,white;
    public GUISkin custom;
    int availableLights;
    bool draw;
	public AudioClip equipSound;
	// Use this for initialization
	void Start () {
		size = Screen.width / 20;
        margin = Screen.width / 60;
        numbengala = CameraScript.data.numBengala;
        reborns = CameraScript.data.NumberReborn;
        position = Vector2.zero;
        availableLights = 0;
        CameraScript.LoadData();
	}
	
	void OnGUI () {
        GUI.skin = custom;
        //Bengala e reborns
        GUI.skin.label.fontSize = (int)(size*0.8f);

        GUI.DrawTexture(new Rect(size * 10, margin*3, size*1.5f, size*1.5f), bengala, ScaleMode.ScaleToFit, false);
        GUI.Label(new Rect(size * 11.5f + margin, margin*3, size*3, size*1.5f ), "x" + numbengala);

        GUI.DrawTexture(new Rect(size * 14.5f, margin * 3, size * 1.5f, size * 1.5f), reborn, ScaleMode.ScaleToFit, false);
        GUI.Label(new Rect(size * 16 + margin, margin * 3, size * 3, size * 1.5f), "x" + reborns);

        //Items
		GUI.skin.label.fontSize = (int)(size * 1.5f);
		GUI.Label(new Rect(margin, margin, size * 5, size * 2 + margin), "Items");

        //LUCE EQUIPAGGIATA
		GUI.skin.label.fontSize = (int)(size);
		GUI.Label(new Rect(margin, size*4, size * 10, size * 2 + margin), "Light equipped");
        equippedLight();

        //LUCI DISPONIBILI
        GUI.Label(new Rect(margin, size * 6 +  margin, size * 10, size * 2 + margin), "Lights available");
        availableLight();
		if(Input.GetKey(KeyCode.Escape))
        {
            CameraScript.SaveData();
            Application.LoadLevel(0);
        }
		   
	}

    private void equippedLight()
    {
        if (CameraScript.data.helmet == Helmet.white)
        {
            GUI.DrawTexture(new Rect(size * 8, size * 4, size * 2, size * 2), white, ScaleMode.ScaleToFit, false);
            return;
        }
        if (CameraScript.data.helmet == Helmet.red)
        {
            GUI.DrawTexture(new Rect(size * 8, size * 4, size * 2, size * 2), redligth, ScaleMode.ScaleToFit, false);
            return;
        }
        if (CameraScript.data.helmet == Helmet.blue)
        {
            GUI.DrawTexture(new Rect(size * 8, size * 4, size * 2, size * 2), blueligth, ScaleMode.ScaleToFit, false);
            return;
        }
        if (CameraScript.data.helmet == Helmet.green)
        {
            GUI.DrawTexture(new Rect(size * 8, size * 4, size * 2, size * 2), greenligth, ScaleMode.ScaleToFit, false);
            return;
        }
        if (CameraScript.data.helmet == Helmet.pink)
        {
            GUI.DrawTexture(new Rect(size * 8, size * 4, size * 2, size * 2), pinkligth, ScaleMode.ScaleToFit, false);
            return;
        }
        if (CameraScript.data.helmet == Helmet.rainbow)
        {
            GUI.DrawTexture(new Rect(size * 8, size * 4, size * 2, size * 2), ranbowligth, ScaleMode.ScaleToFit, false);
            return;
        }
    }

    private void availableLight()
    {
        custom.button.fontSize = (int)(size / 2);
        custom.button.normal.textColor = Color.white;
        availableLights = 0;
        if (CameraScript.data.helmet != Helmet.white)
        {
            GUI.DrawTexture(new Rect(margin + availableLights * (size * 2 + margin), size * 9 -2*margin, size * 2, size * 2), white, ScaleMode.ScaleToFit, false);
            if (GUI.Button(new Rect(margin + availableLights * (size * 2 + margin), size * 10 , size * 2, size * 2), "Equip"))
			{
                CameraScript.data.helmet = Helmet.white;
				audio.PlayOneShot(equipSound);
			}
            availableLights++;
        }
        if (CameraScript.data.lightRed && CameraScript.data.helmet != Helmet.red)
        {
            GUI.DrawTexture(new Rect(margin + availableLights * (size * 2 + margin), size * 9 - 2*margin, size * 2, size * 2), redligth, ScaleMode.ScaleToFit, false);
            if (GUI.Button(new Rect(margin + availableLights * (size * 2 + margin), size * 10 , size * 2, size * 2), "Equip"))
			{
                CameraScript.data.helmet = Helmet.red;
				audio.PlayOneShot(equipSound);
			}
            availableLights++;
        }
        if (CameraScript.data.lightBlue && CameraScript.data.helmet != Helmet.blue)
        {
            GUI.DrawTexture(new Rect(margin + availableLights * (size * 2 + margin), size * 9 - 2*margin, size * 2, size * 2), blueligth, ScaleMode.ScaleToFit, false);
            if (GUI.Button(new Rect(margin + availableLights * (size * 2 + margin), size * 10 , size * 2, size * 2), "Equip"))
			{
                CameraScript.data.helmet = Helmet.blue;
				audio.PlayOneShot(equipSound);
			}
            availableLights++;
        }
        if (CameraScript.data.lightGreen && CameraScript.data.helmet != Helmet.green)
        {
            GUI.DrawTexture(new Rect(margin + availableLights * (size * 2 + margin), size * 9 - 2*margin, size * 2, size * 2), greenligth, ScaleMode.ScaleToFit, false);
            if (GUI.Button(new Rect(margin + availableLights * (size * 2 + margin), size * 10 , size * 2, size * 2), "Equip"))
			{
                CameraScript.data.helmet = Helmet.green;
				audio.PlayOneShot(equipSound);
			}
            availableLights++;
        }
        if (CameraScript.data.lightPink && CameraScript.data.helmet != Helmet.pink)
        {
            GUI.DrawTexture(new Rect(margin + availableLights * (size * 2 + margin), size * 9 - 2*margin, size * 2, size * 2), pinkligth, ScaleMode.ScaleToFit, false);
            if (GUI.Button(new Rect(margin + availableLights * (size * 2 + margin), size * 10, size * 2, size * 2), "Equip"))
			{
                CameraScript.data.helmet = Helmet.pink;
				audio.PlayOneShot(equipSound);
			}
            availableLights++;
        }
        if (CameraScript.data.lightRainbow && CameraScript.data.helmet != Helmet.rainbow)
        {
            GUI.DrawTexture(new Rect(margin + availableLights * (size * 2 + margin), size * 9 - 2*margin, size * 2, size * 2), ranbowligth, ScaleMode.ScaleToFit, false);
            if (GUI.Button(new Rect(margin + availableLights * (size * 2 + margin), size * 10, size * 2, size * 2), "Equip"))
			{
                CameraScript.data.helmet = Helmet.rainbow;
				audio.PlayOneShot(equipSound);
			}
            availableLights++;
        }
        if (availableLights == 0)
        {
            custom.button.fontSize = (int)(size*1.7f);
            custom.button.normal.textColor = Color.white;
            if(GUI.Button(new Rect(size * 3 , size * 8 , size * 14, size * 4), "Buy an helmet!"))
            {
                CameraScript.SaveData();
                Application.LoadLevel(2);
            }
        }
    }
}
