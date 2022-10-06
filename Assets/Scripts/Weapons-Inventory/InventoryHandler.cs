using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    public List<GameObject> items;
    public int selectedItem = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float scrollDirection = Input.mouseScrollDelta.y;
        if (scrollDirection > 0)
        {
            selectedItem += 1;
        }
        if (scrollDirection < 0)
        {
            selectedItem -= 1;
        }
        if (selectedItem > items.Count-1)
        {
            selectedItem = 0;
        }
        if (selectedItem < 0)
        {
            selectedItem = items.Count-1;
        }
        foreach (GameObject item in items)
        {
            if (items.IndexOf(item) == selectedItem)
            {
                item.SetActive(true);
            }
            else
            {
                item.SetActive(false);
            }

        }
    }
}
