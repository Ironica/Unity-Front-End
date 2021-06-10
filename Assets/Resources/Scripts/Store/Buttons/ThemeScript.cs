using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ThemeScript : MonoBehaviour
{
  public void buyTheTheme()
  {
    ItemsInStore storage = JsonObjConverter.toObj();

    int price = int.Parse(this.transform.Find("Body").Find("Buy_Button").Find("Price").GetComponent<Text>().text);

    if(price <= storage.gems)
    {
      storage.gems -= price;
      var itemName = this.transform.Find("Body").Find("Theme_Name").GetComponent<Text>().text;

      storage.themes = storage.themes.Select(e => (e.itemName == itemName) switch {
        true => new ItemForSale(e.itemName, e.priceInGems, true),
        _ => e
      }).ToList();

      transform.parent.parent
      .Find("Top")
      .Find("GemTotal")
      .Find("Total")
      .GetComponent<Text>()
      .text = "" + storage.gems;
      transform.Find("Body").Find("Buy_Button").gameObject.SetActive(false);
    }
    JsonObjConverter.toJson(storage);
  }
}
