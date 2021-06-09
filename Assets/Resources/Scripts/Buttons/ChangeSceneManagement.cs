using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ChangeSceneManagement
{

    private static int previousScene = SceneManager.GetActiveScene().buildIndex;

    public static void getCurrentScene()
    {
        previousScene = SceneManager.GetActiveScene().buildIndex;
    }

    public static int getPreviousScene()
    {

        return previousScene;
    }
}