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

    [Header("--------------안드로이드")]
    public Text GetMeter;
    public Text Times;

    // Start is called before the first frame update
    void Start()
    {
        MessageDispatcher.AddListener("ShowRank_End", ShowRank);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnDestroy()
    {
        MessageDispatcher.RemoveListener("ShowRank_End", ShowRank);
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
        Debug.Log("RankShow");
        Rank.SetActive(true);

        //if (UserNames != null)
        //{
        //    for (int i = 0; i < UserNames.Length; i++)
        //    {
        //        if (JsonManager.Instance.UserList.Count - 1 >= i)
        //        {
        //            UserNames[i].text = JsonManager.Instance.UserList[i].UserName;
        //        }
        //        else
        //        {
        //            UserNames[i].text = "";
        //        }
        //    }
        //}

        //if (ClearTimes != null)
        //{
        //    for (int i = 0; i < ClearTimes.Length; i++)
        //    {
        //        if (JsonManager.Instance.UserList.Count - 1 >= i)
        //        {
        //            ClearTimes[i].text = JsonManager.Instance.UserList[i].ClearTime.ToString();
        //        }
        //        else
        //        {
        //            ClearTimes[i].text = "";
        //        }
        //    }
        //}

        GetMeter.text = CGameManager.Instance.FieldMng.GetMeter().ToString() + "M";
        Times.text = string.Format("{0} : {1:N2}", CGameManager.Instance.minute , CGameManager.Instance.GetTimer());
    }

    public void JumpButton()
    {
        CGameManager.Instance.JumpPlay();
    }
    public void ChochoButton()
    {
        CGameManager.Instance.ChoCho();
    }
}
