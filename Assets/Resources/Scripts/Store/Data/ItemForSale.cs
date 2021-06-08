using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemForSale
{

  //The name of the item
  public string itemName;

  //The cost of the item
  public int priceInGems;

  //True if the item has been sold
  public bool sold;

  public ItemForSale(string itemName, int priceInGems, bool sold){
    this.itemName = itemName;
    this.priceInGems = priceInGems;
    this.sold = sold;
  }
}
