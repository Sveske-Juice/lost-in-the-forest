using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnMouseExplotion : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSystem = default; // Assign your particle system in the inspector

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Checks if left mouse button is clicked
        {
            // Convert mouse position to world position
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0; // Assuming you want the particle system in the XY plane
            Particle(particleSystem, worldPosition);
        }
    }
    void Particle(ParticleSystem particleSystem,Vector3 possitionOfParticalEffect)
    {
        // Instantiate the particle system with the same rotation as the original
        ParticleSystem ps = Instantiate(particleSystem, possitionOfParticalEffect, particleSystem.transform.rotation) as ParticleSystem;
        ps.Play();
        Destroy(ps.gameObject, 2); // Destroy after 3 seconds
    }
}
