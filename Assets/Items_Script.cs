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
	// Use this for initialization
	void Start () {
		size = Screen.width / 20;
        margin = Screen.width / 60;
        numbengala = CameraScript.data.numBengala;
        reborns = CameraScript.data.NumberReborn;
        position = Vector2.zero;
        availableLights = 0;
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
        GUI.Label(new Rect(margin, size * 6 + 2 * margin, size * 10, size * 2 + margin), "Lights available");
        availableLight();
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
        availableLights = 0;
        if (CameraScript.data.helmet != Helmet.white)
        {
            GUI.DrawTexture(new Rect(margin + availableLights * (size * 2 + margin), size * 9, size * 2, size * 2), white, ScaleMode.ScaleToFit, false);
            if (GUI.Button(new Rect(margin + availableLights * (size * 2 + margin), size * 11 + margin, size * 2, size * 2), "Equip"))
                CameraScript.data.helmet = Helmet.white;
            availableLights++;
        }
        if (CameraScript.data.lightRed)
        {
            GUI.DrawTexture(new Rect(margin + availableLights * (size * 2 + margin), size * 9, size * 2, size * 2), redligth, ScaleMode.ScaleToFit, false);
            if (GUI.Button(new Rect(margin + availableLights * (size * 2 + margin), size * 11 + margin, size * 2, size * 2), "Equip"))
                CameraScript.data.helmet = Helmet.red;
            availableLights++;
        }
        if (CameraScript.data.lightBlue)
        {
            GUI.DrawTexture(new Rect(margin + availableLights * (size * 2 + margin), size * 9, size * 2, size * 2), blueligth, ScaleMode.ScaleToFit, false);
            if (GUI.Button(new Rect(margin + availableLights * (size * 2 + margin), size * 11 + margin, size * 2, size * 2), "Equip"))
                CameraScript.data.helmet = Helmet.blue;
            availableLights++;
        }
        if (CameraScript.data.lightGreen)
        {
            GUI.DrawTexture(new Rect(margin + availableLights * (size * 2 + margin), size * 9, size * 2, size * 2), greenligth, ScaleMode.ScaleToFit, false);
            if (GUI.Button(new Rect(margin + availableLights * (size * 2 + margin), size * 11 + margin, size * 2, size * 2), "Equip"))
                CameraScript.data.helmet = Helmet.green;
            availableLights++;
        }
        if (CameraScript.data.lightPink)
        {
            GUI.DrawTexture(new Rect(margin + availableLights * (size * 2 + margin), size * 9, size * 2, size * 2), pinkligth, ScaleMode.ScaleToFit, false);
            if (GUI.Button(new Rect(margin + availableLights * (size * 2 + margin), size * 11 + margin, size * 2, size * 2), "Equip"))
                CameraScript.data.helmet = Helmet.pink;
            availableLights++;
        }
        if (CameraScript.data.lightRainbow)
        {
            GUI.DrawTexture(new Rect(margin + availableLights * (size * 2 + margin), size * 9, size * 2, size * 2), ranbowligth, ScaleMode.ScaleToFit, false);
            if (GUI.Button(new Rect(margin + availableLights * (size * 2 + margin), size * 11 + margin, size * 2, size * 2), "Equip"))
                CameraScript.data.helmet = Helmet.rainbow;
            availableLights++;
        }
    }
}
