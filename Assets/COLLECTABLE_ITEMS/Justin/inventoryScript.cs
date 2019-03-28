using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class inventoryScript : MonoBehaviour
{
    private AudioSource inventoryClick;

    public GameObject inventory;
    private int allSlots;
    private int enabledSlots;
    private GameObject[] slot;
    private Transform child;
    private int currIndex = 0;
    private double startTime;
    private double curr;
    private GameObject subinventory;
    private GameObject[] sub;
    private Sprite[] swap;
    void Start()
    {
        inventoryClick = GetComponent<AudioSource>();

        subinventory = GameObject.FindGameObjectWithTag("Subinventory");
        inventory.SetActive(false);
        allSlots = 9;
        slot = new GameObject[allSlots];
        sub = new GameObject[2];
        swap = new Sprite[2];
        for (int i = 0; i < allSlots; i++)
        {
            slot[i] = inventory.transform.GetChild(i).gameObject;
            if(slot[i].GetComponent<Slot>().item == null)
            {
                slot[i].GetComponent<Slot>().empty = true;
            }
        }
        startTime = Time.time;
    }

    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            inventoryClick.Play();

            inventory.SetActive(!inventory.active);
            subinventory.SetActive(!subinventory.active);
        }
        if (inventory.active)
        {
            child = slot[currIndex].transform.GetChild(0).GetChild(0);
            child.gameObject.SetActive(true);
            for (int i = 0; i < allSlots; i++)
            {
                if(i != currIndex)
                {
                    slot[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                }
            }
            curr = Time.time - startTime;
            if (Input.GetKey("left") && curr > .2)
            {
                if (currIndex == 0)
                {
                    currIndex = allSlots - 1;
                }
                else
                {
                    currIndex--;
                }
                startTime = Time.time;
            }
            if (Input.GetKey("right") && curr > .2)
            {
                if (currIndex == allSlots - 1)
                {
                    currIndex = 0;
                }
                else
                {
                    currIndex++;
                }
                startTime = Time.time;
            }

            if (Input.GetKey("q") && slot[currIndex].transform.childCount > 1)
            {
                if (sub[1] == slot[currIndex].transform.GetChild(1).gameObject && sub[0] != null)
                {
                    swap[1] = swap[0];
                    swap[0] = slot[currIndex].transform.GetComponent<Slot>().icon;
                    GameObject temp = sub[0];
                    sub[0] = sub[1];
                    sub[1] = temp;
                    subinventory.transform.GetChild(0).GetComponent<Image>().sprite = swap[0];
                    subinventory.transform.GetChild(1).GetComponent<Image>().sprite = swap[1];
                }
                else
                {
                    sub[0] = slot[currIndex].transform.GetChild(1).gameObject;
                    swap[0] = slot[currIndex].transform.GetComponent<Slot>().icon;
                    subinventory.transform.GetChild(0).GetComponent<Image>().sprite = swap[0]; 
                }
            }
            if (Input.GetKey("e") && slot[currIndex].transform.childCount > 1)
            {
                if (sub[0] == slot[currIndex].transform.GetChild(1).gameObject && sub[1] != null)
                {
                    swap[0] = swap[1];
                    swap[1] = slot[currIndex].transform.GetComponent<Slot>().icon;
                    GameObject temp = sub[1];
                    sub[1] = sub[0];
                    sub[0] = temp;
                    subinventory.transform.GetChild(1).GetComponent<Image>().sprite = swap[1];
                    subinventory.transform.GetChild(0).GetComponent<Image>().sprite = swap[0];
                }
                else
                {
                    sub[1] = slot[currIndex].transform.GetChild(1).gameObject;
                    swap[1] = slot[currIndex].transform.GetComponent<Slot>().icon;
                    subinventory.transform.GetChild(1).GetComponent<Image>().sprite = swap[1];
                }
            }
        }
        else
        {
            if (Input.GetKeyDown("q") && sub[0] != null)
            {
                sub[0].GetComponent<Item>().itemUsage();
            }
            if (Input.GetKeyDown("e") && sub[1] != null)
            {
                sub[1].GetComponent<Item>().itemUsage();
            }
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
