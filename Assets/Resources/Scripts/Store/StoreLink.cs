using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreLink : MonoBehaviour
{
  private ItemsInStore storage;
  private GameObject skins;
  private GameObject sounds;
  private GameObject themes;

  private string buttonSkin = "Prefabs/STORE/BoutonPerso";
  private string buttonSound = "Prefabs/STORE/Music_Item";
  private string buttonTheme = "Prefabs/STORE/Theme_Item";

  // Start is called before the first frame update
  void Start()
  {
    storage = JsonObjConverter.toObj();

    //Affichage des personnages
    GameObject skins = GameObject.Find("Character") as GameObject;
    GameObject sounds = GameObject.Find("Music") as GameObject;
    GameObject themes = GameObject.Find("Theme") as GameObject;

    /*for(int i = 0; i < storage.skins.Count; i++)
    {
      GameObject skinsButton = Instantiate(UnityEngine.Resources.Load(buttonSkin)) as GameObject;
      skinsButton.transform.SetParent(skins.transform);

      if(!storage.skins[i].sold){
        skinsButton.transform.Find("PricePerso").GetComponent<Text>().text = "" + storage.skins[i].priceInGems;
      }
    }*/

    float objY = 0f;
    for(int i = 0; i < storage.sounds.Count; i++)
    {
      GameObject soundObject = Instantiate(UnityEngine.Resources.Load(buttonSound), sounds.transform) as GameObject;
      soundObject.transform.position = soundObject.transform.position - new Vector3(0,objY,0);

      soundObject.transform.Find("Music_Name").GetComponent<Text>().text = "" + storage.sounds[i].itemName;

      GameObject price = soundObject.transform.Find("Buy_Button").gameObject as GameObject;
      GameObject use = soundObject.transform.Find("Use_Button").gameObject as GameObject;

      if(!storage.sounds[i].sold)
      {
        price.transform.Find("Price").GetComponent<Text>().text = "" + storage.sounds[i].priceInGems;
        Destroy(use);
      }
      else
      {
        Destroy(price);
      }
      objY += 0.8f;
    }
    objY = 0f;
    for(int i = 0; i < storage.themes.Count; i++)
    {
      GameObject themeObject = Instantiate(UnityEngine.Resources.Load(buttonTheme), themes.transform) as GameObject;
      themeObject.transform.position = themeObject.transform.position - new Vector3(0,objY,0);

      themeObject.transform.Find("Body").Find("Theme_Name").GetComponent<Text>().text = "" + storage.themes[i].itemName;

      GameObject price = themeObject.transform.Find("Body").Find("Buy_Button").gameObject as GameObject;

      if(!storage.themes[i].sold)
      {
        price.transform.Find("Price").GetComponent<Text>().text = "" + storage.themes[i].priceInGems;
      }
      objY += 1.5f;
    }

    JsonObjConverter.toJson(storage);
  }

  // Update is called once per frame
  void Update()
  {

  }
}
