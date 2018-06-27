using System;
using UnityEngine;

/// <summary>
/// Spins a gameobject
/// </summary>
public class SpinnerComponent : MonoBehaviour
{
    public float degreesToSpin;
    public float speedMultiplier;
    public float flatSpeedAdd;
    public delegate void SpinEndedEvent();
    public SpinEndedEvent spinEndedEvent;
    private bool triggered;
    private static readonly float floatInprecision = 0.001f;

    private void Update()
    {
        if(degreesToSpin > 0f)
        {
            float val = (degreesToSpin + flatSpeedAdd) * speedMultiplier * Time.deltaTime;
            if (val > degreesToSpin)
            {
                val = degreesToSpin;
            }

            Rotate(val);
            degreesToSpin -= val;

            if(!triggered && degreesToSpin <= 0 + floatInprecision)
            {
                spinEndedEvent.Invoke();
                triggered = true;
            }
        }
    }

    private void Rotate(float val)
    {
        transform.Rotate(Vector3.forward * val);
    }

    internal void SpinDegrees(float v)
    {
        degreesToSpin = v;
        triggered = false;
    }
}