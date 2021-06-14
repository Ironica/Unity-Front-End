

public static class StatData
{
    private static string currentMap= "Chap4-1";
    public static bool isPlayable = false;
    public static int indexStoreMusic = 0;

    public static string getCurrent()
    {
        return currentMap;
    }

    public static void  setCurrent(string change)
    {
        currentMap = change;
    }
}
