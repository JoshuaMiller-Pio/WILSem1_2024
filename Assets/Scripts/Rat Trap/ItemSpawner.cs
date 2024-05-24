using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Spawner References")]
    public GameObject[] itemList;
    [Space]
    public List<Transform> spawnPositions;
    private List<Transform> usedPositions;

    [Header("Spawner Settings")]
    public Vector2 spawnTimer;
    [Range(1, 5)]
    public int maxItems;

    private int _spawnedItems;

    #region EVENTS

    public delegate void OnItemPickup();
    public static OnItemPickup onItemPickup;

    public delegate void OnTrapDeployed();
    public static OnTrapDeployed onTrapDeployed;

    public delegate void OnTrapTriggered();
    public static OnTrapTriggered onTrapTriggered;

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
        int randPos = Random.Range(0, spawnPositions.Count);
        int randItem = Random.Range(0, itemList.Length);

        Instantiate(itemList[randItem], spawnPositions[randPos].position, Quaternion.identity, null);

        usedPositions.Add(spawnPositions[randPos]);
        spawnPositions.RemoveAt(randPos);

        ItemSpawned();
    }

    public void ItemSpawned()
    {
        _spawnedItems++;
    }

    public void ItemPickedUp()
    {
        _spawnedItems--;

        spawnPositions.Add(usedPositions[0]);
        usedPositions.RemoveAt(0);
    }

    #endregion

    #region COROUTINES

    private IEnumerator SpawnSequence()
    {
        usedPositions = new List<Transform>();

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
