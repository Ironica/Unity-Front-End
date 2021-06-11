using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataChapter
{
  public string chapterName;
  public List<DataMap> maps;

  public DataChapter(string chapterName)
  {
    this.chapterName = chapterName;
    maps = new List<DataMap>();
  }
}
