using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuInitialisation : MonoBehaviour {
    public TextMeshPro A;
    public GameObject block;
    // Use this for initialization
    void Start() {
        var letter = transform.parent.GetComponent<Text>();
        A.text = A.text + letter;
        this.block = block;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
