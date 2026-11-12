using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Script;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

public class Controller : MonoBehaviour
{
    [SerializeField] public InteractableObject Pouch, Clicker;
    [SerializeField] private Hand leftHand, rightHand;
    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private InputActionReference commandWorm, interact;
    [SerializeField] private Transform testPositionForWormMovement;
    public static List<InteractableObject> interactableObjects;


    public float MaximumDistanceForGrab;
    
    [Header("Debugging")] 
    public bool printDistances;

    public Color closeEnoughColor, tooFarColor;
    private Color leftHandColor, rightHandColor;

    private void OnEnable()
    {
        SystemManager.HandCanGrab += setColorToTrue;
        SystemManager.HandCannotGrab += setColorToFalse;
    }

    private void OnDisable()
    {
        SystemManager.HandCanGrab -= setColorToTrue;
        SystemManager.HandCannotGrab -= setColorToFalse;
    }
    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // commandWorm = actionAsset.FindAction("CommandThere");
        // if(commandWorm == null) Debug.LogError("CommandWorm action not found");
        // interact = actionAsset.FindAction("Interact");
        // if(interact == null) Debug.LogError("Interact action not found");
        // if (commandWorm != null)
        // {
        //     Debug.Log(commandWorm);
        //     commandWorm.Enable();
        // }
        findActions(commandWorm, "CommandThere");
        findActions(interact, "Interact");
        interactableObjects = findObjectsInSceneByType<InteractableObject>();
        foreach (var interactableObject in interactableObjects)
        {
            Debug.Log(interactableObject.name);
        }

    }

    private List<T> findObjectsInSceneByType<T>() where T : MonoBehaviour
    {
        var array = (T[])Resources.FindObjectsOfTypeAll(typeof(T));
        return array.ToList();
    }

    private void findActions(InputAction action, string actionName)
    {
        action = actionAsset.FindAction(actionName);
        if (action == null) Debug.LogError("Action not found: " + actionName);
        if (action != null)
        {
            action.Enable();
            Debug.Log("Action: " + action.name + " is enabled");
        }
    }

    // Update is called once per frame
    void Update()
    {
        setDistances(printDistances);
        drawLinesFromHands();
        if (commandWorm.action.triggered)
        {
            Debug.Log("Worm, follow my command");
            SystemManager.InvokeCommandWormToNewPosition(testPositionForWormMovement.position);
        }
        if (interact.action.triggered)
        {
            Debug.Log("Interacting");
            if (leftHand.canGrab)
            {
                SystemManager.InvokeInteract(leftHand.transform.position, leftHand);
            } else Debug.Log("No interacting on the left hand");
            if (rightHand.canGrab)
            {
                SystemManager.InvokeInteract(rightHand.transform.position, rightHand);
            } else Debug.Log("No interacting on the right hand");
            
        }
        
    }

    public void setColorToTrue(bool handedness)
    {
        if(handedness) leftHandColor = closeEnoughColor;
        else rightHandColor = closeEnoughColor;
    }

    public void setColorToFalse(bool handedness)
    {
        if(handedness) leftHandColor = tooFarColor;
        else rightHandColor = tooFarColor;
    }

    private void drawLinesFromHands()
    {
        if (!printDistances) return;
        leftHand.drawRayFromHand(leftHandColor, Pouch);
        rightHand.drawRayFromHand(rightHandColor, Pouch);
    }

    private void setDistances(bool Print)
    {
        //Debug.Log("Print distances");
        var closestLeftObject = interactableObjects[0];
        var closestLeftDistance = float.MaxValue;
        var closestRightObject = interactableObjects[0];
        var closestRightDistance = float.MaxValue;
        for (int i = 1; i < interactableObjects.Count; i++)
        {
            if (Vector3.Distance(interactableObjects[i].transform.position, closestLeftObject.transform.position) <=
                closestLeftDistance)
            {
                closestLeftDistance = Vector3.Distance(interactableObjects[i].transform.position, leftHand.transform.position);
                closestLeftObject = interactableObjects[i];
                
            }

            if (Vector3.Distance(interactableObjects[i].transform.position, closestRightObject.transform.position) <=
                closestRightDistance)
            {
                closestRightDistance = Vector3.Distance(interactableObjects[i].transform.position, rightHand.transform.position);
                closestRightObject = interactableObjects[i];
            }
        }
        leftHand.setGrabbableObject(closestLeftObject);
        rightHand.setGrabbableObject(closestRightObject);
        
    }
}
