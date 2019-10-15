using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Initialisation : MonoBehaviour {
    public TextMeshPro A;
    public TextMeshPro B;
    public bool move =false;
    public int count;
    public int movementspeed;
    public int jitter;
    public GameObject gameover;
	// Use this for initialization
	void Start () {
        string st = "OADBIMKL";
        string c = st[Random.Range(0,st.Length)].ToString();
        A.text=c;
        B.text = c;
        this.name = c;
	}
	
	// Update is called once per frame
	void Update () {
        if (move && count < 20) 
        {
            float rotation = gameObject.transform.eulerAngles.y;
            Vector3 position = gameObject.transform.position;
            
            Debug.Log(position.x);
            Debug.Log(rotation);
            if (Mathf.Abs(Mathf.Abs(rotation) - 90) < Mathf.Abs(rotation - 0) && Mathf.Abs(Mathf.Abs(rotation) - 90) < Mathf.Abs(Mathf.Abs(rotation) - 360))
            {
                //rotation = 90
                Debug.Log("90");
                if (Mathf.Abs(position.z - 0) < Mathf.Abs(Mathf.Abs(position.z) - 0.5f))
                {
                    //in the middle
                    Debug.Log("Middle");
                    transform.Translate(transform.right * (Time.deltaTime * (movementspeed)));
                }
                else
                {
                    if (Mathf.Sign(position.z) == -1)
                    {
                        Debug.Log("Left");
                        transform.Translate(transform.forward * (Time.deltaTime * (movementspeed)));
                    }
                    else
                    {
                        Debug.Log("Right");
                        transform.Translate(-transform.forward * (Time.deltaTime * (movementspeed)));

                    }
                }
            }
            else
            {
                //rotation = 0
                Debug.Log("0");
                if (Mathf.Abs(position.x - 0) < Mathf.Abs(Mathf.Abs(position.x) - 0.5f))
                {
                    //in the middle
                    Debug.Log("Middle");
                    transform.Translate(transform.forward * (Time.deltaTime * (movementspeed)));
                }
                else
                {
                    if (Mathf.Sign(position.x) == -1)
                    {
                        Debug.Log("Left");
                        transform.Translate(-transform.right * (Time.deltaTime * (movementspeed)));
                    }
                    else
                    {
                        Debug.Log("Right");
                        transform.Translate(transform.right * (Time.deltaTime * (movementspeed)));
                    }
                }
            }
            transform.Translate(transform.up * (Time.deltaTime * Random.Range(-jitter,jitter)));
            count += 1;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(!move && Time.timeSinceLevelLoad > 5 && collision.gameObject.name == "Floor" )
        {
            Instantiate(gameover, new Vector3(0,0,0), Quaternion.Euler(0, 0, 0));
        }
    }
}
