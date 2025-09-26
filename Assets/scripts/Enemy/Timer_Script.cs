using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Diagnostics;
using Unity.VisualScripting;

public class Timer_Script : MonoBehaviour
{

    public enum Timer_Style
    {
        Timer,
        StopWatch,
        Cooldown,
    }
    public TextMeshProUGUI TMPUGUI;
    public Timer_Style Style = 0;
    public float Timer_Time = 0;
    public UnityEvent Timer_Start;
    public UnityEvent Timer_Fin;
    private float Stopwatch_Time;
    public UnityEvent StopWatch_Start;
    public UnityEvent StopWatch_Fin;
    private float Cooldown_Time = 0;
    public float Cooldown_Max = 0;
    public bool Cooldown_On = false;
    public UnityEvent Cooldown_Start;
    public UnityEvent Cooldown_Fin;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cooldown_Time = Cooldown_Max;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Style == Timer_Style.Timer)
        {
            Timer(Timer_Time);
        }
        else if (Style == Timer_Style.StopWatch)
        {
            Stopwatch();
        }
        else if (Style == Timer_Style.Cooldown)
        {
            Cooldown(Cooldown_Time,Cooldown_Max);
        }
    }

    //Functions
    void Timer(float time)
    {
        bool HasTimerStarted = false;
        if(HasTimerStarted != true)
        {
            HasTimerStarted = true;
            Timer_Start.Invoke();
        }

        if (time > 0)
        {
            time -= Time.fixedDeltaTime;
            Timer_Time = time;
        } 
        else if (time < 0)
        {
            time = 0;
            Timer_Time = time;
            Timer_Fin.Invoke();
        }
        if (TMPUGUI != null)
        {
            int Min = Mathf.FloorToInt(time / 60);
            int Sec = Mathf.FloorToInt(time % 60);
            TMPUGUI.text = string.Format(Min + ":" + Sec);
        }
    }

    void Stopwatch()
    {
        Stopwatch_Time += Time.fixedDeltaTime;
        if  (TMPUGUI != null)
        {
            int Min = Mathf.FloorToInt(Stopwatch_Time / 60);
            int Sec = Mathf.FloorToInt(Stopwatch_Time % 60);
            TMPUGUI.text = string.Format(Min + ":" + Sec);
        }
    }
    
    void Cooldown(float time, float max)
    {
        if (Cooldown_On == true)
        {
            bool HasCooldownStarted = false;
            if (HasCooldownStarted != true)
            {
                HasCooldownStarted = true;
                Cooldown_Start.Invoke();
            }
            if (time <= 0)
            {
                Cooldown_Fin.Invoke();
                Cooldown_Time = max;
                Cooldown_On = false;
            }
            else if (time > 0)
            {
                time -= Time.fixedDeltaTime;
                Cooldown_Time = time;
            }
        } 
        if (TMPUGUI != null)
        {
            int Min = Mathf.FloorToInt(Cooldown_Time / 60);
            int Sec = Mathf.FloorToInt(Cooldown_Time % 60);
            TMPUGUI.text = string.Format(Min + ":" + Sec);
        }
    }
}
