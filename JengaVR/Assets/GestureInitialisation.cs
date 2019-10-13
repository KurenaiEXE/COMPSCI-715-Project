using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureInitialisation : MonoBehaviour {
    public string letter;
	// Use this for initialization
	void Start () {
        GameObject gesture = (GameObject)Resources.Load("Gestures/" + letter);
        this.name = letter;
        
        GameObject gesture1 = Instantiate(gesture, new Vector3(0.5f, 1f, 0.5f), Quaternion.Euler(0, 45, 0));
        GameObject gesture2 = Instantiate(gesture, new Vector3(-0.1f, 1f,-0.3f ), Quaternion.Euler(0, -135, 0));
        gesture1.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        gesture2.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        gesture1.transform.localPosition = new Vector3(0.96f, 1.13f,-2.43f);
        gesture2.transform.localPosition = new Vector3(1.45f, 1.13f, -1.8f);
        gesture1.gameObject.transform.parent = transform;
        gesture2.gameObject.transform.parent = transform;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
