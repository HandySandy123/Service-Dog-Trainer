using System;
using Script;
using UnityEngine;

public class Treat : InteractableObject
{
    private void OnEnable()
    {
        SystemManager.Interact += Pickup;
    }

    private void OnDisable()
    {
        SystemManager.Interact -= Pickup;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Pickup(Vector3 position, Hand hand)
    {
        transform.position = position;
    }
    
}
