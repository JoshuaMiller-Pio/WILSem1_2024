using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class DecoyRat : MonoBehaviour
{
    #region VARIABLES

    [Header("Decoy Rat Settings")]
    public Transform[] patrolNodes;
    public float duration;
    public float turnSpeed;

    private int targetIndex;

    #endregion

    #region UNITY METHODS

    private void Start()
    {
        targetIndex = 0;

        StartCoroutine(Move());
    }

    #endregion

    #region COROUTINES

    private IEnumerator Move()
    {

        while (true)
        {
            float startTime = Time.time;
            float eTime = 0;
            float percentComplete = 0;

            Vector3 startPos = transform.position;
            Vector3 endPos = patrolNodes[targetIndex].position;

            Quaternion lookAtRotation = Quaternion.LookRotation(endPos - transform.position);

            while (percentComplete < 1)
            {
                eTime = Time.time - startTime;
                percentComplete = eTime / duration;

                transform.position = Vector3.Lerp(startPos, endPos, percentComplete);

                transform.rotation = Quaternion.Slerp(transform.rotation, lookAtRotation, turnSpeed);

                yield return null;
            }

            yield return new WaitForSeconds(1f);

            targetIndex++;

            if (targetIndex > patrolNodes.Length - 1)
            {
                targetIndex = 0;
            }
        }
    }

    #endregion
}
