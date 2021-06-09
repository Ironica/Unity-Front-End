using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreviousScene : MonoBehaviour
{

  // Update is called once per frame
  public void goToPreviousScene(){
    int scene = ChangeSceneManagement.getPreviousScene();
    ChangeSceneManagement.getCurrentScene();
    if(scene != SceneManager.GetActiveScene().buildIndex)
    {
      SceneManager.LoadScene(scene);
    }
  }
}
