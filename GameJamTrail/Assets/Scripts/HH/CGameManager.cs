using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class CGameManager : MonoBehaviour
{
    private static CGameManager instance;
    public static CGameManager Instance { get { return instance; } }

    private List<IMove> moveList = new List<IMove>();
    private bool isMove = false;
    // 1√ ø° 1.7m 
    private float speed = 1f;
    [Range(1f, 120f)]
    [SerializeField] private float kms = 10;
    [SerializeField] private CTrain train;
    [SerializeField] private RectTransform graduation;

    private float[] kmsLevel = { 1f, 5f, 10f, 15f };
    private int level = 0;

    private float spped = 1f;

    private bool jump = false;

    private float graduationLeftMax = -142f;
    private float graduationRightMax = 142f;

    private AudioSource audioSource;
    private AudioClip audioClip;

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
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        
        //Timer = StartCoroutine("TimerSet");
    }

    // Update is called once per frame
    private void Update()
    {
        if (isMove)
        {
            for (int i = 0; i < moveList.Count; i++)
            {
                moveList[i].Move(Time.deltaTime * spped * kms);
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

        if (Input.GetKeyDown(KeyCode.F12))// && !jump)
        {
            //jump = true;
            train.Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (graduation.localPosition.x - 10f < graduationLeftMax)
            {
                graduation.localPosition = new Vector3(graduationLeftMax, graduation.localPosition.y);
            }
            else
            {
                graduation.localPosition = new Vector3(graduation.localPosition.x - 10f, graduation.localPosition.y);
            }
            
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (graduation.localPosition.x + 10f > graduationRightMax)
            {
                graduation.localPosition = new Vector3(graduationRightMax, graduation.localPosition.y);
            }
            else
            {
                graduation.localPosition = new Vector3(graduation.localPosition.x + 10f, graduation.localPosition.y);
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            audioClip = Microphone.Start(null, false, 5, 44100);
            while (!(Microphone.GetPosition(null) > 0)) ;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            audioSource.clip = audioClip;
            audioSource.Play();
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
