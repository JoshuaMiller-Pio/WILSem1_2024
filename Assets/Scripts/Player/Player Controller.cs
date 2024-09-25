using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Entity
{
    #region VARIABLES

    [Header("Movement Settings")]
    public Movement movement;
    [Space]
    [Range(0, 1)]
    public float slowPercent;
    public float slowDuration;

    [Header("Dash Stuff")]
    public Collider hitBox;
    public ParticleSystem dashParticles;
    public LayerMask excludeLayer;
    public LayerMask includeLayer;
    [Space]
    public float dashForce;
    public float dashDuration;
    public float dashCooldown;
    [Space]
    public Vector3 dashInfluence;

    [Header("Inventory Settings")]
    public Inventory inventory;

    private Vector2 inputDirection;

    [Header(">>> TESTING ONLY <<<")]
    //State Booleans
    [SerializeField] private bool isDashing;
    [SerializeField] private bool isStunned;

    private float dashTimer;

    #endregion

    #region INPUT METHODS

    public void OnMove(InputAction.CallbackContext ctx)
    {
        inputDirection = ctx.ReadValue<Vector2>();
    }

    public void OnDrop(InputAction.CallbackContext ctx)
    {
        if (!isDead && ctx.started)
        {
            DeployTrap();
        }
    }

    public void OnDash(InputAction.CallbackContext ctx)
    {
        if (!isDead && ctx.started)
        {
            if (Time.time > dashTimer)
            {
                Dash();
            }
        }
    }

    #endregion

    #region UNITY METHODS

    #region ENABLE/DISABLE

    #endregion

    private void FixedUpdate()
    {
        if (!isDead)
        {
            HandleMovement();
        }
    }

    #endregion

    #region METHODS

    #region MOVEMENT

    private void HandleMovement()
    {
        if (!isDashing)
        {
            if (inputDirection != Vector2.zero)
            {
                movement.Move(rb, new Vector3(inputDirection.x, 0, inputDirection.y));
                movement.Rotate(rb, new Vector3(inputDirection.x, 0, inputDirection.y), movement.turnSpeed);
            }
            else
            {
                movement.Break(rb);
                movement.Rotate(rb, transform.forward, movement.turnSpeed);
            }

            movement.ClampVelocity(rb);
        }

        movement.HandleBoosts(speedBoost);
    }

    private void Dash()
    {
        Vector3 dashDirection = transform.forward;

        dashParticles.Play();

        movement.Launch(rb, dashDirection, dashForce, true);
        movement.Launch(rb, Vector3.up, dashForce / 2, true);

        dashTimer = Time.time + dashCooldown;

        StartCoroutine(DashCooldown());
    }

    private IEnumerator DashCooldown()
    {
        isDashing = true;
        hitBox.excludeLayers = excludeLayer;
        yield return new WaitForSeconds(dashDuration);
        hitBox.excludeLayers = includeLayer;
        isDashing = false;
    }

    private IEnumerator Slowed()
    {
        isStunned = true;
        float slowAmount = movement.acceleration * slowPercent;

        speedBoost += -slowAmount;

        yield return new WaitForSeconds(slowDuration);

        isStunned = false;

        speedBoost -= -slowAmount;
    }

    #endregion

    #region RAT TRAPS

    private void DeployTrap()
    {
        if (inventory.item != null)
        {
            inventory.UseItem();
        }
    }

    public override void Damage(Entity eventEntity, float amount)
    {
        currentHealth -= amount;

        onHurt?.Invoke(eventEntity, amount);

        if (!isStunned)
        {
            StopCoroutine(Slowed());
            StartCoroutine(Slowed());
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            onDeath?.Invoke(eventEntity);
        }
    }

    #endregion

    #region DEFEAT

    #endregion

    #endregion
}
