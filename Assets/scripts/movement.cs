using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    public Image StaminaBar;
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
        StaminaBar.fillAmount = MaxStamina;
    }
    public void FixedUpdate()//this updates the staminabar and calcs the speed the player should be moving at bassed on the states of running or not
    {
        StaminaBar.fillAmount = CurrentStamina / MaxStamina;
        RB.linearVelocity = MoveDirection * CurrentSpeed;
        switch (MoveDirection.ToString())//changes the interact collider based on the current move direction
        {
            case "(1.00, 0.00)":
                Interact.offset = new Vector2(1, 0);
                break;
            case "(-1.00, 0.00)":
                Interact.offset = new Vector2(-1, 0);
                break;
            case "(0.00, 1.00)":
                Interact.offset = new Vector2(0, 1);
                break;
            case "(0.00, -1.00)":
                Interact.offset = new Vector2(0, -1);
                break;
            case "(0.71, 0.71)":
                Interact.offset = new Vector2(1, 1);
                break;
            case "(-0.71, 0.71)":
                Interact.offset = new Vector2(-1, 1);
                break;
            case "(0.71, -0.71)":
                Interact.offset = new Vector2(1, -1);
                break;
            case "(-0.71, -0.71)":
                Interact.offset = new Vector2(-1, -1);
                break;
        }
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
    public void Move(InputAction.CallbackContext context)//this gets the player arrow input direction
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
    public void Running(InputAction.CallbackContext context)//this gets the running input
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
