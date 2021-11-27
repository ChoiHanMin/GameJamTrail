using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.ootii.Messages;

public interface IMove
{
    void Move(float movePos);
}



public class CGameManager_HM : MonoBehaviour
{
    private static CGameManager_HM instance;
    public static CGameManager_HM Instance { get { return instance; } }

    private List<IMove> moveList = new List<IMove>();

    private bool isMove = false;
    private float spped = 1f;

    private float GameTimer;
    private bool bIsGameover;
    public Text TimerText;
    public string UserName;

    
    public void AddMove(IMove move)
    {
        moveList.Add(move);
    }

    public void RemoveMove(IMove move)
    {
        moveList.Remove(move);
    }

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (isMove)
        {
            for (int i = 0; i < moveList.Count; i++)
            {
                moveList[i].Move(0);
            }
        }

        if(!bIsGameover)
        {
            GameTimer += Time.deltaTime;
            TimerText.text = string.Format("{0:N2}", GameTimer);
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            MessageDispatcher.SendMessage("SetUserInfos");
        }
    }
}
