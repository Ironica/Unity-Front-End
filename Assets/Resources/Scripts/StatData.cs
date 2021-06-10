

public static class StatData
{
    private static string currentMap= "MapWithPortal";
    public static bool isPlayable { get; set; }
    public static int indexStoreMusic { get; set; }

    public static string getCurrent()
    {
        return currentMap;
    }

    public static void  setCurrent(string change)
    {
        currentMap = change;
    }
}
