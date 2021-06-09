using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //public string levelToLoad;
    public GameObject settingsWindow;
    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/Interface");
        StatData.setCurrent("map5");
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
}
