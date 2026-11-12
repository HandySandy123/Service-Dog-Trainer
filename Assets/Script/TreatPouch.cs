using System;
using System.Collections.Generic;
using Script;
using UnityEngine;
using Random = UnityEngine.Random;

public class TreatPouch : InteractableObject
{
    public Treat Treat;
    public int maxTreatsInScene = 50;
    private Queue<Treat> treats = new Queue<Treat>();

    private void OnEnable()
    {
        SystemManager.Interact += Pickup;

    }

    private void OnDisable()
    {
        SystemManager.Interact -= Pickup;
    }

    public override void Pickup(Vector3 position, Hand hand)
    {
        var treatMat = Instantiate(Treat.GetComponent<MeshRenderer>().material);
        //var array = treatMat.GetPropertyNames(MaterialPropertyType.Float);
        // Debug.Log(array.Length);
        // foreach (var name in array)
        // {
        //     Debug.Log(name);
        // }
        //
        // Debug.Log(treatMat.GetFloat("_DisplaceValue"));
        treatMat.SetFloat("_DisplaceValue", Random.value*360.0f);
        //Debug.Log(treatMat.GetFloat("_DisplaceValue"));
        var newTreat = Instantiate(this.Treat, position, Quaternion.identity, this.transform);
        //newTreat.transform.localScale = Treat.transform.localScale;
        newTreat.GetComponent<MeshRenderer>().material = treatMat;
        newTreat.transform.SetParent(hand.transform);
        newTreat.transform.position = hand.target.position;
        newTreat.transform.rotation = hand.transform.rotation;
        Debug.Log("Getting new Treat");
        AddToTreatsArray(newTreat);
    }

    private void AddToTreatsArray(Treat newTreat)
    {
        if(treats.Count >= maxTreatsInScene) {Destroy(treats.Dequeue());}
        treats.Enqueue(newTreat);
    }
}
