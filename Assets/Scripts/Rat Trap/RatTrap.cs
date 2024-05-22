using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatTrap : MonoBehaviour
{
    #region VARIABLES

    [Header("References")]
    public Rigidbody rb;
    public Image timer;

    [Header("Trap Settings")]
    public float deployLaunchForce;
    public float deployTime;

    private float windupTime;
    private bool isPrimed;

    private Entity entity;

    private bool isDeployed;

    #endregion

    #region UNITY METHODS

    private void Start()
    {
        isDeployed = false;
    }

    private void Update()
    {
        if (isDeployed)
        {
            rb.MovePosition(transform.parent.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPrimed && other.CompareTag("Entity"))
        {
            //Use TryGetComponent to get the Monobehaviour that has the Entity class on the Rat, then damage it
            Trigger(entity, other.GetComponent<Entity>());
        }
    }

    #endregion

    #region METHODS

    private void Trigger(Entity eventEntity, Entity victim)
    {
        victim.Damage(eventEntity, 1);
    }

    public void Deploy()
    {
        isDeployed = true;

        this.transform.SetParent(null);//Remove itself from its parent
        rb.AddForce(Vector3.up * deployLaunchForce, ForceMode.Impulse);
        StartCoroutine(StartDeploy());
    }

    private IEnumerator StartDeploy()
    {
        while (windupTime < deployTime)
        {
            windupTime = Mathf.Lerp(0, deployTime, Time.deltaTime);
            timer.fillAmount = windupTime / deployTime;
            yield return null;
        }

        windupTime = deployTime;
        timer.fillAmount = 0;
    }

    #endregion
}
