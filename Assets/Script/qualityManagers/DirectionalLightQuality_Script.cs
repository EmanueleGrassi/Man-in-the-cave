using UnityEngine;
using System.Collections;

public class DirectionalLightQuality_Script : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        if (Application.platform == RuntimePlatform.WP8Player ||
           iPhone.generation == iPhoneGeneration.iPodTouch4Gen || iPhone.generation == iPhoneGeneration.iPodTouch3Gen ||
           iPhone.generation == iPhoneGeneration.iPodTouch2Gen || iPhone.generation == iPhoneGeneration.iPodTouch1Gen ||
           iPhone.generation == iPhoneGeneration.iPhone3GS || iPhone.generation == iPhoneGeneration.iPhone4 ||
           iPhone.generation == iPhoneGeneration.iPad2Gen || iPhone.generation == iPhoneGeneration.iPad1Gen)
        {
            light.shadows = LightShadows.None;
        }
	}
}
