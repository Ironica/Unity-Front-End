using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapChoice : MonoBehaviour
{
    public void changeChapter()
    {
      ChapterManagement.currentChapter = transform.parent.GetSiblingIndex();
    }

    public void changeMap()
    {
      StatData.setCurrent(transform.GetComponent<Text>().text);
    }

    public void quitToInterface()
    {
      SceneManager.LoadScene("Scenes/Interface");
    }
}
