using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] float mainThrust = 100.0f;
    [SerializeField] float rotationSpeed = 100.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() 
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime); // Shorthand for 0, 1, 0
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D)) 
        {
            ApplyRotation(Vector3.back);
        }
    }

    private void ApplyRotation(Vector3 direction)
    {
        rb.freezeRotation = true; // Freezing rotation so we can manually rotate
        transform.Rotate(direction * rotationSpeed * Time.deltaTime);
        rb.freezeRotation = false; // Unfreeze rotation so the physics system can take over
    }
}
