using Resources.Scripts;
using Resources.Scripts.DataScript;
using UnityEngine;

public class CommonSceneQuittingHelper: MonoBehaviour
{
    private void OnApplicationQuit()
    {
        var shutdownApi = "simulatte/shutdown";
        new ShutDown(shutdownApi, Global.port).ShutDownOldServer();
    }
}