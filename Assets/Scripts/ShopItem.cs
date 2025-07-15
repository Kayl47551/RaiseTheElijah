using NUnit.Framework.Constraints;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public GameObject Item;

    public void buy()
    {
        Instantiate(Item);
    }
}
