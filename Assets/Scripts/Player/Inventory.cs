using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, IInventory
{
    [Header("Deployable Prefab")]
    public GameObject item;

    [Header("Deploy Settings")]
    public Transform holdPos;

    private RatTrap ratTrap;

    public void Pickup(GameObject item)
    {
        if (this.item == null)
        {
            this.item = item;
            GameObject obj = Instantiate(item, holdPos.position, Quaternion.identity, null);
            
            if (obj.TryGetComponent(out RatTrap trap))
            {
                ratTrap = trap;
                trap.holdPosition = holdPos;
            }
        }
    }

    public void UseItem()
    {
        if (item != null)
        {
            ratTrap.Deploy();
            item = null;
        }
    }
}

public interface IInventory
{
    public abstract void Pickup(GameObject item);

    public abstract void UseItem();
}
