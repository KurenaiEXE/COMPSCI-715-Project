using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.UI;
using System.Linq;
using TMPro;

namespace VRStandardAssets.Examples
{
    // This script is a simple example of how an interactive item can
    // be used to change things on gameobjects by handling events.
    public class CancelButtonHandler : MonoBehaviour
    {
        [SerializeField]
        private Material m_NormalMaterial;
        [SerializeField]
        private Material m_OverMaterial;
        [SerializeField]
        private Material m_ClickedMaterial;
        [SerializeField]
        private Material m_DoubleClickedMaterial;
        [SerializeField]
        private VRInteractiveItem m_InteractiveItem;
        [SerializeField]
        private Renderer m_Renderer;

        public GameObject pointer;
        public float totalTime = 2;
        public bool over;
        public float timer;
        public GameObject readybutton;

        private void Start()
        {
            pointer = GameObject.Find("Canvas/Image");
        }
        private void Update()
        {
            if (over)
            {
                timer += Time.deltaTime;
                pointer.GetComponent<Image>().fillAmount = 1 - timer / totalTime;
            }
            if (timer > totalTime)
            {
                HandleClick();
            }
        }

        private void Awake()
        {
            m_Renderer.material = m_NormalMaterial;
        }


        private void OnEnable()
        {
            m_InteractiveItem.OnOver += HandleOver;
            m_InteractiveItem.OnOut += HandleOut;
            m_InteractiveItem.OnClick += HandleClick;
            m_InteractiveItem.OnDoubleClick += HandleDoubleClick;
        }


        private void OnDisable()
        {
            m_InteractiveItem.OnOver -= HandleOver;
            m_InteractiveItem.OnOut -= HandleOut;
            m_InteractiveItem.OnClick -= HandleClick;
            m_InteractiveItem.OnDoubleClick -= HandleDoubleClick;
        }


        //Handle the Over event
        private void HandleOver()
        {
            over = true;
            Debug.Log("Show over state");
            m_Renderer.material = m_OverMaterial;
        }


        //Handle the Out event
        private void HandleOut()
        {
            Debug.Log("Show out state");
            m_Renderer.material = m_NormalMaterial;
            pointer.GetComponent<Image>().fillAmount = 1;
            timer = 0;
            over = false;

        }


        //Handle the Click event
        private void HandleClick()
        {
            Debug.Log("Show click state");
            m_Renderer.material = m_ClickedMaterial;
            pointer.GetComponent<Image>().fillAmount = 1;
            timer = 0;
            over = false;
            if (!readybutton.GetComponent<ReadyButtonHandler>().accept)
            {
                readybutton.GetComponent<ReadyButtonHandler>().wct.Stop();
                Destroy(transform.parent.gameObject);
            }
            else
            {
                ReadyButtonHandler readyScript = readybutton.GetComponent<ReadyButtonHandler>();

                readyScript.attempts += 1;
                readyScript.TakeSnapshot();
                string[] recognitionResultsString = readyScript.GestureRecognition();
                Debug.Log(string.Join(",", recognitionResultsString));

                float[] recognitionResults = new float[recognitionResultsString.Length];
                for (int i = 0; i < recognitionResultsString.Length; i++)
                {
                    recognitionResults[i] = float.Parse(recognitionResultsString[i]);
                }
                int maxIndex = recognitionResults.ToList().IndexOf(recognitionResults.Max());
                string letter = transform.parent.gameObject.GetComponent<MenuInitialisation>().letter;
                readyScript.accuracy = recognitionResults[readyScript.alphabet.IndexOf(letter)];
                GameObject.Find("Canvas/Panel/Accuracy").gameObject.GetComponent<Accuracy>().accuracy = Mathf.RoundToInt(readyScript.accuracy * 100);
                GameObject.Find("Canvas/Panel/Attempts").gameObject.GetComponent<Attempts>().attempts = readyScript.attempts;
                readyScript.text.GetComponent<TextMeshPro>().text = "The gesture you have performed is:" + readyScript.alphabet[maxIndex] ;
            }  

        }


        //Handle the DoubleClick event
        private void HandleDoubleClick()
        {
            Debug.Log("Show double click");
            m_Renderer.material = m_DoubleClickedMaterial;
        }
    }

}