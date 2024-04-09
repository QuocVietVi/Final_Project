using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public GameObject icon;
    public ItemType itemType;
    private DragItem dragItem;

    public void OnDrop(PointerEventData eventData)
    {
       
        GameObject dropped = eventData.pointerDrag;
        DragItem item = dropped.GetComponent<DragItem>();
        if (item.itemType == this.itemType)
        {
            if (transform.childCount >= 1)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
            dragItem = Instantiate(item, transform);
            dragItem.image.raycastTarget = true;
            dragItem.isDropped = true;
            if (dragItem.image.GetComponent<Image>().enabled == false)
            {
                dragItem.image.GetComponent<Image>().enabled = true;
            }
        }
       
    }
}
