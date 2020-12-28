using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // singleton
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
            Debug.LogWarning("more than one instance of inventory");
        instance = this;
    }

    // pub sub
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    // public fields
    public List<Item> items = new List<Item>();
    public int space = 20;

    public bool Add(Item item)
    {
        if(!item.isDefaultItem)
        {
            if(items.Count >= space)
            {
                Debug.Log("Not Enough room.");
                return false;
            }
            items.Add(item);
            // trigger event
            if(onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
            
        }
        return true;

    }

    public void Remove(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
