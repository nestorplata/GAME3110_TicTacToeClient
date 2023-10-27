using UnityEngine;
using UnityEngine.UI;

public class UIGameState : UIBaseState
{
    public override void StartState(UIStateManager manager)
    {
        EnumState = UIStates.game;
        EnumStateToReturn = UIStates.room;

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


    }
    public override void OnContinue(UIStateManager manager)
    {

    }

    public override void OnReturn(UIStateManager manager)
    {
        manager.networkClient.SendMessageToServer(manager.currentState.EnumState.ToString() +
    ',' + manager.InputUserName.text + ',' + manager.InputPassword.text + ",1," +
    manager.networkClient.GetNetworkConnectionID());


    }
}
