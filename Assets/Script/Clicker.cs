using Script;
using UnityEngine;

public class Clicker : InteractableObject
{
    public override void Pickup(Vector3 position, Hand hand)
    {
        transform.position = position;
        
    }
}
