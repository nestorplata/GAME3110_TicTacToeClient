using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ButtonScript : MonoBehaviour
{
    UIStateManager stateManager;
    Text ButtonText;
    public UIStates EnumState;


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
        stateManager.SetState(EnumState);
        switch (EnumState)
        {
            case UIStates.login:
                EnumState = UIStates.create;
                break;

            case UIStates.create:
                EnumState = UIStates.login;
                break;
        }
        ButtonText.text = stateManager.GetState().GetButtonText();

    }

    public void OnClickContinue()
    {
        stateManager.Continue();

    }

}
