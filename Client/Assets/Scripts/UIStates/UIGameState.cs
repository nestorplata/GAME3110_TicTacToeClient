using System;
using System.Collections;
using UnityEditor.Experimental.GraphView;
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
        for (int i = 0; i < ValidCharachters.Length; i++)
        {
            ValidCharachters[i] = (char)(49 + i);
        }
    }
    // Start is called before the first frame update
    public override void EnterState(UIStateManager manager)
    {
        manager.Buttons[ClientMessageType.OnSpecial].SetActive(true);
        manager.InputHolder1.SetActive(true);



        manager.GetButtonText(ClientMessageType.OnReturn).text = "EXIT GAME";
        manager.GetDescriptionText(InputNumber.Input1).text = "Enter Move:";
        manager.GetDescriptionText(InputNumber.Input2).text = "Enter Message:";
        manager.GetButtonText(ClientMessageType.OnContinue).text = "SEND MESSAGE";

    }
    //message
    public override void OnContinue(UIStateManager manager)
    {
        message = ClientToServerSignifiers.game + "," + ClientMessageType.OnContinue
            + "," + manager.GetInputFieldText(InputNumber.Input2).text;

        manager.SendMessageToServer(message);
    }

    public override void OnReturn(UIStateManager manager)
    {
        message = ClientToServerSignifiers.game + "," + ClientMessageType.OnReturn;
        manager.SendMessageToServer(message);
    }

    //Game
    public override void OnSpecial(UIStateManager manager)
    {
        char[] characters = manager.GetInputFieldText(InputNumber.Input1).text.ToCharArray();

        if (characters.Length == 1)
        {
            foreach (char valid in ValidCharachters)
            {
                if (characters[0] == valid)
                {
                    message = ClientToServerSignifiers.game + "," + ClientMessageType.OnSpecial
                        + "," + manager.GetInputFieldText(InputNumber.Input1).text;
                    manager.SendMessageToServer(message);
                    return;
                }
            }
            Debug.Log("Invalid Character");

        }
        else
        {
            Debug.Log("incorrect move amount code");
        }

    }
    public override void MessageRecieved(UIStateManager manager, string[] msg)
    {
        switch (int.Parse(msg[0]))
        {
            case ServerToClientSignifiers.ReturnSuccess:
                manager.ChangeStateTo(EnumStateToReturn);
                break;

            case ServerToClientSignifiers.SpecialSuccess:
                manager.GetButtonComponent(ClientMessageType.OnSpecial).enabled = false;
                manager.GetInputFieldComponent(InputNumber.Input1).enabled = false;

                break;
            case ServerToClientSignifiers.EnemyMoved:
                manager.GetButtonComponent(ClientMessageType.OnSpecial).enabled = true;
                manager.GetInputFieldComponent(InputNumber.Input1).enabled = true;

                break;
        }

    }
    


}
