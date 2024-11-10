using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnRat : MonoBehaviour
{
    public GameObject ratPrefab;
    private GameObject[] _nodes, _ratPool;
    [SerializeField]
    int _ratsAllowed = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnRat());
        _nodes = GameObject.FindGameObjectsWithTag("SpawnNodes");
        _ratPool = new GameObject[_ratsAllowed];
        for (int i = 0; i < _ratsAllowed; i++)
        {
            Transform newPos = _nodes[Random.Range(0, _nodes.Length - 1)].transform;
            GameObject rat = Instantiate(ratPrefab, newPos.position, Quaternion.identity);
            _ratPool[i] = rat;
            _ratPool[i].transform.parent = transform;
            _ratPool[i].SetActive(false);
        }

    }

    IEnumerator spawnRat()
    {
        while (_ratsAllowed > 0)
        {
            yield return new WaitForSecondsRealtime(5);
            _ratsAllowed--;
            _ratPool[_ratsAllowed].SetActive(true);
        }
        
        yield return null;
    }
}
