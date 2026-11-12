using System;
using System.Diagnostics;
using Script;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;

public class Hand : MonoBehaviour
{
    [SerializeField] private bool leftHand;

    [FormerlySerializedAs("closestIObject")] public float closestInteractiveObject;
    private InteractableObject trackedItem;
    public Transform target;
    [SerializeField] private InputActionReference pickUpAction;
    public bool canGrab = false; 

    private void OnEnable()
    {
        SystemManager.HandCanGrab += setCanGrab;
        SystemManager.HandCannotGrab += setCanGrab;
        SystemManager.Interact += grab;
    }

    private void OnDisable()
    {
        SystemManager.HandCanGrab -= setCanGrab;
        SystemManager.HandCannotGrab -= setCanGrab;
        SystemManager.Interact -= grab;
    }

    private void setCanGrab(bool canGrab)
    {
        this.canGrab = canGrab;
    }

    private void Update()
    {
        // if (pickUpAction.action.WasPressedThisFrame())
        // {
        //     Debug.Log("pickup");
        //     grab(trackedItem);
        // }
    }

    public void drawRayFromHand(Color drawColor, InteractableObject interactableObject)
    {
        
        Debug.DrawLine(this.transform.position, trackedItem.transform.position, drawColor, 0.5f);
    }

    public void grab(Vector3 position, Hand hand)
    {
        if (canGrab)
        {
            trackedItem.Pickup(target.position, this);
        }
    }

    public void setDistance(float distance, float maximumDistance)
    {
       closestInteractiveObject = distance;
       if (closestInteractiveObject < maximumDistance)
       {
           canGrab = true;
       }
    }

    public void setGrabbableObject(InteractableObject trackedItem)
    {
        this.trackedItem = trackedItem;
    }
}
