using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Accuracy : MonoBehaviour {

    public int accuracy;
    // Use this for initialization
    void Start()
    {
        accuracy = 0;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "Accuracy " + accuracy;
    }
}
