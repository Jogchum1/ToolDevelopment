using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropdownTest : MonoBehaviour
{
    public GameObject PrefabA;
    public GameObject PrefabB;
    

    public BoidMaster boidMaster;

    public void Start()
    {
        //boidMaster.replaceBoids(PrefabA);
    }

    public void HandleInputData(int val)
    {
        if(val == 0)
        {
            Debug.Log("A");
            boidMaster.replaceBoids(PrefabA);
        }
        if(val == 1)
        {
            Debug.Log("B");
            boidMaster.replaceBoids(PrefabB);
        }
        if (val == 3)
        {
            Debug.Log("C");
           // boidMaster.replaceBoids(PrefabC);
        }
    }
}
