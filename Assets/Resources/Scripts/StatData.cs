

public static class StatData
{
    private static string currentMap= "MapWithPortal";

    public static string getCurrent()
    {
        return currentMap;
    }

    public static void  setCurrent(string change)
    {
        currentMap = change;
    }
}
