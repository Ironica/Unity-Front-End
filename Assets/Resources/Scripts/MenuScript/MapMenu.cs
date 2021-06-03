using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapMenu : MonoBehaviour
{
 public void map5()
 {
  StatData.setCurrent("map5.json");
  SceneManager.LoadScene("Interface");
 }

 public void mapDylan()
 {
  StatData.setCurrent("MapTestDylan.json");
  SceneManager.LoadScene("Interface");
 }

 public void mapTest()
 {
  StatData.setCurrent("Test.json");
  SceneManager.LoadScene("Interface");
 }
}
