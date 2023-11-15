using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public enum UIStates
{
    login,
    create,
    lobby,
    room,
    game
}

public class UIStateManager : MonoBehaviour
{
    public List<GameObject> HidablesList = new List<GameObject>();

    public List<GameObject> Buttons = new List<GameObject>();
    public List<GameObject> Descriptions = new List<GameObject>();
    public List<GameObject> Fields = new List<GameObject>();


    List<UIBaseState> stateList = new List<UIBaseState>();
    UILoginState loginState = new UILoginState();
    UICreateState createState = new UICreateState();
    UILobbyState lobbyState = new UILobbyState();
    UIRoomState RoomState = new UIRoomState();
    UIGameState GameState = new UIGameState();

    public GameObject InputHolder1;
    public UIBaseState currentState;
    public Text TittleText;


    void Start()
    {
        NetworkClientProcessing.SetStateManager(this);

        stateList.Add(loginState);
        stateList.Add(createState);
        stateList.Add(lobbyState);
        stateList.Add(RoomState);
        stateList.Add(GameState);
        foreach (UIBaseState state in stateList)
        {
            state.StartState();
        }
        currentState = stateList[0];

    }
    
    public void OnReturn()
    {
        currentState.OnReturn(this);
    }

    public void ChangeStateTo(UIStates state)
    {
        if (currentState.EnumState != currentState.EnumStateToReturn)
        {
            foreach (var UI in stateList)
            {
                if (state == UI.EnumState)
                {

                    currentState = UI;
                    break;
                }
            }
            currentState.EnterState(this);

        }

    }



    public void OnContinue()
    {

        char[] characters = GetInputFieldText(InputNumber.Input1).text.ToCharArray();
        char[] InvalidCharacters = "/\\?%*:|\"<>".ToCharArray();

        if (GetInputFieldText(InputNumber.Input1).text != "" ||
            GetInputFieldText(InputNumber.Input1).text !="Enter text...")
        {
            foreach (char c in characters)
            {
                foreach (char I in InvalidCharacters)
                {
                    if (c == I)
                    {
                        Debug.Log("\"" + c + "\"" + " Invalid character");
                        return;
                    }
                }
            }
            if (GetInputFieldText(InputNumber.Input2).text != "" ||
            GetInputFieldText(InputNumber.Input2).text != "Enter text...")
            {
                currentState.OnContinue(this);
            }
            else
            {
                Debug.Log("No password introduced");

            }
        }
        else
        {
            Debug.Log("No username introduced");
        }
        
    }

    public void SendMessageToServer(string msg)
    {
        NetworkClientProcessing.SendMessageToServer(msg, TransportPipeline.ReliableAndInOrder);

    }

    public Text GetInputFieldText(int signifier)
    {
        return Fields[signifier].GetComponentInChildren<Text>();
    }
    public Text GetButtonText(int signifier)
    {
        return Buttons[signifier].GetComponentInChildren<Text>();
    }
    public Text GetDescriptionText(int signifier)
    {
        return Descriptions[signifier].GetComponent<Text>();
    }

    public InputField GetInputFieldComponent(int signifier)
    {
        return Fields[signifier].GetComponent<InputField>();
    }
    public Button GetButtonComponent(int signifier)
    {
        return Buttons[signifier].GetComponent<Button>();
    }

}

#region Protocol Signifiers
static public class ClientToServerSignifiers
{
    public const int none = 0;
    public const int login = 1;
    public const int create = 2;
    public const int lobby = 3;
    public const int room = 4;
    public const int game = 5;
}

static public class ClientMessageType
{
    public const int OnContinue = 0;
    public const int OnReturn = 1;
    public const int OnSpecial = 2;
}

static public class ServerToClientSignifiers
{
    public const int BasicSuccess = 0;
    public const int SuccessA = 1;
    public const int ReturnSuccess = 2;
    public const int BasicFailure = 4;
    public const int FailureA = 5;
    public const int FailureB = 6;
}

static public class InputNumber
{
    public const int Input1 = 0;
    public const int Input2 = 1;
}
#endregion



