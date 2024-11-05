using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Entity
{
    #region VARIABLES

    [Header("Animation References")]
    public Animator animator;

    [Header("Movement Settings")]
    public Movement movement;
    [Space]
    [Range(0.1f, 5)]
    public float slowStrength;
    public float slowDuration;

    [Header("Dash Stuff")]
    public Collider hitBox;
    public ParticleSystem dashParticles;
    public LayerMask excludeLayer;
    public LayerMask includeLayer;
    [Space]
    public float forwardDashForce;
    public float upDashForce;
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

    private void Update()
    {
        if (!isDead)
        {
            HandleAnimation();
        }
    }

    private void Start()
    {
        inventory.Startup();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Rat"))
        {
            GameManager.Instance.GameOver(GameFinished.Failure);
        }
    }

    #endregion

    #region METHODS

    #region ANIMATION

    public void HandleAnimation()
    {
        animator.SetBool("isMoving", inputDirection.magnitude > 0);
        animator.SetBool("isHoldingTrap", inventory.item == null);
    }

    #endregion

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
        animator.SetTrigger("dash");
        gameObject.GetComponent<AudioSource>().Play();
        dashTimer = Time.time + dashCooldown;

        Vector3 dashDirection = transform.forward;

        dashParticles.Play();

        rb.velocity = Vector3.zero;
        movement.Launch(rb, dashDirection, forwardDashForce, true);
        movement.Launch(rb, Vector3.up, upDashForce, true);

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
        rb.drag = slowStrength;

        yield return new WaitForSeconds(slowDuration);

        isStunned = false;
        rb.drag = 1;
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
