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
    // 1�ʿ� 1.7m 
    private float speed = 0.17f;
    [Range(1f, 120f)]
    [SerializeField] private float kms = 1;

    private float[] kmsLevel = { 1f, 20f, 40f, 60f, 80f, 120f };
    private int level = 0;
        

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
                moveList[i].Move(Time.deltaTime * speed * kms);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMove = !isMove;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (level < kmsLevel.Length - 1)
            {
                level++;
                kms = kmsLevel[level];
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (level > 0)
            {
                level--;
                kms = kmsLevel[level];
            }
        }
    }
}
