

public static class StatData
{
    private static string currentMap= "map5.json";

    public static string getCurrent()
    {
        return currentMap;
    }

    public static void  setCurrent(string change)
    {
        currentMap = change;
    }
}
