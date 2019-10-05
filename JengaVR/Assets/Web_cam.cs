using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Web_cam : MonoBehaviour {
    int currentCamIndex = 0;
    WebCamTexture tex;
    public RawImage display;

    public void StartStopCam_Clicked()
    {

    }
	// Use this for initialization
	void Start () {
        if(tex!= null)
        {
            display.texture = null;
            tex.Stop();
            tex = null;
        }
        else
        {
            WebCamDevice device = WebCamTexture.devices[currentCamIndex];
            tex = new WebCamTexture(device.name);
            display.texture = tex;
            tex.Play();
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
