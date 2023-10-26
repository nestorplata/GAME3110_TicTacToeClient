using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIBaseState
{
    public UIStates EnumState;
    public UIStates EnumStateToChange;

    public string tittleText;
    public string ButtonText;
    public string TypeText;

    public string Name;
    public string Password;


    public abstract void StartState(UIStateManager manager);

    public abstract void EnterState(UIStateManager manager);

    public abstract void UpdateState(UIStateManager manager);

    public abstract void Continue(UIStateManager manager);

}
