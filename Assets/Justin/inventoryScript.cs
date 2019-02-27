using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryScript : MonoBehaviour
{
    public GameObject inventory;
    private int allSlots;
    private int enabledSlots;
    private GameObject[] slot;
    void Start()
    {
        inventory.SetActive(false);
        allSlots = 9;
        slot = new GameObject[allSlots];

        for (int i = 0; i < allSlots; i++)
        {
            slot[i] = inventory.transform.GetChild(i).gameObject;
            if(slot[i].GetComponent<Slot>().item == null)
            {
                slot[i].GetComponent<Slot>().empty = true;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.SetActive(!inventory.active);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            GameObject itemPickedUp = other.gameObject;
            Item item = itemPickedUp.GetComponent<Item>();
            addItem(itemPickedUp, item.id, item.type, item.description, item.icon);
        }
    }

    private void addItem(GameObject itemPickedUp, int id, string type, string description, Sprite icon)
    {
        for (int i = 0; i < allSlots; i++)
        {
            if (slot[i].GetComponent<Slot>().empty)
            {
                itemPickedUp.GetComponent<Item>().pickedUp = true;
                slot[i].GetComponent<Slot>().icon = icon;
                slot[i].GetComponent<Slot>().id = id;
                slot[i].GetComponent<Slot>().type = type;
                slot[i].GetComponent<Slot>().description = description;
                slot[i].GetComponent<Slot>().item = itemPickedUp;
                itemPickedUp.transform.parent = slot[i].transform;
                itemPickedUp.SetActive(false);

                slot[i].GetComponent<Slot>().updateSlot();
                slot[i].GetComponent<Slot>().empty = false;
                return;
            }
            
        }
    }
}
