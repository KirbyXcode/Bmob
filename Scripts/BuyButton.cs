using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class BuyButton : MonoBehaviour,IPointerClickHandler 
{

    public float price;
    public string foodName;
    public void OnPointerClick(PointerEventData eventData) 
    {
        BmobPayManager.Instance.OnBuyButtonClick(price,foodName);
    }
}
