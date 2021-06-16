

using UnityEngine;

public static class StatData
{
    private static string currentMap= "Chap2-1";
    public static bool isPlayable { get; set; } //On/Off the test Music in the Store
    public static bool storeListening = false; // Disable Menu's music when listening a test music <3
    public static int musicTest; // Music you test on the store
    public static int musicUsed = 3; // Music that is used in the game
    public static string playerUsed = "frog";
    

    public static string getCurrent()
    {
        return currentMap;
    }

    public static void  setCurrent(string change)
    {
        currentMap = change;
    }
}
