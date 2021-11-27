using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMove
{
    void Move(float movePos);
}



public class CGameManager : MonoBehaviour
{
    private static CGameManager instance;
    public static CGameManager Instance { get { return instance; } }

    private List<IMove> moveList = new List<IMove>();

    private bool isMove = false;
    private float spped = 1f;
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
    }
}
