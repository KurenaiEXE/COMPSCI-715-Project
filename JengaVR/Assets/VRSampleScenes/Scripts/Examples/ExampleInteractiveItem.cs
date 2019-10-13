using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.UI;

namespace VRStandardAssets.Examples
{
    // This script is a simple example of how an interactive item can
    // be used to change things on gameobjects by handling events.
    public class ExampleInteractiveItem : MonoBehaviour
    {
        [SerializeField] private Material m_NormalMaterial;                
        [SerializeField] private Material m_OverMaterial;                  
        [SerializeField] private Material m_ClickedMaterial;               
        [SerializeField] private Material m_DoubleClickedMaterial;         
        [SerializeField] private VRInteractiveItem m_InteractiveItem;
        [SerializeField] private Renderer m_Renderer;

        public GameObject menu;
        public GameObject pointer;
        public float totalTime = 2;
        public bool over;
        public float timer;
        public GameObject gesture;

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
        private void Awake ()
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
            over = false;
            timer = 0;
            pointer.GetComponent<Image>().fillAmount = 1;
            Debug.Log("Show out state");
            m_Renderer.material = m_NormalMaterial;
        }


        //Handle the Click event
        private void HandleClick()
        {
            Debug.Log("Show click state");
            over = false;
            m_Renderer.material = m_ClickedMaterial;
            menu.gameObject.GetComponent<MenuInitialisation>().block = this.gameObject;
            Instantiate(menu, new Vector3(0.4f, 2, -1.4f), Quaternion.Euler(0, -45, 0));
            gesture.gameObject.GetComponent<GestureInitialisation>().letter = this.name;
            timer = 0;
            pointer.GetComponent<Image>().fillAmount = 1;
            Instantiate(gesture, new Vector3(0,0,0), Quaternion.Euler(0, 0, 0));
        }


        //Handle the DoubleClick event
        private void HandleDoubleClick()
        {
            Debug.Log("Show double click");
            m_Renderer.material = m_DoubleClickedMaterial;
        }
    }

}