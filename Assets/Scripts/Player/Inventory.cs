using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, IInventory
{
    [Header("Holding Visuals")]
    public GameObject holdingVis;

    [Header("Deployable Prefab")]
    public GameObject item;

    [Header("Deploy Settings")]
    public Transform holdPos;

    private RatTrap ratTrap;

    public void Startup()
    {
        this.item = null;

        if (this.item == null)
        {
            holdingVis.SetActive(false);
        }
        else
        {
            holdingVis.SetActive(true);
        }
    }

    public void Pickup(GameObject item)
    {
        if (this.item == null)
        {
            this.item = item;
            holdingVis.SetActive(true);
        }
    }

    public void UseItem()
    {
        if (item != null)
        {
            holdingVis.SetActive(false);
            Instantiate(item, holdPos.position, Quaternion.identity);
            item = null;
        }
    }
}

public interface IInventory
{
    public abstract void Pickup(GameObject item);

    public abstract void UseItem();
}
