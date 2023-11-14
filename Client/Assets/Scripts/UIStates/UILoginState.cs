
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


        tittleText = "LOGIN";
        ButtonText = "TO MAKE ACCOUNT";
        TypeText = "Enter Password:";

    }
    public override void EnterState(UIStateManager manager)
    {
        foreach (GameObject garbage in manager.HidablesList)
        {
            garbage.SetActive(true);
        }
    }
    public override void OnContinue(UIStateManager manager)
    {
        message = ClientToServerSignifiers.login + "," + ClientMessageType.OnContinue
            + "," + manager.Input1.text + "_" + manager.Input2.text;
        manager.SendMessageToServer(message);

    }
    public override void OnReturn(UIStateManager manager)
    {
        manager.ChangeState(EnumStateToContinue);

    }
    public override string MessageRecieved(UIStateManager manager, string msg)
    {
        switch (int.Parse(msg))
        {
            case ServerToClientSignifiers.BasicSuccess:
                message = "logged in";
                manager.ChangeState(EnumStateToContinue);
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
