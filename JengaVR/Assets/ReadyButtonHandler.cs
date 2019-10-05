using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.UI;

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
        private void Awake()
        {
            m_Renderer.material = m_NormalMaterial;
            int currentCamIndex = 0;
            WebCamDevice device = WebCamTexture.devices[currentCamIndex];
            wct = new WebCamTexture(device.name, 400, 300, 12);
            Debug.Log("found");
            display.texture = wct;
            Debug.Log("textujre)");
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
        }


        //Handle the Out event
        private void HandleOut()
        {
            Debug.Log("Show out state");
            m_Renderer.material = m_NormalMaterial;
        }


        //Handle the Click event
        private void HandleClick()
        {
            Debug.Log("Show click state");
            m_Renderer.material = m_ClickedMaterial;

            TakeSnapshot();
            wct.Stop();
            transform.parent.gameObject.GetComponent<MenuInitialisation>().block.gameObject.GetComponent<Initialisation>().move = true;
            GameObject.Find("Canvas/Panel/Points").gameObject.GetComponent<Points>().points += 1;
            Destroy(transform.parent.gameObject);

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

        void TakeSnapshot()
        {
                       
            RenderTexture PhotoTaken = new RenderTexture(wct.width, wct.height, 0); Graphics.Blit(wct, PhotoTaken);
            Texture2D snap = new Texture2D(wct.width, wct.height);
            snap.ReadPixels(new Rect(0,0,PhotoTaken.width, PhotoTaken.height),0,0);
            snap.Apply();
            System.IO.File.WriteAllBytes(_SavePath + wct.name + _CaptureCounter.ToString() + ".png", snap.EncodeToPNG());
            ++_CaptureCounter;
        }
    }

}