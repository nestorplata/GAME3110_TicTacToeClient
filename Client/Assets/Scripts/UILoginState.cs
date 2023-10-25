
using System.IO;
using UnityEngine;

public class UILoginState : UIBaseState
{
    public override void EnterState(UIStateManager manager)
    {
        EnumState = UIStates.login;
        tittleText = "LOGIN";
        ButtonText = "TO MAKE ACCOUNT";

    }
    public override void UpdateState(UIStateManager manager)
    {

    }

    public override void Continue()
    {
        
        Debug.Log(Name + ',' + Password);
        string pathfile = "Accounts\\" + Name + ".txt";

        if (File.Exists(pathfile))
        {
            using (StreamReader sr = new StreamReader("Accounts\\" + Name + ".txt"))
            {
                if (Password == sr.ReadLine())
                {
                    Debug.Log("Login Succeded");
                }
                else
                {
                    Debug.Log("Wrong Password");
                }
            }
        }
        else
        {
            Debug.Log("Wrong Username");

        }


    }


}
