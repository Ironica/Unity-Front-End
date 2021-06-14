using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MusicScript : MonoBehaviour
{
  private int musicIndex;
  public void getMusicIndex()
  {
     musicIndex = this.transform.GetSiblingIndex();
     /* TODO: Dylan,
     ** add a function that start the music selected with this musicIndex here
     */
     
     if (StatData.indexStoreMusic == musicIndex)
     {
       StatData.isPlayable = !StatData.isPlayable;
     }
     else
     {
       StatData.indexStoreMusic = musicIndex;
       StatData.isPlayable = true;
     }
  }
  
  public void buyTheMusic()
  {
    ItemsInStore storage = JsonObjConverter.toObj();

    int price = int.Parse(this.transform.Find("Buy_Button").Find("Price").GetComponent<Text>().text);

    if(price <= storage.gems)
    {
      storage.gems -= price;
      var itemName = this.transform.Find("Music_Name").GetComponent<Text>().text;

      storage.sounds = storage.sounds.Select(e => (e.itemName == itemName) switch {
        true => new ItemForSale(e.itemName, e.priceInGems, true),
        _ => e
      }).ToList();

      transform.parent.parent
      .Find("Top")
      .Find("GemTotal")
      .Find("Total")
      .GetComponent<Text>()
      .text = "" + storage.gems;
      transform.Find("Use_Button").gameObject.SetActive(true);
      transform.Find("Buy_Button").gameObject.SetActive(false);
    }
    JsonObjConverter.toJson(storage);
  }

}
