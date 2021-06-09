using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console_Button : MonoBehaviour
{
    public GameObject Console; 
    public Sprite[] consoleSprites;

    private Image consoleImage;

    private int consoleState; 
    // Start is called before the first frame update
    void Start()
    {
        consoleState = 0;
        consoleImage = GetComponent<Button>().image;
        consoleImage.sprite = consoleSprites[consoleState]; 

        gameObject.GetComponent<Button>().onClick.AddListener(TurnOnAndOff);
        
        Console.gameObject.SetActive(consoleState != 0);
    }

    private void TurnOnAndOff()
    {
        consoleState = 1 - consoleState;
        consoleImage.sprite = consoleSprites[consoleState];
        Console.gameObject.SetActive(consoleState != 0);
    }
}
