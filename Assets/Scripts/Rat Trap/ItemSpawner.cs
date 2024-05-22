using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Spawner References")]
    public GameObject[] itemList;
    [Space]
    public Transform[] spawnPositions;

    [Header("Spawner Settings")]
    public Vector2 spawnTimer;
    [Range(1, 5)]
    public int maxItems;

    private int _spawnedItems;

    #region EVENTS

    public delegate void OnItemPickup();
    public static OnItemPickup onItemPickup;

    #endregion

    #region UNITY METHODS

    private void OnEnable()
    {
        onItemPickup += ItemPickedUp;
    }

    private void OnDisable()
    {
        onItemPickup -= ItemPickedUp;
    }

    private void Start()
    {
        StartCoroutine(SpawnSequence());
    }

    #endregion

    #region METHODS

    public void SpawnItemRandom()
    {
        Instantiate(itemList[Random.Range(0, itemList.Length)], spawnPositions[Random.Range(0, spawnPositions.Length)].position, Quaternion.identity, null);
        ItemSpawned();
    }

    public void ItemSpawned()
    {
        _spawnedItems++;
    }

    public void ItemPickedUp()
    {
        _spawnedItems--;
    }

    #endregion

    #region COROUTINES

    private IEnumerator SpawnSequence()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawnTimer.x, spawnTimer.y));

            if (_spawnedItems >= maxItems)
            {
                yield return new WaitUntil(() => _spawnedItems < maxItems);
            }

            SpawnItemRandom();
        }
    }

    #endregion
}
