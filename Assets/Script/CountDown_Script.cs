using UnityEngine;
using System.Collections;

public class CountDown_Script : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        numero = 0; 
        guiText.fontSize = Screen.width / 10;
	}
    float CountDownTime = 0f;

    private byte _num = 5;
    private byte numero
    {
        get { return _num; }
        set 
        {
            if (_num != value)
            {
                _num = value;
                if (value == 0)
                    guiText.text = "survive!";
                else
                    guiText.text = value.ToString();
                //suona
            }        
        }
    }

    void Update()
    {
        if (CountDownTime >= 1.5)
            gameObject.SetActive(false);
        /*
        if (CountDownTime >= 1 && CountDownTime < 2)
            numero = 2;
        else if (CountDownTime >= 2 && CountDownTime < 3)
            numero = 1;
        else if (CountDownTime >= 3 && CountDownTime < 4.5)
            numero = 0;
        if (CountDownTime >= 4.5)
            gameObject.SetActive(false);*/

        CountDownTime += Time.deltaTime;
    }
}
