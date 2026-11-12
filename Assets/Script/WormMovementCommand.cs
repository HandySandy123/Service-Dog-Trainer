using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class WormMovementCommand : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    //[SerializeField] private InputActionReference commandPosition;
    public Vector3 desiredPosition;

    private void OnEnable()
    {
        SystemManager.CommandWormToNewPosition += moveToPosition;
    }

    private void OnDisable()
    {
        SystemManager.CommandWormToNewPosition -= moveToPosition;
    }

    private void Update()
    {
        
    }

    private void moveToPosition(Vector3 position)
    {
        Debug.Log("I am worm and I am moving to new position");
        desiredPosition = position;
        transform.LookAt(position);
        var coroutinePos = MoveEnumerator(position);
        StartCoroutine(coroutinePos);
    }

    private IEnumerator MoveEnumerator(Vector3 position)
    {
        while (Vector3.Distance(transform.position, position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * speed);
            yield return null;
        }
        Debug.Log("Moved to position");
    }
}
