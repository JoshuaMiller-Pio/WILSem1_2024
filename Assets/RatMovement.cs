using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RatMovement : MonoBehaviour
{
    private GameObject[] _nodes;
    private Transform _currentPos, _prePos;
    private NavMeshAgent _nvMa;
    private bool _isAttacking;
    private int _counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        _nodes = GameObject.FindGameObjectsWithTag("MoveNodes");
        _nvMa = GetComponent<NavMeshAgent>();
        _currentPos = _nodes[Random.Range(0, _nodes.Length - 1)].transform;
        _nvMa.SetDestination(_currentPos.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (_nvMa.remainingDistance <0.5 && !_isAttacking)
        {
            changePos();
        }

        if (_counter == 10 && !_isAttacking)
        {
            _isAttacking = true;
            
        }
    }

    private void attack()
    {
        
    }
    
    private void changePos()
    {
        _counter++;
        _prePos = _currentPos;
        _currentPos = _nodes[Random.Range(0, _nodes.Length - 1)].transform;
        if (_currentPos == _prePos)
        {
            changePos();
        }

        _nvMa.SetDestination(_currentPos.position);
    }
}
