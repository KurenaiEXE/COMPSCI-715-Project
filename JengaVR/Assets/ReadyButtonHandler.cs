using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.UI;
using System.IO;
using System.Net;
using System;
using System.Linq;
using TMPro;

namespace VRStandardAssets.Examples
{
    // This script is a simple example of how an interactive item can
    // be used to change things on gameobjects by handling events.
    public class ReadyButtonHandler : MonoBehaviour
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

        public WebCamTexture wct;
        public RawImage display;
        public GameObject pointer;
        public bool accept=false;
        public float totalTime = 2; 
        public bool over=false;
        public float timer;
        public string alphabet = "ABCDEFGHI";
        public GameObject cancelButton;
        public TextMeshPro text;
        public int attempts;
        public float accuracy;
        public const int MAX_ATTEMPTS = 5;


        private void Start()
        {
            pointer = GameObject.Find("Canvas/Image");

        }
        private void Update()
        {
            if (over)
            {
                timer += Time.deltaTime;
                pointer.GetComponent<Image>().fillAmount = 1-timer / totalTime;
            }
            if (timer > totalTime)
            {
                HandleClick();
            }
        }

        private void Awake()
        {
            m_Renderer.material = m_NormalMaterial;
            int currentCamIndex = 0;
            WebCamDevice device = WebCamTexture.devices[currentCamIndex];
            wct = new WebCamTexture(device.name, 400, 300, 12);
            display.texture = wct;
            wct.Play();
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
            Debug.Log("Show over state");
            m_Renderer.material = m_OverMaterial;
            over = true;
        }


        //Handle the Out event
        private void HandleOut()
        {
            Debug.Log("Show out state");
            m_Renderer.material = m_NormalMaterial;
            over = false;
            timer = 0;
            pointer.GetComponent<Image>().fillAmount = 1;
        }


        //Handle the Click event
        private void HandleClick()
        {
            Debug.Log("Show click state");
            m_Renderer.material = m_ClickedMaterial;
            pointer.GetComponent<Image>().fillAmount = 1;
            timer = 0;
            over = false;
            if (!accept)
            {
                TakeSnapshot();

                string[] recognitionResultsString = GestureRecognition();
                Debug.Log(string.Join(",", recognitionResultsString));
                float[] recognitionResults = new float[recognitionResultsString.Length];
                for (int i = 0; i < recognitionResultsString.Length; i++)
                {
                    recognitionResults[i] = float.Parse(recognitionResultsString[i]);
                }

                int maxIndex = recognitionResults.ToList().IndexOf(recognitionResults.Max());
                string letter = transform.parent.gameObject.GetComponent<MenuInitialisation>().letter;
                accuracy = recognitionResults[alphabet.IndexOf(letter)];
                GameObject.Find("Canvas/Panel/Accuracy").gameObject.GetComponent<Accuracy>().accuracy = Mathf.RoundToInt(accuracy * 100);

                text.GetComponent<TextMeshPro>().text = "Please perform the gesture for: " + letter + "The gesture you have performed is: " + alphabet[maxIndex];
                this.gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Submit";
                cancelButton.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Try Again";
                accept = true;
            }
            else
            {
   
                wct.Stop();
                transform.parent.gameObject.GetComponent<MenuInitialisation>().block.gameObject.GetComponent<Initialisation>().move = true;
                GameObject.Find("Canvas/Panel/Points").gameObject.GetComponent<Points>().points += Mathf.RoundToInt(100 * accuracy*(1-(float)attempts/MAX_ATTEMPTS)) ;

                Destroy(transform.parent.gameObject);
            }
            
            
            

            

        }


        //Handle the DoubleClick event
        private void HandleDoubleClick()
        {
            Debug.Log("Show double click");
            m_Renderer.material = m_DoubleClickedMaterial;
        }


        //take photo from webcam
        public Texture2D heightmap;
        public Vector3 size = new Vector3(100, 10, 100);
        private string _SavePath = "C:/WebcamSnaps/";
        int _CaptureCounter = 0; 

        public void TakeSnapshot()
        {
                       
            RenderTexture PhotoTaken = new RenderTexture(wct.width, wct.height, 0); Graphics.Blit(wct, PhotoTaken);
            Texture2D snap = new Texture2D(wct.width, wct.height);
            snap.ReadPixels(new Rect(0,0,PhotoTaken.width, PhotoTaken.height),0,0);
            snap.Apply();
            System.IO.File.WriteAllBytes(_SavePath +"0.png", snap.EncodeToPNG());
            ++_CaptureCounter;
        }
        public string[] GestureRecognition()
        {

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:5000/predict");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"image\":\"C:/WebcamSnaps/0.png\", \"class\":0}";
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result.Substring(1, result.Length - 2).Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            } 

        }
    }

}