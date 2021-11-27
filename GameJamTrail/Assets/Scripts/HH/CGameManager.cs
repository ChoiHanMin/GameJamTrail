using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//HM_ADD
using System.IO;
using System.Text;
using System.Linq;

public interface IMove
{
    void Move(float movePos);
}


public class CGameManager : MonoBehaviour
{
    

    private static CGameManager instance;
    public static CGameManager Instance { get { return instance; } }

    private List<IMove> moveList = new List<IMove>();
    [SerializeField] private bool isMove = false;
    // 1√ ø° 1.7m 
    private float speed = 1f;
    [Range(1f, 120f)]
    [SerializeField] private float kms = 0f;
    [SerializeField] private CTrain[] trains;
    [SerializeField] private RectTransform graduation;

    private float[] kmsLevel = { 1f, 5f, 10f, 15f };
    private int level = 0;

    private float spped = 1f;

    private bool jump = false;

    private float graduationLeftMax = -142f;
    private float graduationRightMax = 142f;

    private AudioSource audioSource;
    private AudioClip audioClip;

    [SerializeField] private bool firstTrainMove = false;
    [SerializeField] private bool firstTrainMoveEnd = false;
    [SerializeField] private float zPos = 0f;
    [SerializeField] private Text speedText;
    private float firstSpeed = 0f;


    private float GameTimer;
    private bool bIsGameStart;



    public Text TimerText;

    public float GetGameTime()
    {
        return GameTimer;
    }

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
    }

    // Update is called once per frame
    private void Update()
    {
        //HM_AdD
        if (bIsGameStart)
        {
            GameTimer += Time.deltaTime;
            TimerText.text = string.Format("{0:N2}", GameTimer);
        }

        if (isMove && firstTrainMoveEnd)
        {
            for (int i = 0; i < moveList.Count; i++)
            {
                moveList[i].Move(Time.deltaTime * spped * kms);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && !firstTrainMove && !firstTrainMoveEnd)
        {
            firstTrainMove = true;
            
        }

        if (firstTrainMove && !firstTrainMoveEnd)
        {
            zPos += kms * Time.deltaTime;
            if (kms < 10f)
            {
                kms += 0.07f;
            }
            else
            {
                kms = 10f;
            }

            if (zPos >= 3f)
            {
                zPos = 3f;
                isMove = true;
                firstTrainMoveEnd = true;
                bIsGameStart = true;
            }
            for (int i = 0; i < trains.Length; i++)
            {
                trains[i].FirstMove(zPos, i);
            }
            trains[2].AniStart();
            KmToString();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (level < kmsLevel.Length - 1)
            {
                level++;
                kms = kmsLevel[level];
                KmToString();
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (level > 0)
            {
                level--;
                kms = kmsLevel[level];
                KmToString();
            }
        }

        if (Input.GetKeyDown(KeyCode.F12))// && !jump)
        {
            bool jumping = false;
            for (int i = 0; i < trains.Length; i++)
            {
                jumping = trains[i].IsJump();
            }
            if (!jumping)
            {
                trains[0].Jump();
            }

            //jump = true;
            //train.Jump();
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

    private void KmToString()
    {
        float speedX;
        if (kms > 10)
        {
            speedX = 100 + (kms % 10f) * 20f;
        }
        else
        {
            speedX = kms * 10f;
        } 
        speedText.text = speedX.ToString("#0") + "Km";
        trains[2].SpeedChange(kms);
    }
    public void NextJump(int num)
    {
        if (num + 1 < trains.Length)
        {
            trains[num + 1].Jump();
        }
    }
}
