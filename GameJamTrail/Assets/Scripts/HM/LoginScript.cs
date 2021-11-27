using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoginScript : MonoBehaviour
{
    public InputField Nickname;
    public CGameManager_HM GM;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NicknameSave()
    {
        if("".Equals(Nickname.text))
        {
            GM.UserName = Nickname.text;
        }
        Debug.Log(Nickname.text);
    }
}
