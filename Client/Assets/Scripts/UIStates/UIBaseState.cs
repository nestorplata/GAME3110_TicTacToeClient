using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIBaseState
{
    public UIStates EnumState;
    public UIStates EnumStateToReturn;
    public UIStates EnumStateToContinue;

    protected char[] ValidCharachters = new char[9];

    public string message;

    public abstract void StartState();

    public abstract void EnterState(UIStateManager manager);

    public abstract void OnContinue(UIStateManager manager);

    public abstract void OnReturn(UIStateManager manager);

    public abstract void OnSpecial(UIStateManager manager);

    public abstract void MessageRecieved(UIStateManager manager, string[] msg);


}
