using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Resources.Scripts.MenuScript.Interface_Scripts
{
    public class TestText : MonoBehaviour
    {

        public Text UserCode;
        
        void Start()
        {
            gameObject.GetComponent<Button>().onClick.AddListener(FillText);
        }

        private void FillText()
        {
            UserCode.text += "It works";
        }
    }
}