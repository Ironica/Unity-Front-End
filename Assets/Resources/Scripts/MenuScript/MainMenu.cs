using System.Linq;
using Resources.Scripts;
using Resources.Scripts.DataScript;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //public string levelToLoad;
    public void Continue()
    {
        ChangeSceneManagement.getCurrentScene();
        SceneManager.LoadScene("Scenes/New_Interface");
        //StatData.setCurrent("map5");
    }
    public void Store()
    {
      ChangeSceneManagement.getCurrentScene();
      SceneManager.LoadScene("Scenes/New_Store");
    }

    public void MapMenu()
    {
        ChangeSceneManagement.getCurrentScene();
        SceneManager.LoadScene("Scenes/New_Map_Menu");
    }
    public void Settings()
    {
        SceneManager.LoadScene("Scenes/New_Settings");
    }
    
    public void Credits()
    {
      ChangeSceneManagement.getCurrentScene();
      SceneManager.LoadScene("Scenes/New_Credit");
    }

    public void Help()
    {
        ChangeSceneManagement.getCurrentScene();
        SceneManager.LoadScene("Scenes/New_Help");
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
