using System;
using System.Collections;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class UIGameState : UIBaseState
{
    public override void StartState()
    {
        EnumState = UIStates.game;
        EnumStateToContinue = UIStates.game;
        EnumStateToReturn = UIStates.lobby;
    }
    // Start is called before the first frame update
    public override void EnterState(UIStateManager manager)
    {
        manager.TittleText.text = "PLAYING";
        manager.GetButtonText(ClientMessageType.OnReturn).text = "EXIT GAME";
        manager.GetDescriptionText(InputNumber.Input1).text = "Enter Move:";
        manager.GetDescriptionText(InputNumber.Input2).text = "Enter Message:";

    }

    public override void OnContinue(UIStateManager manager)
    {
        message = ClientToServerSignifiers.game + "," + ClientMessageType.OnContinue
            + "," + manager.GetInputFieldText(InputNumber.Input1).text;

        manager.SendMessageToServer(message);

    }

    public override void OnReturn(UIStateManager manager)
    {
        message = ClientToServerSignifiers.game + "," + ClientMessageType.OnReturn;
        manager.SendMessageToServer(message);
    }

    public override string MessageRecieved(UIStateManager manager, string msg)
    {
        string[] csv = msg.Split(',');
        switch (int.Parse(csv[0]))
        {
            case ServerToClientSignifiers.BasicSuccess:
                message = csv[1];
                manager.ChangeStateTo(EnumStateToContinue);
                break;
            case ServerToClientSignifiers.SuccessA:
                message = csv[1];
                manager.ChangeStateTo(EnumStateToContinue);
                break;
            case ServerToClientSignifiers.ReturnSuccess:
                message = "Removed from Gameplay";
                manager.ChangeStateTo(EnumStateToReturn);
                break;

        }
        return message;

    }


}
