using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class PlayerScript : MonoBehaviour
{

  public void usePlayer()
  {
    StatData.playerUsed = this.transform.Find("Name").GetComponent<TextMeshProUGUI>().text;
    Debug.Log(StatData.playerUsed);
  }
  public void buyThePlayer()
  {
    ItemsInStore storage = JsonObjConverter.toObj();

    int price = int.Parse(this.transform.Find("Price_Button").Find("Price").GetComponent<TextMeshProUGUI>().text);

    if(price <= storage.gems)
    {
      storage.gems -= price;
      var itemName = this.transform.Find("Name").GetComponent<TextMeshProUGUI>().text;

      storage.skins = storage.skins.Select(e => (e.itemName == itemName) switch {
        true => new ItemForSale(e.itemName, e.priceInGems, true),
        _ => e
      }).ToList();

      transform.parent.parent.parent
      .Find("Top_Panel")
      .Find("Gem_Score")
      .Find("Gem_Number")
      .GetComponent<TextMeshProUGUI>()
      .text = "" + storage.gems;
      transform.Find("Use_Button").gameObject.SetActive(true);
      transform.Find("Price_Button").gameObject.SetActive(false);
    }
    JsonObjConverter.toJson(storage);
  }


}
