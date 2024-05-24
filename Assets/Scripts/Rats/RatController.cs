using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class RatController : Entity
{
    private GameObject[] _nodes;
    private Transform _currentPos, _prePos ,_LKP;
    private GameObject _objective,_player;
    private NavMeshAgent _nvMa;
    private bool _isAttacking = false, inSight = false;
    private int _counter = 0;
    private RatFsm _currentMode;
    private CapsuleCollider _capColl;
    private SphereCollider _sphColl;
    
    private enum RatFsm{
        Roam,
        AttackPlayer,
        AttackObjective,
        Search
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        _nodes = GameObject.FindGameObjectsWithTag("MoveNodes");
        _nvMa = GetComponent<NavMeshAgent>();
        _currentPos = _nodes[Random.Range(0, _nodes.Length - 1)].transform;
        _nvMa.SetDestination(_currentPos.position);
        _currentMode = RatFsm.Roam;
        _sphColl = GetComponent<SphereCollider>();
        _capColl = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {

        switch (_currentMode)
        {
            case RatFsm.Roam:
                _sphColl.enabled = false;
                _capColl.enabled = true;
                if (_nvMa.remainingDistance <0.5)
                {
                    ChangePos();
                }
                break;
            
            case RatFsm.AttackPlayer:
                _sphColl.enabled = false;
                _capColl.enabled = true;    
                AttackPlayer();
                    
                break;
            
            case RatFsm.AttackObjective:
                _sphColl.enabled = true;
                _capColl.enabled = false;
                AttackOBJ();
                break;
            case RatFsm.Search:
                _sphColl.enabled = false;
                _capColl.enabled = true;
                if (_nvMa.remainingDistance <0.5)
                {
                    SearchNearby();
                }
                break;
        }

    }

    private void AttackOBJ()
    {
        _objective = GameObject.FindGameObjectWithTag("Objective");
        _nvMa.SetDestination(_objective.transform.position);
       
    }
    private void AttackPlayer()
    {
        _nvMa.SetDestination(_player.transform.position);
    }

    private void SearchNearby()
    {
        Vector3 searchPos = new Vector3(_LKP.position.x + Random.Range(-5,5), _LKP.position.y, _LKP.position.z+ Random.Range(-5,5));
        _nvMa.SetDestination(searchPos);
    }

    private void ChangePos()
    {
        if (_counter == 5)
        {
            _currentMode = RatFsm.AttackObjective;
            return;
        }
        _counter++;
        _prePos = _currentPos;
        _currentPos = _nodes[Random.Range(0, _nodes.Length - 1)].transform;
        if (_currentPos == _prePos)
        {
            ChangePos();
        }

        _nvMa.SetDestination(_currentPos.position);
    }


    IEnumerator EnemySearch()
    {
        yield return new WaitForSecondsRealtime(2);
        if (inSight)
        {
            _currentMode = RatFsm.AttackPlayer;
            yield return null;

        }
        _currentMode = RatFsm.Roam;
        yield return null;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _player = other.gameObject;
            _currentMode = RatFsm.AttackPlayer;
            inSight = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _LKP = other.transform;
            _currentMode = RatFsm.Search;
            StartCoroutine(EnemySearch());
            inSight = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("touched");
        }
    }
}
