using System.Linq;
using Resources.Scripts;
using Resources.Scripts.DataScript;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //public string levelToLoad;
    public GameObject settingsWindow;
    public void Continue()
    {
        ChangeSceneManagement.getCurrentScene();
        SceneManager.LoadScene("Scenes/Interface");
        //StatData.setCurrent("map5");
    }
    public void Store()
    {
      ChangeSceneManagement.getCurrentScene();
      SceneManager.LoadScene("Scenes/Store");
    }

    public void MapMenu()
    {
        ChangeSceneManagement.getCurrentScene();
        SceneManager.LoadScene("Scenes/Map_Menu");
    }
    public void Settings()
    {
        settingsWindow.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsWindow.SetActive(false);
    }
    public void Credits()
    {
      ChangeSceneManagement.getCurrentScene();
      // TODO add credit scene
    }

    public void Help()
    {
        ChangeSceneManagement.getCurrentScene();
        // TODO add help scene
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        var shutdownApi = "simulatte/shutdown";
        new ShutDown(shutdownApi, Global.port).ShutDownOldServer();
    }
}
