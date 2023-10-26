using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ButtonScript : MonoBehaviour
{
    UIStateManager stateManager;
    Text ButtonText;



    // Start is called before the first frame update
    void Start()
    {
        stateManager = gameObject.GetComponentInParent<UIStateManager>();
        ButtonText = transform.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickChangeState()
    {
        stateManager.OnChange();

    }

    public void OnClickContinue()
    {
        stateManager.OnContinue();

    }

}
