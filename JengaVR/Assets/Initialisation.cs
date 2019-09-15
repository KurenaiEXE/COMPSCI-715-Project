using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Initialisation : MonoBehaviour {
    public TextMeshPro A;
    public TextMeshPro B;
	// Use this for initialization
	void Start () {
        string st = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string c = st[Random.Range(0,st.Length)].ToString();
        A.text=c;
        B.text = c;
        this.name = c;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
