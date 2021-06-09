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
  private string buttonTheme = "Prefabs/STORE/BoutonTheme";

  // Start is called before the first frame update
  void Start()
  {
    storage = JsonObjConverter.toObj();

    //Affichage des personnages
    GameObject skins = GameObject.Find("Character") as GameObject;
    GameObject sounds = GameObject.Find("Music") as GameObject;
    GameObject themes = GameObject.Find("Themes") as GameObject;

    /*for(int i = 0; i < storage.skins.Count; i++)
    {
    GameObject skinsButton = Instantiate(UnityEngine.Resources.Load(buttonSkin)) as GameObject;
    skinsButton.transform.SetParent(skins.transform);

    if(!storage.skins[i].sold){
    skinsButton.transform.Find("PricePerso").GetComponent<Text>().text = "" + storage.skins[i].priceInGems;
  }
}
for(int i = 0; i < storage.sounds.Count; i++)
{
GameObject soundObject = Instantiate(UnityEngine.Resources.Load(buttonSound)) as GameObject;
soundObject.transform.Find("").GetComponent<Text>().text = "" + storage.sounds[i].itemName;
GameObject price = soundObject.transform.Find("").gameObject as GameObject;
GameObject use = soundObject.transform.Find("").gameObject as GameObject;
if(!storage.sounds[i].sold)
{
price.GetComponent<Text>().text = "" + storage.sounds[i].priceInGems;
Destroy(use);
}
else
{
Destroy(price);
}
}
for(int i = 0; i < storage.themes.Count; i++)
{
GameObject themeObject = Instantiate(UnityEngine.Resources.Load(buttonTheme)) as GameObject;
themeObject.transform.Find("").GetComponent<Text>().text = "" + storage.themes[i].itemName;
GameObject price = themeObject.transform.Find("").gameObject as GameObject;
GameObject use = themeObject.transform.Find("").gameObject as GameObject;
if(!storage.themes[i].sold)
{
themeObject.transform.Find("").GetComponent<Text>().text = "" + storage.themes[i].priceInGems;
Destroy(use);
}
else
{
Destroy(price);
}
}
*/
JsonObjConverter.toJson(storage);
}

// Update is called once per frame
void Update()
{

}
}
