using System.Collections;
using UnityEngine;

public interface ITradeItem
{
    void BuyItem(Item itemToBuy);
    void SellItem(Item itemToSell);
}
