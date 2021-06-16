using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapHandler : MonoBehaviour
{

  public void goToPreviousScene(){
    StatData.storeListening = false;
    StatData.isPlayable = false;
    int scene = ChangeSceneManagement.getPreviousScene();
    ChangeSceneManagement.getCurrentScene();
    if(scene != SceneManager.GetActiveScene().buildIndex)
    {
      SceneManager.LoadScene(scene);
    }
  }

  public void goToStore(){
    ChangeSceneManagement.getCurrentScene();
    SceneManager.LoadScene("Scenes/New_Store");
  }

  public void goHome(){
    ChangeSceneManagement.getCurrentScene();
    SceneManager.LoadScene("Scenes/New_Menu");
    StatData.storeListening = false;
    StatData.isPlayable = false;
  }

  public void goMapMenu()
  {
    ChangeSceneManagement.getCurrentScene();
    SceneManager.LoadScene("Scenes/New_Map_Menu");
  }
}
