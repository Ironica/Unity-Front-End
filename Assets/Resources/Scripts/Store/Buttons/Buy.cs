using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Buy : MonoBehaviour
{
    public void buyTheMusic()
    {
      ItemsInStore storage = JsonObjConverter.toObj();

      int price = int.Parse(this.GetComponent<Text>().text);

      if(price <= storage.gems)
      {
        storage.gems -= price;
        string itemName = transform.parent.parent.Find("Music_Name").GetComponent<Text>().text;

        storage.sounds = storage.sounds.Select(e => (e.itemName == itemName) switch {
          true => new ItemForSale(e.itemName, e.priceInGems, true),
          _ => e
        }).ToList();



        transform.parent.parent.parent.parent
        .Find("Top")
        .Find("GemTotal")
        .Find("Total")
        .GetComponent<Text>()
        .text = "" + storage.gems;
        transform.parent.parent.Find("Use_Button").gameObject.SetActive(true);
        transform.parent.gameObject.SetActive(false);
      }
      JsonObjConverter.toJson(storage);
    }
}
