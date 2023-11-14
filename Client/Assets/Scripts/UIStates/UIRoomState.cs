using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRoomState : UIBaseState
{
    // Start is called before the first frame update
    public override void StartState()
    {
        EnumState = UIStates.room;
        EnumStateToReturn = UIStates.lobby;
        EnumStateToContinue = UIStates.game;
        tittleText = "WAITING...";
        ButtonText = "Return";
        TypeText = "Enter Message:";


    }
    // Start is called before the first frame update
    public override void EnterState(UIStateManager manager)
    {
        manager.Input1.enabled = false;
        manager.Input2.enabled = false;
        manager.ContinueButton.enabled = false;

        OnContinue(manager);

    }
    public override void OnContinue(UIStateManager manager)
    {
        message = ClientToServerSignifiers.room + "," + ClientMessageType.OnContinue
            + "," + manager.Input2.text;
        manager.SendMessageToServer(message);
    }

    public override void OnReturn(UIStateManager manager)
    {
        message = ClientToServerSignifiers.room + "," + ClientMessageType.OnReturn
            + "," + manager.Input2.text;
        manager.SendMessageToServer(message);
    }

    public override string MessageRecieved(UIStateManager manager, string msg)
    {
        switch (int.Parse(msg))
        {
            case ServerToClientSignifiers.BasicSuccess:
                message = "Moved To GamePlay As Player";
                manager.ChangeState(EnumStateToContinue);
                break;
            case ServerToClientSignifiers.SuccessA:
                message = "Moved To GamePlay As Observer";
                manager.ChangeState(EnumStateToContinue);
                break;
            case ServerToClientSignifiers.ReturnSuccess:
                message = "Removed from GameRoom";
                manager.ChangeState(EnumStateToReturn);
                break;
            case ServerToClientSignifiers.BasicFailure:
                message = "Waiting for new player";
                break;
        }
        if (int.Parse(msg) == ServerToClientSignifiers.ReturnSuccess &&
            int.Parse(msg) == ServerToClientSignifiers.BasicSuccess )
        {
            manager.Input1.enabled = true;
            manager.Input2.enabled = true;
            manager.ContinueButton.enabled = true;
        }
        return message;

    }


}
