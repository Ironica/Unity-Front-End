using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //public string levelToLoad;
    public GameObject settingsWindow;
    public void StartGame()
    {
        ChangeSceneManagement.getCurrentScene();
        SceneManager.LoadScene("Scenes/Interface");
        StatData.setCurrent("map5");
    }
    public void Continue()
    {
      ChangeSceneManagement.getCurrentScene();
    }
    public void Settings()
    {
        ChangeSceneManagement.getCurrentScene();
        settingsWindow.SetActive(true);
    }

    public void CloseSettings()
    {
        ChangeSceneManagement.getCurrentScene();
        settingsWindow.SetActive(false);
    }
    public void Credits()
    {
      ChangeSceneManagement.getCurrentScene();

    }
}
