using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Movement
{
    #region VARIABLES

    [Header("Movement Settings")]
    public float acceleration;
    public float deceleration;
    public float maxVelocity;
    public float turnSpeed;

    [Header("Gravity Settings")]
    private float gravityConst = -9.81f;
    public float gravityMultiplier;
    public bool applyGravity;
    [Space]
    public float fallSpeed;

    private float _maxVel;

    #endregion

    #region METHODS

    public void Move(Rigidbody rb, Vector3 direction)
    {
        if (rb.isKinematic)
        {
            rb.MovePosition(rb.position + direction * (acceleration));
        }
        else
        {
            rb.AddForce(direction * acceleration, ForceMode.Force);
        }
    }

    public void Launch(Rigidbody rb, Vector3 direction, float force, bool normalise)
    {
        if (normalise)
        {
            direction = direction.normalized;
        }

        rb.AddForce(direction * force, ForceMode.Impulse);
    }

    public void Rotate(Rigidbody rb, Vector3 direction, float turnSpeed = 45)
    {
        Quaternion lookAtRotation = Quaternion.LookRotation(direction);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, lookAtRotation, turnSpeed));
    }

    public void Move(Rigidbody rb, Vector2 direction)
    {
        Vector3 d = new Vector3(direction.x, 0, direction.y);

        if (rb.isKinematic)
        {
            rb.MovePosition(rb.position + d * acceleration);
        }
        else
        {
            rb.AddForce(d * acceleration, ForceMode.Force);
        }
    }
    public void HandleGravity(Rigidbody rb)
    {
        rb.AddForce(Vector3.down * gravityConst * gravityMultiplier, ForceMode.Force);
    }

    public void HandleBoosts(float speedBoost)
    {
        _maxVel = maxVelocity + speedBoost; 
    }

    public void Halt(Rigidbody rb)
    {
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
    }

    public void Break(Rigidbody rb)
    {
        Vector3 brakeVelocity = new Vector3(deceleration * rb.velocity.x * -1, rb.velocity.y, deceleration * rb.velocity.z * -1);
        rb.AddForce(brakeVelocity, ForceMode.Force);
    }

    public void ClampVelocity(Rigidbody rb)
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        Vector3 tallVel = new Vector3(0f, rb.velocity.y, 0);

        if (flatVel.magnitude > _maxVel)
        {
            Vector3 clampedVel = flatVel.normalized * _maxVel;
            rb.velocity = new Vector3(clampedVel.x, rb.velocity.y, clampedVel.z);
        }

        if (rb.velocity.y < 0 && tallVel.magnitude > fallSpeed)
        {
            Vector3 clampedVel = tallVel.normalized * fallSpeed;
            rb.velocity = new Vector3(rb.velocity.x, clampedVel.y, rb.velocity.z);
        }
    }

    #endregion
}
