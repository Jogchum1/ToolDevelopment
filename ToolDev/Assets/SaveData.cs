using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[System.Serializable]
public class SaveData
{

    public string name;
    public float boidAmount;
    public float speed;
    public float separation;
    public float cohesion;
    public float push;

    

    [OnSerializing]
    void OnSerializing(StreamingContext context)
    {
        Debug.Log("wordt gesaved");
    }

    [OnSerialized]
    void OnSerialized(StreamingContext context)
    {
        Debug.Log("is gesaved");
    }

    [OnDeserializing]
    void OnDeserializing(StreamingContext context)
    {
        Debug.Log("wordt geladen");
    }

    [OnDeserialized]
    void OnDeserialized(StreamingContext context)
    {
        Debug.Log("is geladen");

        // Binary Defaults...
        
    }
}
