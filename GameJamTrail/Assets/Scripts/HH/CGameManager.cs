using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class CGameManager_HM : MonoBehaviour
{
    private static CGameManager_HM instance;
    public static CGameManager_HM Instance { get { return instance; } }

    private List<IMove> moveList = new List<IMove>();

    private bool isMove = false;
<<<<<<< HEAD
    // 1ÃÊ¿¡ 1.7m 
    private float speed = 1f;
    [Range(1f, 120f)]
    [SerializeField] private float kms = 10;

    private float[] kmsLevel = { 1f, 5f, 10f, 15f };
    private int level = 0;
        
=======
    private float spped = 1f;


    // MH_ADD
    private float GameTimer;
    public Text TimeText;
    // public Text[] ScrambleTexts;
    //Coroutine Timer;
>>>>>>> e19b129c32c51deefbccf81d4693a46aee7a75a0

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
        //Timer = StartCoroutine("TimerSet");
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
        if (GameTimer < 30)
        {
            GameTimer += Time.deltaTime;
            TimeText.text = string.Format("{0:N2}", GameTimer);
        }
    }



    //IEnumerator TimerSet()
    //{
    //    for (int i = 0; i < ScrambleTexts.Length; i++)
    //    {
    //        ScrambleTexts[i].GetComponent<DOTweenAnimation>().DOPlay();
    //    }

    //    while (Time < 30)
    //    {
    //        Time++;
    //        TimeText.text = Time.ToString();
    //        yield return new WaitForSeconds(1.0f);
    //    }
    //}
}
