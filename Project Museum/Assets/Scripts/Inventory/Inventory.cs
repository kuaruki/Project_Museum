using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // slots do inventário
    private const int SLOTS = 9;

    // lista com os itens do inventário
    public List<IInventoryItem> mItems = new List<IInventoryItem>();

    // eventos
    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> ItemRemoved;
    public event EventHandler<InventoryEventArgs> ItemUsed;

    // adicionar um item ao inventário
    public void AddItem(IInventoryItem item)
    {
        if(mItems.Count < SLOTS)
        {
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if(collider.enabled)
            {
                collider.enabled = false;

                mItems.Add(item);

                item.OnPickup();

                if(ItemAdded != null)
                {
                    ItemAdded(this, new InventoryEventArgs(item));
                }
            }
        }
    }

    // remover um item do inventário
    public void RemoveItem(IInventoryItem item)
    {
        Debug.Log("Itens" + mItems);
        if (mItems.Contains(item))
        {
            mItems.Remove(item);


            item.OnDrop();

            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();

            if (collider != null)
            {
                collider.enabled = true;
            }

            if (ItemRemoved != null)
            {
                ItemRemoved(this, new InventoryEventArgs(item));
            }
        }
    }

    // usar um item do inventário
    internal void UseItem(IInventoryItem item)
    {
        if (ItemUsed != null)
        {
            ItemUsed(this, new InventoryEventArgs(item));
        }

        item.OnUse();
    }
}
