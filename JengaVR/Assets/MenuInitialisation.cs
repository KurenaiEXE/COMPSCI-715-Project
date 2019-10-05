using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuInitialisation : MonoBehaviour {
    public TextMeshPro A;
    public GameObject block;
    // Use this for initialization
    void Start() {
        var letter = block.gameObject.name;
        A.text = A.text + letter;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
