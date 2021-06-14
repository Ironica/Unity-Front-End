using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class MapMenuLink : MonoBehaviour
{

  private string bookPath = "Prefabs/MAP_MENU/Book";
  private string mapButtonPath = "Prefabs/MAP_MENU/Map_Button";

  private int currentChapter;
  private string currentMap;

  private void leftSideBook(DataChapter chapter)
  {
    Debug.Log("leftSideBook");
    GameObject openedBook = transform.Find("Background").Find("Opened_Book").gameObject as GameObject;

    openedBook.transform.Find("Chapter_Name").GetComponent<Text>().text = chapter.chapterName;
    float mapY = 0f;
    foreach(DataMap map in chapter.maps)
    {
      GameObject mapObject = Instantiate(UnityEngine.Resources.Load(mapButtonPath), openedBook.transform.Find("Map_Buttons")) as GameObject;
      mapObject.transform.position = mapObject.transform.position - new Vector3(0, mapY, 0);
      mapObject.transform.Find("Map_Name").GetComponent<Text>().text = map.name;
      mapY += 0.5f;
    }
  }

  private void destroyLeftSideBook()
  {
    foreach (Transform child in transform.Find("Background").Find("Opened_Book").Find("Map_Buttons"))
    {
      Destroy(child.gameObject);
    }
  }

  private void rigthSideBook(DataMap map)
  {
    GameObject openedBook = transform.Find("Background").Find("Opened_Book").gameObject as GameObject;

    openedBook.transform.Find("Story_Title").GetComponent<Text>().text = "Story: " + map.storyTilte;
    openedBook.transform.Find("Story").GetComponent<Text>().text = map.story;
    openedBook.transform.Find("Goals_Title").GetComponent<Text>().text = "Goal: " + map.goalsTitle;
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
    openedBook.transform.Find("Goals").GetComponent<Text>().text = goal;
  }


  void Start()
  {
    ChapterManagement.chaptersConstruction();

    int chapterNumber = 1;
    float bookX = 0f;
    foreach(DataChapter chapter in ChapterManagement.chapters){
      Debug.Log("In foreach");
      GameObject book = Instantiate(UnityEngine.Resources.Load(bookPath), transform.Find("Background").Find("Library")) as GameObject;
      book.transform.position = book.transform.position + new Vector3(bookX,0,0);
      book.transform.Find("Book_Button").Find("Book_Number").GetComponent<Text>().text = "" + chapterNumber;
      bookX += 1f;
      chapterNumber++;
    }

    currentChapter = ChapterManagement.currentChapter;

    leftSideBook(ChapterManagement.chapters[currentChapter]);
    //rigthSideBook(ChapterManagement.chapters[0].maps[0]);
  }

  void Update()
  {
    if(!StatData.getCurrent().Equals(currentMap))
    {
      currentMap = StatData.getCurrent();
      int i = 0;
      bool found = false;
      while(i<ChapterManagement.chapters[currentChapter].maps.Count && !found)
      {
        if(ChapterManagement.chapters[currentChapter].maps[i].name.Equals(currentMap))
        {
          rigthSideBook(ChapterManagement.chapters[currentChapter].maps[i]);
          found = true;
        }
        i++;
      }
      if(!found)
      {
        Debug.Log("Map could not be load");
      }

    }
    if(ChapterManagement.currentChapter != currentChapter){
      leftSideBook(ChapterManagement.chapters[currentChapter]);
    }
  }
}
