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
    }
    // Start is called before the first frame update
    public override void EnterState(UIStateManager manager)
    {
        manager.GetInputFieldComponent(InputNumber.Input1).enabled = false;
        manager.GetInputFieldComponent(InputNumber.Input2).enabled = false;
        manager.GetButtonComponent(ClientMessageType.OnContinue).enabled = false;

        manager.TittleText.text = "WAITING FOR NEW PLAYER";
        manager.GetButtonText(ClientMessageType.OnReturn).text = "Return";
        manager.GetDescriptionText(InputNumber.Input1).text = "Enter Message:";

        OnContinue(manager);

    }
    public override void OnContinue(UIStateManager manager)
    {
        message = ClientToServerSignifiers.room + "," + ClientMessageType.OnContinue
            + "," + manager.GetInputFieldText(InputNumber.Input2).text;
        manager.SendMessageToServer(message);
    }

    public override void OnReturn(UIStateManager manager)
    {
        message = ClientToServerSignifiers.room + "," + ClientMessageType.OnReturn;
        manager.SendMessageToServer(message);
    }

    public override string MessageRecieved(UIStateManager manager, string msg)
    {
        switch (int.Parse(msg))
        {
            case ServerToClientSignifiers.BasicSuccess:
                message = "Moved To GamePlay As Player";
                manager.ChangeStateTo(EnumStateToContinue);
                break;
            case ServerToClientSignifiers.SuccessA:
                message = "Moved To GamePlay As Observer";
                manager.ChangeStateTo(EnumStateToContinue);
                break;
            case ServerToClientSignifiers.ReturnSuccess:
                message = "Removed from GameRoom";
                manager.ChangeStateTo(EnumStateToReturn);
                break;
            case ServerToClientSignifiers.BasicFailure:
                message = "Waiting for new player";
                break;
        }
        if (int.Parse(msg) == ServerToClientSignifiers.ReturnSuccess ||
            int.Parse(msg) == ServerToClientSignifiers.BasicSuccess )
        {
            manager.GetInputFieldComponent(InputNumber.Input1).enabled = true;
            manager.GetInputFieldComponent(InputNumber.Input2).enabled = true;
            manager.GetButtonComponent(ClientMessageType.OnContinue).enabled = true;
        }
        return message;

    }


}
