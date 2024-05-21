using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnRat : MonoBehaviour
{
    public GameObject ratPrefab;
    private GameObject[] _nodes;
    [SerializeField]
    int _ratsAllowed = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnRat());
        _nodes = GameObject.FindGameObjectsWithTag("SpawnNodes");

    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator spawnRat()
    {
        while (_ratsAllowed > 0)
        {
            yield return new WaitForSecondsRealtime(5);
            _ratsAllowed--;
            Transform newPos = _nodes[Random.Range(0, _nodes.Length - 1)].transform;
            GameObject rat = Instantiate(ratPrefab, newPos.position, Quaternion.identity);
             rat.transform.parent = transform;
        }
        
        yield return null;
    }
}
