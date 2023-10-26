using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.VersionControl.Asset;

public enum UIStates
{
    login,
    create,
    lobby
}

public class UIStateManager : MonoBehaviour
{
    public List<GameObject> HidablesList = new List<GameObject>();
    List<UIBaseState> stateList = new List<UIBaseState>();

    NetworkClient networkClient;

    UIBaseState currentState;
    UILoginState loginState = new UILoginState();
    UICreateState createState = new UICreateState();
    UILobbyState lobbyState = new UILobbyState();


    InputField InputUserName;
    InputField InputPassword;

    public Text ButtonText;
    public Text TittleText;
    public Text TypeText;

    void Start()
    {
        networkClient = GameObject.Find("EventSystem").GetComponent<NetworkClient>();

        stateList.Add(loginState);
        stateList.Add(createState);
        stateList.Add(lobbyState);

        foreach(UIBaseState state in stateList)
        {
            state.StartState(this);
        }


        currentState = stateList[0];

        InputUserName = transform.Find("username").GetComponent<InputField>();
        InputPassword =transform.Find("password").GetComponent<InputField>();

    }

    // Update is called once per frame


    public void OnChange()
    {
        ChangeState(currentState.EnumStateToChange);
    }

    public void ChangeState(UIStates state)
    {
        foreach (var UI in stateList)
        {
            if (state == UI.EnumState)
            {
                currentState = UI;
                break;
            }
        }
        ButtonText.text = currentState.ButtonText;
        TittleText.text = currentState.tittleText;
        TypeText.text = currentState.TypeText;
        currentState.EnterState(this);

    }



    public void OnContinue()
    {
        char[] characters = InputUserName.text.ToCharArray();
        string InvalidString = "/\\?%*:|\"<>";
        char[] Invalid = InvalidString.ToCharArray();
 
        if (InputUserName.text!="")
        {
            foreach (char c in characters)
            {
                foreach (char I in Invalid)
                {
                    if(c==I)
                    {
                        Debug.Log("\""+c+"\"" + " Invalid character");
                        return;
                    }
                }
            }
            if(InputPassword.text != "")
            {
                string message = currentState.EnumState.ToString() + ',' +
                    InputUserName.text + ',' + InputPassword.text + ',' +
                    networkClient.GetNetworkConnectionID();

                networkClient.SendMessageToServer(message);
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

        StartCoroutine(WaitForServerResponse(1.0f));
    }


    IEnumerator WaitForServerResponse(float time)
    {
        yield return new WaitForSeconds(time);

        if (networkClient.GetMesssage()== "Login Succeded")
        {
            ChangeState(UIStates.lobby);

        }

    }

    void Update()
    {
        currentState.UpdateState(this);

    }



}



