using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DefenseObjective : MonoBehaviour
{
    [Header("Objective Settings")]
    public float Health = 100;
    public int force = 15;

    [Header("Health Bar References")]
    public TMP_Text healthCounter;

    private float _health;

    private void Start()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Rat")
        {
            Rigidbody enemyRig = other.rigidbody;
            enemyRig.isKinematic = false;
            enemyRig.AddForce(-transform.forward * force, ForceMode.Impulse);
            StartCoroutine(knockback(enemyRig));
            _health -= 10;
            healthCounter.text = _health.ToString();

            if (_health <= 0)
            {
                Death();
            }
        }
    }

    IEnumerator knockback(Rigidbody enemyRig)
    {
        yield return new WaitForSecondsRealtime(0.25f);
        enemyRig.isKinematic = true;
        yield return null;
    }

    void Death()
    {
        GameManager.onGameEnd?.Invoke(GameFinished.Failure);
    }
}
