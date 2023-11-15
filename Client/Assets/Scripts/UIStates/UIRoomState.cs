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

        manager.TittleText.text = "WAITING ROOM";
        manager.GetButtonText(ClientMessageType.OnReturn).text = "RETURN";
        manager.GetDescriptionText(InputNumber.Input1).text = "Waiting For New Player";
        manager.GetButtonText(ClientMessageType.OnContinue).text = "NO AVAILABLE";


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
    public override void OnSpecial(UIStateManager manager)
    {

    }
    public override void MessageRecieved(UIStateManager manager, string[] msg)
    {
        switch (int.Parse(msg[0]))
        {
            case ServerToClientSignifiers.ContinueSuccess:
                manager.TittleText.text = "PLAYING";
                manager.GetInputFieldComponent(InputNumber.Input1).enabled = true;
                manager.GetInputFieldComponent(InputNumber.Input2).enabled = true;
                manager.GetButtonComponent(ClientMessageType.OnContinue).enabled = true;
                manager.ChangeStateTo(EnumStateToContinue);
                break;

            case ServerToClientSignifiers.ContinueAsObserver:
                manager.TittleText.text = "OBSERVING";
                manager.GetInputFieldComponent(InputNumber.Input1).enabled = false;
                manager.GetInputFieldComponent(InputNumber.Input2).enabled = false;
                manager.GetButtonComponent(ClientMessageType.OnContinue).enabled = false;
                manager.ChangeStateTo(EnumStateToContinue);
                break;

            case ServerToClientSignifiers.ReturnSuccess:

                manager.ChangeStateTo(EnumStateToReturn);
                break;
            case ServerToClientSignifiers.Failure:

                //manager.ChangeStateTo(EnumStateToReturn);
                break;

        }


 

    }


}
