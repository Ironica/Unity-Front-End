using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using Resources.Scripts.Utils;
using TMPro;


public class MapMenuLink : MonoBehaviour
{

  private string chapterPath = "Prefabs/MAP_MENU/Chapter_Button";
  private string mapButtonPath = "Prefabs/MAP_MENU/Map_Button1";

  private int currentChapter;
  private string currentMap;


  private void leftSideBook(DataChapter chapter)
  {
    // Debug.Log("leftSideBook");
    GameObject Map_Panel = transform.Find("Main_Panel").Find("Map_Panel").gameObject as GameObject;

    destroyLeftSideBook();

    float mapY = 0f;
    foreach(DataMap map in chapter.maps)
    {
      GameObject mapObject = Instantiate(UnityEngine.Resources.Load(mapButtonPath), Map_Panel.transform) as GameObject;
      mapObject.transform.position = mapObject.transform.position + new Vector3(0, mapY, 0);
      mapObject.transform.Find("Map_Name").GetComponent<TMP_Text>().text = map.name;
      mapY -= 0.09f * Screen.height;
    }
  }


  private void destroyLeftSideBook()
  {
    foreach (Transform child in transform.Find("Main_Panel").Find("Map_Panel"))
    {
      Destroy(child.gameObject);
    }
  }

  private void rightSideBook(DataMap map)
  {
    GameObject Map_Description = transform.Find("Main_Panel").Find("Map_Description").gameObject as GameObject;

    Map_Description.transform.Find("Map_Title").GetComponent<TMP_Text>().text = "Story: " + map.storyTitle;
    Map_Description.transform.Find("Map_Description").GetComponent<TMP_Text>().text = map.story;
    Map_Description.transform.Find("Goals_Title").GetComponent<TMP_Text>().text = "Goal: ";
    string goal;
    switch(map.goal){
      case Goals.GEM: goal = "You have to collect all the gems.";
      break;
      case Goals.SWITCHON: goal = "You have to put all the switches open.";
      break;
      case Goals.MONSTER: goal = "You have to beat all the monsters.";
      break;
      default: throw new Exception("MapMenuLink:: Unknown goal data");
    }
    Map_Description.transform.Find("Map_Goals").GetComponent<TMP_Text>().text = map.goalsTitle + " \n" + goal;
  }


  void Start()
  {
    ChapterManagement.chaptersConstruction();

    int chapterNumber = 1;
    float bookX = 0f;
    foreach(DataChapter chapter in ChapterManagement.chapters){
      GameObject chapter_Button = Instantiate(UnityEngine.Resources.Load(chapterPath), transform.Find("Main_Panel").Find("Chapter_Panel")) as GameObject;
      chapter_Button.transform.position = chapter_Button.transform.position + new Vector3(0,bookX,0);
      chapter_Button.transform.Find("Chapter_Name").GetComponent<TMP_Text>().text = "Chapter " + chapterNumber;
      bookX -= 0.09f * Screen.height;
      chapterNumber++;
    }

    currentChapter = ChapterManagement.currentChapter;

    leftSideBook(ChapterManagement.chapters[currentChapter]);
    //rigthSideBook(ChapterManagement.chapters[0].maps[0]);
  }

  void Update()
  {
    if (StatData.getCurrent() != currentMap)
    {
      currentMap = StatData.getCurrent();
      var ctx = ChapterManagement
        .chapters[currentChapter].maps.FirstOrDefault(e => e.name == currentMap)
        ?.Apply(e => rightSideBook(e));
      if (ctx == null)
      {
        Debug.Log("Map could not be load");
      }
    }

    if(ChapterManagement.currentChapter != currentChapter){
      currentChapter = ChapterManagement.currentChapter;
      leftSideBook(ChapterManagement.chapters[currentChapter]);
    }
  }
}
