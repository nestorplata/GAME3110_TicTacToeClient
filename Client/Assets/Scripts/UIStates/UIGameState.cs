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

        tittleText = "PLAYING";
        ButtonText = "EXIT GAME";
        TypeText = "Enter Message:";

    }
    // Start is called before the first frame update
    public override void EnterState(UIStateManager manager)
    {


    }

    public override void OnContinue(UIStateManager manager)
    {
        message = ClientToServerSignifiers.game + "," + ClientMessageType.OnContinue
            + "," + manager.Input2.text;
        manager.SendMessageToServer(message);

    }

    public override void OnReturn(UIStateManager manager)
    {
        message = ClientToServerSignifiers.game + "," + ClientMessageType.OnReturn
            + "," + manager.Input2.text;
        manager.SendMessageToServer(message);
    }

    public override string MessageRecieved(UIStateManager manager, string msg)
    {
        string[] csv = msg.Split(',');
        switch (int.Parse(csv[0]))
        {
            case ServerToClientSignifiers.BasicSuccess:
                message = csv[1];
                manager.ChangeState(EnumStateToContinue);
                break;
            case ServerToClientSignifiers.SuccessA:
                message = csv[1];
                manager.ChangeState(EnumStateToContinue);
                break;
            case ServerToClientSignifiers.ReturnSuccess:
                message = "Removed from Gameplay";
                manager.ChangeState(EnumStateToReturn);
                break;

        }
        return message;

    }


}
