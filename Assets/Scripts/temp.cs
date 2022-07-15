using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("a");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("b");
    }
    
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("C");
    }
}
