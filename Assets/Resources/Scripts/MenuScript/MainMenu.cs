using System;
using Resources.Scripts;
using Resources.Scripts.DataScript;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //public string levelToLoad;
    public GameObject settingsWindow;
    
    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/Interface");
    }
    public void Continue()
    {
        
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
        
    }

    private void OnApplicationQuit()
    {
        var shutdownApi = "simulatte/shutdown";
        new ShutDown(shutdownApi, Global.port).ShutDownOldServer();
    }
}
