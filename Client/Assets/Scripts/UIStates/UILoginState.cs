
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Windows;

public class UILoginState : UIBaseState
{
    public override void StartState()
    {
        EnumState = UIStates.login;
        EnumStateToReturn = UIStates.create;
        EnumStateToContinue = UIStates.lobby;

    }
    public override void EnterState(UIStateManager manager)
    {
        manager.HidablesList[0].SetActive(true);
        manager.HidablesList[1].SetActive(false);

        manager.TittleText.text = "LOGIN";
        manager.GetButtonText(ClientMessageType.OnReturn).text = "TO MAKE AN ACCOUNT";
        manager.GetDescriptionText(InputNumber.Input1).text = "Enter Password:";
    }
    public override void OnContinue(UIStateManager manager)
    {
        message = ClientToServerSignifiers.login + "," + ClientMessageType.OnContinue + "," + 
            manager.GetInputFieldText(InputNumber.Input1).text + "_" +
            manager.GetInputFieldText(InputNumber.Input2).text;
        manager.SendMessageToServer(message);

    }
    public override void OnReturn(UIStateManager manager)
    {
        manager.ChangeStateTo(EnumStateToReturn);

    }
    public override string MessageRecieved(UIStateManager manager, string msg)
    {
        switch (int.Parse(msg))
        {
            case ServerToClientSignifiers.BasicSuccess:
                message = "logged in";
                manager.ChangeStateTo(EnumStateToContinue);
                break;
            case ServerToClientSignifiers.BasicFailure:
                message = "Wrong Username";
                break;
            case ServerToClientSignifiers.FailureA:
                message = "Wrong Password";
                break;
            case ServerToClientSignifiers.FailureB:
                message = "Account Already in use";
                break;

        }
        return message;
    }



}
