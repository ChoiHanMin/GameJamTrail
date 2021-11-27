using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.ootii.Messages;

public class EndScript : MonoBehaviour
{

    public GameObject Rank;
    public Text[] UserNames;
    public Text[] ClearTimes;

    // Start is called before the first frame update
    void Start()
    {
        MessageDispatcher.AddListener("ShowRank", ShowRank);
    }

    // Update is called once per frame
    void Update()
    {
        MessageDispatcher.RemoveListener("ShowRank", ShowRank);
    }

    public void SetRestart()
    {
        Application.LoadLevel("InGame");
    }

    public void GetoMain()
    {
        Application.LoadLevel("StartScene");
    }

    private void ShowRank(IMessage rMessage)
    {
        Rank.SetActive(true);

        if (UserNames != null)
        {
            for (int i = 0; i < UserNames.Length; i++)
            {
                if (JsonManager.Instance.UserList.Count - 1 >= i)
                {
                    UserNames[i].text = JsonManager.Instance.UserList[i].UserName;
                }
                else
                {
                    UserNames[i].text = "";
                }
            }
        }

        if (ClearTimes != null)
        {
            for (int i = 0; i < ClearTimes.Length; i++)
            {
                if (JsonManager.Instance.UserList.Count - 1 >= i)
                {
                    ClearTimes[i].text = JsonManager.Instance.UserList[i].ClearTime.ToString();
                }
                else
                {
                    ClearTimes[i].text = "";
                }
            }
        }
    }
}
