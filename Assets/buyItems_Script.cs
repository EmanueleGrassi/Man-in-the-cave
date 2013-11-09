using UnityEngine;
using System.Collections;

public class buyItems_Script : MonoBehaviour {

    public Texture arcoLight, bengal, bluLight, greenLight, piccone, pinkLight, reborn, redLight;
    float size, margin;
    public GUISkin custom;
    // Use this for initialization
    void Start()
    {
        size = Screen.width / 20;
        margin = Screen.width / 60;
    }
    Vector2 position = Vector2.zero;

    void OnGUI()
    {
        GUI.skin = custom;
        custom.label.fontSize = (int)(size * 1.5);
        GUI.Label(new Rect(margin, margin, size * 10, size * 2 + margin), "Buy Items");
        custom.label.fontSize = (int)(size * 0.5f);
        GUI.Label(new Rect(margin, margin * 11 - 3, size * 6, size), "n Pickaxe: ");// AGGIUNGERE I PICKAXE
        GUI.Label(new Rect(size * 5, margin * 11 - 3, size * 6, size), "n Reborn: "); //AGGIUNGERE I REBORN
        if (GUI.Button(new Rect(margin, margin * 13, size * 9, size * 3), piccone))
            return;
        if (GUI.Button(new Rect(margin, margin * 13 + size * 3, size * 9, size * 3), reborn))
            return;
        position = GUI.BeginScrollView(new Rect(size * 10, margin * 7, size * 10, size * 10), position, new Rect(0, 0, size * 10, size * 16));
        if (GUI.Button(new Rect(0, 0, size * 9, size * 3), redLight))
            return;
        if (GUI.Button(new Rect(0, margin / 2 + size * 3, size * 9, size * 3), bluLight))
            return;
        if (GUI.Button(new Rect(0, margin + size * 6, size * 9, size * 3), greenLight))
            return;
        if (GUI.Button(new Rect(0, margin + margin/2 + size * 9, size * 9, size * 3), pinkLight))
            return;
        if (GUI.Button(new Rect(0, margin * 2 + size * 12, size * 9, size * 3), arcoLight))
            return;
        GUI.EndScrollView(); 
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            bool fInsideList = IsTouchInsideList(touch.position);
            print(fInsideList);

            if (touch.phase == TouchPhase.Moved && fInsideList)
            {
                position.y += touch.deltaPosition.y * 5;
            }
        }
    }

    bool IsTouchInsideList(Vector2 touchPos)
    {
        Vector2 screenPos = new Vector2(touchPos.x, touchPos.y);
        print("" + screenPos.x + " " + screenPos.y);
        Rect rAdjustedBounds = new Rect(size * 10, margin * 7, size * 10, size * 16);

        return rAdjustedBounds.Contains(screenPos);
    }

}
