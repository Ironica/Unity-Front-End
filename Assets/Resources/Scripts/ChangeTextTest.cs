using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class ChangeTextTest : MonoBehaviour
{

    public TMP_Text changingText; 
    
    public void TextChange()
    {
        changingText.text += "This finaly worked";
    }
}
