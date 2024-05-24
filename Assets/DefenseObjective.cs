using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseObjective : MonoBehaviour
{
    private float Health = 100;
    

    public int force = 15;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0) 
        {
            death();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
         
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("collision");

        if (other.gameObject.tag == "rat")
        {

            Rigidbody enemyRig = other.rigidbody;
            enemyRig.isKinematic = false;
            enemyRig.AddForce(-transform.forward*force,ForceMode.Impulse);
            StartCoroutine(knockback(enemyRig));
            Health -= 10;
            Debug.Log("attacking");

        }      }

    IEnumerator knockback(Rigidbody enemyRig)
    {
        yield return new WaitForSecondsRealtime(0.25f);
        enemyRig.isKinematic = true;

        
        
        yield return null;
    }



    void death()
    {
        Debug.Log("gameOver");
    }
}
