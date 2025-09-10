using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    public Rigidbody2D RB;
    public BoxCollider2D Interact;
    public Vector2 MoveDirection;
    public float MoveSpeed;
    public float RunSpeed;
    public float SlowSpeed;
    public float CurrentSpeed;
    public float CurrentStamina;
    public float MaxStamina;
    public float StaminaLoss;
    public float StaminaReturn;
    public bool IsRunning;
    public bool IsTired;
    public float Tiredtimer;
    public float TiredtimerMax;
    public void Awake()
    {
        CurrentSpeed = MoveSpeed;
        CurrentStamina = MaxStamina;
        Tiredtimer = TiredtimerMax;
    }
    public void FixedUpdate()
    {
        RB.linearVelocity = MoveDirection * CurrentSpeed;
        if (IsTired)
        {
            Tiredtimer -= Time.fixedDeltaTime;
            CurrentSpeed = SlowSpeed;
            if (Tiredtimer < 0)
            {
                Tiredtimer = TiredtimerMax;
                IsTired = false;
                CurrentSpeed = MoveSpeed;
            }
        }
        else
        {
            if (IsRunning == false && CurrentStamina < MaxStamina && IsTired == false)
            {
                CurrentStamina = CurrentStamina + (StaminaReturn * Time.fixedDeltaTime);
                Debug.Log("Stamina going up");
            }
            else if (IsRunning)
            {
                CurrentStamina = CurrentStamina - (StaminaLoss * Time.fixedDeltaTime);
                Debug.Log("Stamina going down");
                if(CurrentStamina < 0)
                {
                    IsTired = true;
                }
            }
            else if (CurrentStamina > MaxStamina)
            {
                CurrentStamina = MaxStamina;
            }
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            MoveDirection = context.ReadValue<Vector2>();
        }
        if (context.canceled)
        {
            MoveDirection = new Vector2(0, 0);
        }
    }
    public void Running(InputAction.CallbackContext context)
    {
        if (context.performed && IsTired != true)
        {
            IsRunning = true;
            CurrentSpeed = RunSpeed;
        }
        if (context.canceled)
        {
            IsRunning = false;
            CurrentSpeed = MoveSpeed;
        }
    }
}
