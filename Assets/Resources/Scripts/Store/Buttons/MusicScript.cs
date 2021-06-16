using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class MusicScript : MonoBehaviour
{
  private int musicIndex;
  public void getMusicIndex()
  {
     musicIndex = this.transform.GetSiblingIndex();

     if (StatData.musicTest== musicIndex)
     {
       StatData.isPlayable = !StatData.isPlayable;
       StatData.storeListening = !StatData.storeListening;
     }
     else
     {
       StatData.musicTest = musicIndex;
       StatData.isPlayable = true;
       StatData.storeListening = true;
     }
  }

  public void useMusic()
  {
    StatData.musicUsed = this.transform.GetSiblingIndex();
  }

  public void buyTheMusic()
  {
    ItemsInStore storage = JsonObjConverter.toObj();

    int price = int.Parse(this.transform.Find("Price_Button").Find("Price").GetComponent<TextMeshProUGUI>().text);

    if(price <= storage.gems)
    {
      storage.gems -= price;
      var itemName = this.transform.Find("Name").GetComponent<TextMeshProUGUI>().text;

      storage.sounds = storage.sounds.Select(e => (e.itemName == itemName) switch {
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
