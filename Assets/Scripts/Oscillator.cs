using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    // [SerializeField] [Range(0,1)] 
    float movementFactor;
    [SerializeField] float period = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) return;

        float cycles = Time.time / period; // Continually growing over time
        const float tau = Mathf.PI * 2; // Constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); // Going from -1 to 1

        movementFactor = (rawSinWave + 1.0f) / 2.0f; // Take our value from -1 - 1 to 0 - 1

        Vector3 offset = movementVector * movementFactor;
        Vector3 newPosition = startingPosition + offset;
        transform.position = newPosition;
    }
}
