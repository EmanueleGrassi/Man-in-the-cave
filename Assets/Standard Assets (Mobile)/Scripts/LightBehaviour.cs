using UnityEngine;
using System.Collections;

public class LightBehaviour : MonoBehaviour 
{
    bool rainbow=false;
	float speed=180f;
	Action colorAction = Action.saleBlue;
	// Use this for initialization
	void Start () 
    {       
		if (CameraScript.data.helmet == Helmet.white)
        {
            this.light.color = Color.white;
        }
        else if (CameraScript.data.helmet == Helmet.red)
        {
            this.light.color = Color.red;
        }
        else if (CameraScript.data.helmet == Helmet.blue)
        {
            this.light.color = Color.cyan;
        }
        else if (CameraScript.data.helmet == Helmet.pink)
        {
            this.light.color = Color.magenta;
        }
        else if (CameraScript.data.helmet == Helmet.green)
        {
            this.light.color = Color.green;
        }
		else if (CameraScript.data.helmet == Helmet.rainbow)
        {
            //if (Application.platform == RuntimePlatform.WP8Player)
            light.intensity = 0.01f;
            rainbow = true;            
        }
	}
	
	// Update is called once per frame	
	enum Action
	{
		saleBlue,
		scendeRosso,
		salegreen,
		scendiBlue,
		saliRosso,
		scendiGreen
	}
    void Update()
    {    
		if(rainbow)
		{
            switch (colorAction)
            {
                case Action.saleBlue:
                    this.light.color = new Color(255f, 0f, this.light.color.b + (speed * Time.deltaTime));
                    if (this.light.color.b >= 320)
                        colorAction = Action.scendeRosso;
                    break;
                case Action.scendeRosso:
                    this.light.color = new Color(this.light.color.r - (speed * Time.deltaTime), 0f, 255f);
                    if (this.light.color.r <= -80f)
                        colorAction = Action.salegreen;
                    break;
                case Action.salegreen:
                    this.light.color = new Color(0f, this.light.color.g + (speed * Time.deltaTime), 255f);
                    if (this.light.color.g >= 320f)
                        colorAction = Action.scendiBlue;
                    break;
                case Action.scendiBlue:
                    this.light.color = new Color(0f, 255f, this.light.color.b - (speed * Time.deltaTime));
                    if (this.light.color.b <= -80f)
                        colorAction = Action.saliRosso;
                    break;
                case Action.saliRosso:
                    this.light.color = new Color(this.light.color.r + (speed * Time.deltaTime), 255, 0);
                    if (this.light.color.r >= 320)
                        colorAction = Action.scendiGreen;
                    break;
                case Action.scendiGreen:
                    this.light.color = new Color(255, this.light.color.g - (speed * Time.deltaTime), 0);
                    if (this.light.color.g <= -80)
                        colorAction = Action.saleBlue;
                    break;

            }
		}
	}
}
