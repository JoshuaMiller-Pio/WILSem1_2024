using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatTrapItem : MonoBehaviour, IPickup
{
    [Header("Rat Trap Settings")]
    public EntityTags pickupTargets;
    public GameObject deployable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(pickupTargets.ToString()))
        {
            if (other.TryGetComponent<PlayerController>(out PlayerController player))
            {
                if (player.inventory.item == null)
                {
                    OnPickup(player.inventory);
                }
            }
        }
    }

    public void OnPickup(Inventory inv)
    {
        inv?.Pickup(deployable);//Put this item in the player's inventory

        ItemSpawner.onItemPickup?.Invoke();//Tell the Item Spawner that I have been picked up!

        Destroy(this.gameObject);
    }
}

public enum EntityTags
{
    Player,
    Rat
}

public interface IPickup
{
    public void OnPickup()
    {
        //Also do stuff by default
    }
}
