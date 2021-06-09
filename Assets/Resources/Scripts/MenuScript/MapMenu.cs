using System.Collections;
using System.Collections.Generic;
using Resources.Scripts;
using Resources.Scripts.DataScript;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapMenu : MonoBehaviour
{
 public void map5()
 {
  StatData.setCurrent("map5");
  SceneManager.LoadScene("Interface");
 }

 public void mapDylan()
 {
  StatData.setCurrent("MapTestDylan");
  SceneManager.LoadScene("Interface");
 }

 public void mapTest()
 {
  StatData.setCurrent("Test");
  SceneManager.LoadScene("Interface");
 }

 private void OnApplicationQuit()
 {
  var shutdownApi = "simulatte/shutdown";
  new ShutDown(shutdownApi, Global.port).ShutDownOldServer();
 }
}
