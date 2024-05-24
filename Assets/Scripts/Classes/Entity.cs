using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Entity : IEntity
{
    #region VARIABLES

    [Header("Entity References")]
    public Rigidbody rb;

    [Header("Entity Settings")]
    public string entityName;
    public float health;
    public float healthBoost;
    public float damageBoost;
    public float speedBoost;

    protected float currentHealth;
    public bool isDead = false;

    #endregion

    #region EVENTS

    public delegate void OnHurt(Entity eventEntity, float amount);
    public OnHurt onHurt;

    public delegate void OnHeal(Entity eventEntity, float amount);
    public OnHeal onHeal;

    public delegate void OnDamageDealt(Entity eventEntity, float amount);
    public OnDamageDealt onDamageDealt;

    public delegate void OnDeath(Entity eventEntity);
    public OnDeath onDeath;

    #endregion

    #region INTERFACE METHODS

    public override void Damage(Entity eventEntity, float amount)
    {
        currentHealth -= amount;

        onHurt?.Invoke(eventEntity, amount);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Death(eventEntity);
        }
    }


    public override void Heal(Entity eventEntity, float amount)
    {
        if (amount + currentHealth > health)
        {
            amount = ((amount + currentHealth) - health);
            onHeal?.Invoke(eventEntity, amount);
            return;
        }
        else
        {
            currentHealth += amount;
            onHeal?.Invoke(eventEntity, amount);
            return;
        }
    }

    public override void Death(Entity eventEntity)
    {
        onDeath?.Invoke(eventEntity);
        isDead = true;
        Destroy(eventEntity);
    }


    public override void Destroy(Entity eventEntity)
    {
        Destroy(this.gameObject);
    }

    #endregion
}

public abstract class IEntity: MonoBehaviour
{
    public abstract void Damage(Entity eventEntity, float amount);

    public abstract void Heal(Entity eventEntity, float amount);

    public abstract void Destroy(Entity eventEntity);

    public abstract void Death(Entity eventEntity);
}
