using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public Image itemImage;
    public ItemType itemType;
    public bool canDrop;
    public List<DropSlot> dropSlots = new List<DropSlot>();
    public WeaponType weaponType;
    public ShieldType shieldType;
    public AllyType allyType;
    public SkillType skillType;
    public GameObject defaultSkill;
    public GameObject defaultAlly;
    private DragItem dragItem;

    public void OnDrop(PointerEventData eventData)
    {
       
        GameObject dropped = eventData.pointerDrag;
        DragItem item = dropped.GetComponent<DragItem>();
        if (item.itemType == this.itemType )
        {
            ItemExist(item);
            if (transform.childCount >= 1)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
            dragItem = Instantiate(item, transform);
            dragItem.image.raycastTarget = true;
            itemImage = dragItem.image; 
            dragItem.isDropped = true;
            this.weaponType = dragItem.weaponType;
            this.shieldType = dragItem.shieldType;
            this.allyType = dragItem.allyType;
            this.skillType = dragItem.skillType;
            if (dragItem.image.GetComponent<Image>().enabled == false)
            {
                dragItem.image.GetComponent<Image>().enabled = true;
            }
        }
       
    }


    public void ItemExist(DragItem item)
    {
        for (int i = 0; i < dropSlots.Count; i++)
        {

            if (dropSlots[i].allyType != AllyType.Default && dropSlots[i].itemType == ItemType.Ally)
            {
                if (dropSlots[i].allyType == item.allyType)
                {
                    Destroy(dropSlots[i].dragItem.gameObject);
                    Instantiate(defaultAlly, dropSlots[i].gameObject.transform);
                    dropSlots[i].allyType = AllyType.Default;
                }
            }
            

        }
        if (dropSlots[0].skillType != SkillType.Default && dropSlots[0].itemType == ItemType.Skill)
        {
            if (dropSlots[0].skillType == item.skillType)
            {
                Destroy(dropSlots[0].dragItem.gameObject);
                Instantiate(defaultSkill, dropSlots[0].gameObject.transform);
                dropSlots[0].skillType = SkillType.Default;
            }
        }
    }

}
