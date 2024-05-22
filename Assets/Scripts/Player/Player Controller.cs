using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region VARIABLES

    [Header("Entity Settings")]
    public Entity entity;

    [Header("Movement Settings")]
    public Movement movement;
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
        if (ctx.started)
        {
            DeployTrap();
        }
    }

    public void OnDash(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
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

    private void OnEnable()
    {
        entity.onDeath += Death;
    }

    private void OnDisable()
    {
        entity.onDeath -= Death;
    }

    #endregion

    private void FixedUpdate()
    {
        HandleMovement();
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
                movement.Move(entity.rb, new Vector3(inputDirection.x, 0, inputDirection.y));
                movement.Rotate(entity.rb, new Vector3(inputDirection.x, 0, inputDirection.y), movement.turnSpeed);
            }
            else
            {
                movement.Break(entity.rb);
                movement.Rotate(entity.rb, transform.forward, movement.turnSpeed);
            }

            movement.ClampVelocity(entity.rb, movement.maxVelocity);
        }
    }

    private void Dash()
    {
        Vector3 dashDirection = transform.forward;

        movement.Launch(entity.rb, dashDirection, dashForce, true);
        movement.Launch(entity.rb, Vector3.up, dashForce / 2, true);

        dashTimer = Time.time + dashCooldown;

        StartCoroutine(DashCooldown());
    }

    private IEnumerator DashCooldown()
    {
        isDashing = true;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
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

    #endregion

    #region DEFEAT

    private void Death(Entity eventEntity)
    {

    }

    #endregion

    #endregion
}
