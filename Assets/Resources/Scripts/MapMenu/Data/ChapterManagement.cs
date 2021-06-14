using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class ChapterManagement
{
  public static List<DataChapter> chapters = new List<DataChapter>();
  private static string path = "Assets/Resources/MapJson/DataMap/";

  public static int currentChapter = 0;

  public static void chaptersConstruction()
  {
    string[] files = Directory.GetFiles(path);
    foreach (string file in files){
      if(!file.Contains(".meta")){
        addToAChapter(getDataMap(file));
      }
    }
  }

  private static DataMap getDataMap (string file){

    var json = System.IO.File.ReadAllText(file);
    return JsonConvert.DeserializeObject<DataMap>(json);

  }

  private static bool contains(List<DataMap> maps, string mapName)
  {
    int i = 0;
    bool found = false;
    while(i<maps.Count && !found)
    {
      if(maps[i].name.Equals(mapName))
      {
        found = true;
      }
      i++;
    }
    return found;
  }

  private static void addToAChapter(DataMap map)
  {
    var chapterName = map.chapterFile;
    bool found = false;
    for(int i = 0; i<chapters.Count; i++)
    {
      if(chapters[i].chapterName.Equals(map.chapterFile))
      {
        if(!contains(chapters[i].maps, map.name))
        chapters[i].maps.Add(map);
        found = true;
      }
    }
    if(!found)
    {
      DataChapter newChapter = new DataChapter(map.chapterFile);
      newChapter.maps.Add(map);
      chapters.Add(newChapter);
    }
  }

}
