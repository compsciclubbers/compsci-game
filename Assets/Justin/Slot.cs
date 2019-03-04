using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public bool empty;
    public Sprite icon;
    public int id;
    public string type;
    public string description;
    public GameObject item;
    private Transform slotIconGo;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        item.GetComponent<Item>().itemUsage();
    }
    private void Start()
    {
        slotIconGo = transform.GetChild(0);
    }
    public void updateSlot()
    {
        slotIconGo = transform.GetChild(0);
        print(icon);
        print(slotIconGo);
        slotIconGo.GetComponent<Image>().sprite = icon;
        
    }
}
