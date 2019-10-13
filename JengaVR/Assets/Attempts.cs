using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Attempts : MonoBehaviour {

    public int attempts;
    // Use this for initialization
    void Start()
    {
        attempts = 0;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "Attempts: " + attempts;
    }
}
