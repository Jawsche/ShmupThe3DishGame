using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ItemUse : MonoBehaviour {
    public bool isPlayer = false;
    public int inventorySize = 2;
    public GameObject itemHand;
    public GameObject equippedItem;
    public int currentEquipIndex = 0;
    public GameObject[] items;

   

	// Use this for initialization
	void Awake () {
        itemHand = gameObject.transform.GetChild(0).gameObject;

        items = new GameObject[inventorySize];
        for (int i = 0; i < items.Length; i++)
        {
            if (itemHand.transform.childCount > i)
            {
                items[i] = itemHand.transform.GetChild(i).gameObject;
                if (i != 0)
                    items[i].SetActive(false);
            }
        }
        equippedItem = items[currentEquipIndex];

        if (gameObject.tag == "Player")
            isPlayer = true;

    }

    // Update is called once per frame
    void Update()
    {
        
        if(equippedItem != items[currentEquipIndex])
        {
            equippedItem.SetActive(false);
            items[currentEquipIndex].SetActive(true);
            equippedItem = items[currentEquipIndex];
        }
        equippedItem = items[currentEquipIndex];

        if (isPlayer)
        {
            if (CrossPlatformInputManager.GetButton("Fire1") && equippedItem.activeInHierarchy == true)
            {
                UseItem();
            }

            if (CrossPlatformInputManager.GetButtonDown("Jump") && equippedItem.activeInHierarchy == true)
            {
                SwapItem();
            }
        }
    }

    public void UseItem()
    {
        Component weaponScript = equippedItem.GetComponent(equippedItem.name);
        weaponScript.SendMessage("Fire");
    }

    public void SwapItem()
    {
        if (currentEquipIndex >= items.Length - 1)
            currentEquipIndex = 0;
        else
            currentEquipIndex++;
    }
}
