using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRoomState : UIBaseState
{
    // Start is called before the first frame update
    public override void StartState(UIStateManager manager)
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
        manager.InputUserName.enabled = false;
        manager.InputPassword.enabled = false;
        manager.ContinueButton.enabled = false;

        manager.networkClient.SendMessageToServer(manager.currentState.EnumState.ToString() +
            ',' + manager.InputUserName.text + ',' + manager.InputPassword.text + ",0"
            , TransportPipeline.ReliableAndInOrder);

    }

    public override void OnReturn(UIStateManager manager)
    {
        manager.networkClient.SendMessageToServer(manager.currentState.EnumState.ToString() +
            ',' + manager.InputUserName.text + ',' + manager.InputPassword.text + ",1"
            , TransportPipeline.ReliableAndInOrder);


    }


}
