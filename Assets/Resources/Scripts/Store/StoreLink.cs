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

  // Start is called before the first frame update
  void Start()
  {
    storage = JsonObjConverter.toObj();

    //Affichage des personnages
    GameObject skins = GameObject.Find("Skin") as GameObject;



    JsonObjConverter.toJson(storage);
  }

  // Update is called once per frame
  void Update()
  {

  }
}
