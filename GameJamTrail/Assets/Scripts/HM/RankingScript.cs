using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using com.ootii.Messages;

public class RankingScript : MonoBehaviour
{
    public JSon Jsons;
    public Text[] UserNames;
    public Text[] ClearTimes;

    // Start is called before the first frame update
    void Start()
    {
        MessageDispatcher.AddListener("SetUserInfos", SetUserInfos);
    }

    private void OnDestroy()
    {
        MessageDispatcher.RemoveListener("SetUserInfos", SetUserInfos);
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void SetUserInfos(IMessage rMessage)
    {
        this.GetComponent<DOTweenAnimation>().DOPlay();
        if (UserNames != null)
        {
            for (int i = 0; i < UserNames.Length; i++)
            {
                if (Jsons.aaa.Count - 1 >= i)
                {
                    UserNames[i].text = Jsons.aaa[i].UserName;
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
                if (Jsons.aaa.Count - 1 >= i)
                {
                    ClearTimes[i].text = Jsons.aaa[i].ClearTime.ToString();
                }
                else
                {
                    ClearTimes[i].text = "";
                }
            }
        }
    }
}