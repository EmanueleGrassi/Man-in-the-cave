using UnityEngine;
using System.Collections;

public class ShowFPS : MonoBehaviour {

    GUIText gui;
    float updateInterval = 1.0f;
    double lastInterval;
    int frames;

	// Use this for initialization
	void Start () {
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;
	}

    void OnDisable()
    {
        if (gui)
            DestroyImmediate(gui.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        ++frames;
        float timeNow = Time.realtimeSinceStartup;
        if (timeNow > lastInterval + updateInterval)
        {
            if (!gui)
            {
                GameObject go = new GameObject("FPS Display", typeof(GUIText));
                go.hideFlags = HideFlags.HideAndDontSave;
                go.transform.position = new Vector3(0, 0, 0);
                gui = go.guiText;
                gui.pixelOffset = new Vector2(5, 55);
            }
            float fps = (float)(frames / (timeNow - lastInterval));
            float ms = 1000.0f / Mathf.Max(fps, 0, 00001);
            gui.text = ms.ToString("f1") + "ms " + fps.ToString("f2") + "FPS";
            frames = 0;
            lastInterval = timeNow;
        }
	}
}
