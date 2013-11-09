using UnityEngine;
using System.Collections;

public class Items_Script : MonoBehaviour {

    Vector2 position;
	float size, margin;
	int picconi,reborns;
	public Texture piccone,bengala,coin,blueligth,pinkligth,redligth,greenligth,ranbowligth,reborn;
    public GUISkin custom;
	// Use this for initialization
	void Start () {
		size = Screen.width / 20;
        margin = Screen.width / 60;
		picconi=CameraScript.data.NumberPickaxe;
		reborns=CameraScript.data.NumberReborn;
        position = Vector2.zero;
	}
	
	// Update is called once per frame
	void OnGUI () {
        GUI.skin = custom;
        position = GUI.BeginScrollView(new Rect(0,0, Screen.width, Screen.height), position, new Rect(0, 0, Screen.width, size*21));
		GUI.skin.label.fontSize = (int)(size * 1.5);
		GUI.Label(new Rect(margin, margin, size * 5, size * 2 + margin), "Items");
		GUI.skin.label.fontSize = (int)(size);
		GUI.Label(new Rect(margin, margin+size*2, size * 10, size * 2 + margin), "Ligths:");
		GUI.DrawTexture(new Rect(margin*5,margin+size*4,size*2,size*2),blueligth,ScaleMode.StretchToFill);
		GUI.DrawTexture(new Rect(margin*5+size*3,margin+size*4,size*2,size*2),pinkligth,ScaleMode.StretchToFill);
		GUI.DrawTexture(new Rect(margin*5+size*6,margin+size*4,size*2,size*2),redligth,ScaleMode.StretchToFill);
		GUI.DrawTexture(new Rect(margin*5+size*9,margin+size*4,size*2,size*2),greenligth,ScaleMode.StretchToFill);
		GUI.DrawTexture(new Rect(margin*5+size*12,margin+size*4,size*2,size*2),ranbowligth,ScaleMode.StretchToFill);
		GUI.Label(new Rect(margin, margin+size*7, size * 10, size * 2 + margin), "Pickaxes:");
		GUI.DrawTexture(new Rect(margin*5,margin+size*9,size*4,size*4),piccone,ScaleMode.StretchToFill);
		GUI.Label(new Rect(margin*5+size*5, margin+size*10, size * 10, size * 2 + margin), "x"+picconi);
		GUI.Label(new Rect(margin, margin+size*14, size * 10, size * 2 + margin), "Reborns:");
		GUI.DrawTexture(new Rect(margin*5,margin+size*16,size*4,size*4),reborn,ScaleMode.StretchToFill);
		GUI.Label(new Rect(margin*5+size*5, margin+size*17, size * 10, size * 2 + margin), "x"+reborns);
        GUI.EndScrollView();
	}

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Moved)
            {
                position.y += touch.deltaPosition.y * 3;
            }
        }
    }
}
