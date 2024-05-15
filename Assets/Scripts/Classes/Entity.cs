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
    public float health;
    public float healthBoost;
    public float damageBoost;
    public float speedBoost;

    private float currentHealth;

    #endregion

    #region EVENTS

    public delegate void OnHurt(Entity eventEntity, float amount);
    public event OnHurt onHurt;

    public delegate void OnHeal(Entity eventEntity, float amount);
    public event OnHeal onHeal;

    public delegate void OnDamageDealt(Entity eventEntity, float amount);
    public event OnDamageDealt onDamageDealt;

    public delegate void OnDeath(Entity eventEntity);
    public event OnDeath onDeath;

    #endregion

    #region INTERFACE METHODS

    public void Damage(Entity eventEntity, float amount)
    {
        currentHealth -= amount;

        onHurt?.Invoke(eventEntity, amount);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            onDeath?.Invoke(eventEntity);
        }
    }

    public void Heal(Entity eventEntity, float amount)
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

    public void Destroy(Entity eventEntity)
    {
        onDeath?.Invoke(eventEntity);
    }

    #endregion
}

public interface IEntity
{
    public abstract void Damage(Entity eventEntity, float amount);

    public abstract void Heal(Entity eventEntity, float amount);

    public abstract void Destroy(Entity eventEntity);
}
