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
    public Transform holdPosition;

    [Header("Trap Settings")]
    public float deployLaunchForce;
    public float deployTime;

    private bool isPrimed = false;

    private Entity entity;

    private bool isDeployed;

    #endregion

    #region UNITY METHODS

    private void Start()
    {
        isDeployed = false;
        isPrimed = false;
        entity = new Entity();
        entity.entityName = "Rat Trap";

        if (holdPosition == null)
        {
            Deploy();
        }
    }

    private void Update()
    {
        if (!isDeployed && holdPosition != null)
        {
            rb.position = holdPosition.position;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isPrimed && isDeployed && (other.CompareTag("Player") || other.CompareTag("Rat")))
        {
            //Use TryGetComponent to get the Monobehaviour that has the Entity class on the Rat, then damage it
            Trigger(entity, other.GetComponent<Entity>());
        }
    }

    #endregion

    #region METHODS

    private void Trigger(Entity eventEntity, Entity victim)
    {
        if (victim.CompareTag("Rat"))
        {
            victim.Damage(eventEntity, 1);
        }
        else if (victim.CompareTag("Player"))
        {
            victim.Damage(eventEntity, 0);
        }

        Destroy(this.gameObject);
    }

    public void Deploy()
    {
        isDeployed = true;

        rb.AddForce(Vector3.up * deployLaunchForce, ForceMode.Impulse);
        StartCoroutine(StartDeploy());
    }

    private IEnumerator StartDeploy()
    {
        float elapsedTime = 0;

        while (timer.fillAmount != 1)
        {
            elapsedTime += Time.deltaTime;
            float percentComplete = elapsedTime / deployTime;

            timer.fillAmount = Mathf.Lerp(0, 1, percentComplete);

            yield return null;
        }

        timer.fillAmount = 0;
        isPrimed = true;

        yield return null;
    }

    #endregion
}
