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
}
