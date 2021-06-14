using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChoice : MonoBehaviour
{
    public void changeChapter()
    {
      ChapterManagement.currentChapter = transform.parent.GetSiblingIndex();
    }
}
