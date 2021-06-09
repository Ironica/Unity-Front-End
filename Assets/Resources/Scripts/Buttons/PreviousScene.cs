using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreviousScene : MonoBehaviour
{

    private int previousScene;

    // Start is called before the first frame update
    void Start()
    {
      previousScene = SceneManager.GetActiveScene().buildIndex-1;
    }

    // Update is called once per frame
    public void goToPreviousScene(){
      SceneManager.LoadScene(0);
    }
}
