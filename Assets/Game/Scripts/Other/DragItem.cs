using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform parentAfterDrag;
    public Image image;
    public bool isDropped;
    public ButtonSetup buttonSetup;
    public ItemType itemType;

    private void Update()
    {
        this.itemType = buttonSetup.itemType;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isDropped == false)
        {
            Debug.Log("Begin drag");
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            image.raycastTarget = false;
        }


    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDropped == false)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDropped == false)
        {
            Debug.Log("End drag");
            transform.SetParent(parentAfterDrag);
            image.raycastTarget = true;
        }

    }

}
