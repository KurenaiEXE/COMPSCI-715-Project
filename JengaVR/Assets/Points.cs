using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Points : MonoBehaviour {
    public int points;
	// Use this for initialization
	void Start () {
        points = 0;
         
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<TextMeshProUGUI>().text = "Points: " + points;
    }
}
