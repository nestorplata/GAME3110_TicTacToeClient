
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
        manager.InputHolder1.SetActive(true);
        manager.Buttons[ClientMessageType.OnSpecial].SetActive(false);

        manager.TittleText.text = "LOGIN";
        manager.GetButtonText(ClientMessageType.OnReturn).text = "TO MAKE AN ACCOUNT";
        manager.GetDescriptionText(InputNumber.Input2).text = "Enter Password:";
        manager.GetButtonText(ClientMessageType.OnContinue).text = "CONTINUNE";

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
    public override void OnSpecial(UIStateManager manager)
    {

    }
    public override void MessageRecieved(UIStateManager manager, string[] msg)
    {
        switch (int.Parse(msg[0]))
        {
            case ServerToClientSignifiers.ContinueSuccess:
                manager.ChangeStateTo(EnumStateToContinue);
                break;


        }

    }



}
