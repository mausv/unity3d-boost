using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Parameters - for tuning, typically set in the editor
    // Cache - e.g. ref for readability or speed
    // State - private instance (member) variables

    [SerializeField] float mainThrust = 100.0f;
    [SerializeField] float rotationSpeed = 100.0f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;


    private Rigidbody rb;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space)) StartThrusting();
        else StopThrusting();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A)) RotateLeft();
        else if (Input.GetKey(KeyCode.D)) RotateRight();
        else StopRotating();
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime); // Shorthand for 0, 1, 0
        if (!audioSource.isPlaying) audioSource.PlayOneShot(mainEngine);
        if (!mainEngineParticles.isPlaying) mainEngineParticles.Play();
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void RotateRight()
    {
        ApplyRotation(Vector3.back);
        if (!leftThrustParticles.isPlaying) leftThrustParticles.Play();
    }

    private void RotateLeft()
    {
        ApplyRotation(Vector3.forward);
        if (!rightThrustParticles.isPlaying) rightThrustParticles.Play();
    }

    private void StopRotating()
    {
        rightThrustParticles.Stop();
        leftThrustParticles.Stop();
    }

    private void ApplyRotation(Vector3 direction)
    {
        rb.freezeRotation = true; // Freezing rotation so we can manually rotate
        transform.Rotate(direction * rotationSpeed * Time.deltaTime);
        rb.freezeRotation = false; // Unfreeze rotation so the physics system can take over
    }
}
