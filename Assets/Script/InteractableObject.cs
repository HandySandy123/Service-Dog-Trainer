using UnityEngine;

namespace Script
{
    public abstract class InteractableObject : MonoBehaviour
    {
        public abstract void Pickup(Vector3 position, Hand hand);
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            gameObject.tag = "InteractableObject";
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
