using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemsInStore
{
  public List<ItemForSale> skins {get; set;}
  public List<ItemForSale> sounds {get; set;}
  public List<ItemForSale> themes {get; set;}

  public int gems{get; set;}

  public ItemsInStore()
  {
    skins = new List<ItemForSale>();
    sounds = new List<ItemForSale>();
    themes = new List<ItemForSale>();
    gems = 0;
  }
}
