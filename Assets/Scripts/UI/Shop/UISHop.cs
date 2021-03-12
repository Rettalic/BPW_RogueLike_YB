using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : MonoBehaviour
{
    private Transform shop;
    private Transform shopItemTemplate;

    private void Awake()
    {
        shop = transform.Find("Shop");
        shopItemTemplate = shop.Find("AbilityImageBuy");
        shopItemTemplate.gameObject.SetActive(false);
    }

    private void CreateItemButton(Sprite itemSprite, string itemName, int itemCost)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, shop);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
        

    }
}
