using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class text : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int argNumber;
        string argMessage;
        string? argDefault;
        random? argInan;

        Method(out argNumber, out argMessage, out argDefault, out argInan);
        Debug.Log(argNumber);
        Debug.Log(argMessage);
        Debug.Log(argDefault == null);
        Debug.Log(argInan == null);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Method(out int answer, out string message, out string? stillNull, out random Inan)
    {
        answer = 44;
        message = "I've been returned";
        stillNull = null;
        Inan = universe;
    }

    random universe = new random();

}

public class random : MonoBehaviour
{
    public random(){
        
        }

    int i;
}