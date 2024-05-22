using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Entity
{
    #region VARIABLES

   
    [Header("Movement Settings")]
    public Movement movement;

    [Header("Inventory Settings")]
    public Inventory inventory;

    private Vector2 inputDirection;

    #endregion

    #region INPUT METHODS

    public void OnMove(InputAction.CallbackContext ctx)
    {
        inputDirection = ctx.ReadValue<Vector2>();
    }

    public void OnDrop(InputAction.CallbackContext ctx)
    {

    }

    #endregion

    #region UNITY METHODS

    #region ENABLE/DISABLE

    private void OnEnable()
    {
       onDeath += Death;
    }

    private void OnDisable()
    {
        onDeath -= Death;
    }

    #endregion

    private void FixedUpdate()
    {

    }

    #endregion

    #region METHODS

    #region MOVEMENT

    private void Move()
    {
        if (inputDirection != Vector2.zero)
        {
            movement.Move(rb, inputDirection);
        }
    }

    #endregion

    #region RAT TRAPS

    private void DeployTrap()
    {

    }

    #endregion

    #region DEFEAT

    private void Death(Entity eventEntity)
    {

    }

    #endregion

    #endregion
}
