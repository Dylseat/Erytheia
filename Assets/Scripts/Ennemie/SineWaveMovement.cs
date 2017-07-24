using UnityEngine;
using System.Collections;

public class SineWaveMovement : BaseMovement
{
    [SerializeField]
    private float frequency = 1f;
    [SerializeField]
    private float wavelength = 1f;

    protected override float GetY()
    {
        // try and mess with some of the values here to see what dif results you get...
        return Mathf.Sin(2 * Mathf.PI * Time.time * frequency) * wavelength;
    }

    protected override void Move()
    {
        base.Move();
    }
}