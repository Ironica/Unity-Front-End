using System.Collections;
using System.Collections.Generic;
using Resources.Scripts;
using Resources.Scripts.DataScript;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreLink : MonoBehaviour
{
  private ItemsInStore storage;
  private GameObject skins;
  private GameObject sounds;

  private GameObject top;
  private GameObject pagePanel;

  private string buttonSkin = "Prefabs/STORE/Character_Item1";
  private string buttonSound = "Prefabs/STORE/Music_Item1";


  // Start is called before the first frame update
  void Start()
  {
    storage = JsonObjConverter.toObj();

    top = GameObject.Find("Main_Panel").gameObject as GameObject;
    top.transform.Find("Top_Panel")
      .Find("Gem_Score")
    .Find("Gem_Number")
    .GetComponent<TMP_Text>()
    .text = "" + storage.gems;

    //Affichage des personnages
    pagePanel = GameObject.Find("Page_Panel");
    Transform skins = pagePanel.transform.Find("Characters_Panel");
    Transform sounds = pagePanel.transform.Find("Music_Panel");



    float objX = 0f;
    for(int i = 0; i < storage.skins.Count; i++)
    {
      GameObject skinObject = Instantiate(UnityEngine.Resources.Load(buttonSkin), skins.transform) as GameObject;
      skinObject.transform.transform.position = skinObject.transform.position + new Vector3(objX,0,0);

      skinObject.transform.Find("Name").GetComponent<TMP_Text>().text = "" + storage.skins[i].itemName;

      GameObject price = skinObject.transform.Find("Price_Button").gameObject as GameObject;

      if(!storage.skins[i].sold){
        price.transform.Find("Price").GetComponent<TMP_Text>().text = "" + storage.skins[i].priceInGems;
      }
      objX += 0.4f * Screen.height;

    }

    objX= 0f;
    for(int i = 0; i < storage.sounds.Count; i++)
    {
      GameObject soundObject = Instantiate(UnityEngine.Resources.Load(buttonSound), sounds.transform) as GameObject;
      soundObject.transform.position = soundObject.transform.position + new Vector3(objX,0,0);

      soundObject.transform.Find("Name").GetComponent<TMP_Text>().text = "" + storage.sounds[i].itemName;

      GameObject price = soundObject.transform.Find("Price_Button").gameObject as GameObject;
      GameObject use = soundObject.transform.Find("Use_Button").gameObject as GameObject;

      if(!storage.sounds[i].sold)
      {
        price.transform.Find("Price").GetComponent<TMP_Text>().text = "" + storage.sounds[i].priceInGems;
      }
      else
      {
        price.SetActive(false);
        use.SetActive(true);
      }
      objX += 0.3f * Screen.height;
    }
    JsonObjConverter.toJson(storage);
  }

  public void buy()
  {

  }

  private void OnApplicationQuit()
  {
    var shutdownApi = "simulatte/shutdown";
    new ShutDown(shutdownApi, Global.port).ShutDownOldServer();
  }
}
