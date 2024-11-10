using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatTrap : Entity, IPickup
{
    #region VARIABLES

    [Header("References")]
    public Image timer;
    public Transform holdPosition;
    public Animator animator;
    public Collider triggerCollider;

    private ItemSpawner spawner;

    [Header("Trap Settings")]
    public float deployLaunchForce;
    public float deployTime;

    private bool isPrimed = false;

    private bool isDeployed;

    #endregion

    #region UNITY METHODS

    private void Start()
    {
        isDeployed = false;
        isPrimed = false;
        this.entityName = "Rat Trap";

        Deploy();
    }

    private void OnTriggerStay(Collider other)
    {
        if (isPrimed && isDeployed && (other.CompareTag("Player") || other.CompareTag("Rat")))
        {
            if (other.TryGetComponent<Entity>(out Entity victim))
            {
                //Use TryGetComponent to get the Monobehaviour that has the Entity class on the Rat, then damage it
                Trigger(this, victim);
            }
            else
            {
                Trigger(this, null);
            }
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

    #endregion

    #region METHODS

    private void Trigger(Entity eventEntity, Entity victim)
    {
        Destroy(this.gameObject, 0.51f);

        triggerCollider.enabled = false;

        animator.SetTrigger("Snap");

        if (victim != null)
        {
            if (victim.CompareTag("Rat"))
            {
                victim.Damage(eventEntity, 1);
            }
            else if (victim.CompareTag("Player"))
            {
                victim.Damage(eventEntity, 0);
            }
        }

        if (spawner != null)
        {
            spawner.onItemPickup?.Invoke();
        }
    }

    public void Deploy()
    {
        isDeployed = true;
        triggerCollider.enabled = false;

        rb.AddForce(Vector3.up * deployLaunchForce, ForceMode.Impulse);
        StartCoroutine(StartDeploy());
    }

    private IEnumerator StartDeploy()
    {
        float elapsedTime = 0;

        animator.speed = 1 / deployTime;
        animator.SetTrigger("Arm");

        while (timer.fillAmount != 1)
        {
            elapsedTime += Time.deltaTime;
            float percentComplete = elapsedTime / deployTime;

            timer.fillAmount = Mathf.Lerp(0, 1, percentComplete);

            yield return null;
        }

        animator.speed = 1;
        triggerCollider.enabled = true;

        timer.fillAmount = 0;
        isPrimed = true;

        yield return null;
    }

    public void SetSpawner(ItemSpawner spawner)
    {
        if (spawner != null)
        {
            this.spawner = spawner;
        }
    }

    #endregion
}
