using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIBag : MonoBehaviour, IDropHandler
{
    
    public event Action OnGoldAddedToBag; //to countHandler

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Bag OnDrop activate");
        var droppedItem = eventData.pointerDrag.GetComponent<RewardItem>();
        if(droppedItem != null)
        {
            droppedItem.transform.SetParent(this.transform, true);
            OnGoldAddedToBag?.Invoke();
            Debug.Log("Gold added to bag");
        }
        
    }
}
