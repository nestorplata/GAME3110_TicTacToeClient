using System.Collections;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class UIGameState : UIBaseState
{
    public override void StartState(UIStateManager manager)
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
        if(manager.networkClient.GetMesssage() != "success,Observer Moved to GamePlay")
        {
            manager.InputUserName.enabled = true;
            manager.InputPassword.enabled = true;
            manager.ContinueButton.enabled = true;
        }




    }

    public override void OnReturn(UIStateManager manager)
    {
        manager.networkClient.SendMessageToServer(manager.currentState.EnumState.ToString() +
    ',' + manager.InputUserName.text + ',' + manager.InputPassword.text + ",1," +
    manager.networkClient.GetNetworkConnectionID());


    }
    

}
