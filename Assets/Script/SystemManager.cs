using System;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    public static Action<bool> HandCanGrab;
    public static Action<bool> HandCannotGrab;
    public static Action<Vector3, Hand> Interact;
    public static Action<Vector3> CommandWormToNewPosition;

    public static void InvokeHandCanGrab(bool leftHand)
    {
        //Debug.Log("Invoking HandCanGrab");
        HandCanGrab?.Invoke(leftHand);
    }
    public static void InvokeHandCannotGrab(bool leftHand)
    {
        //Debug.Log("Invoking HandCannotGrab");
        HandCannotGrab?.Invoke(leftHand);
    }

    public static void InvokeInteract(Vector3 position, Hand hand)
    {
        Interact?.Invoke(position, hand);
    }

    public static void InvokeCommandWormToNewPosition(Vector3 position)
    {
        CommandWormToNewPosition?.Invoke(position);
    }
}
