using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//HM_ADD
using System.IO;
using System.Text;
using System.Linq;
using com.ootii.Messages;

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


    // 1초에 1.7m 
    private float speed = 1f;
    [Range(1f, 120f)]
    [SerializeField] public float kms = 0f;
    [SerializeField] private CTrain[] trains;
    [SerializeField] private RectTransform graduation;

    private float[] kmsLevel = { 1f, 5f, 10f, 15f };
    private int level = 2;

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
    private bool finish = false;

    private float GameTimer;
    private bool bIsGameStart;

    private float moveM = 0f;

    public Text TimerText;

    public float sensitivity = 100;
    public float loudness = 0;

    [Header("----------------인게임 시간 필요 변수")]
    public Text TimeMinute;
    public int minute;

    public float GetGameTime()
    {
        return GameTimer;
    }

    public float MoveM()
    {
        return moveM;
    }

    public void AddMove(IMove move)
    {
        if (!moveList.Contains(move))
        {
            moveList.Add(move);
        }
    }

    public void RemoveMove(IMove move)
    {
        if (moveList.Contains(move))
        {
            moveList.Remove(move);
        }
    }

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Microphone.Start(null, false, 999, 44100);
        while (!(Microphone.GetPosition(null) > 0)) ;
        audioSource.Play();
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
            if (GameTimer >= 60)
            {
                GameTimer = 0;
                minute++;
                TimeMinute.text = minute.ToString();
            }
        }

        if (isMove && firstTrainMoveEnd && !finish)
        {
            float nextPos = Time.deltaTime * speed * kms;
            for (int i = 0; i < moveList.Count; i++)
            {
                moveList[i].Move(nextPos);
            }

            //Debug.Log("GGG " + nextPos);


            moveM += Time.deltaTime * speed * kms;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !firstTrainMove && !firstTrainMoveEnd)
        {
            firstTrainMove = true;
            CSoundManager.Instance.InGameBgmPlay();
        }

        if (firstTrainMove && !firstTrainMoveEnd && !isDamage)
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
        //if (Input.GetKeyDown(KeyCode.UpArrow) && firstTrainMove && firstTrainMoveEnd)
        //{
        //    if (level < kmsLevel.Length - 1)
        //    {
        //        level++;
        //    }
        //    kms = kmsLevel[level];
        //    KmToString();
        //}

        //if (Input.GetKeyDown(KeyCode.DownArrow) && firstTrainMove && firstTrainMoveEnd)
        //{
        //    if (level > 0)
        //    {
        //        level--;
        //    }
        //    kms = kmsLevel[level];
        //    KmToString();
        //}

        if (Input.GetKeyDown(KeyCode.X))// && !jump)
        {
            bool jumping = false;
            for (int i = 0; i < trains.Length; i++)
            {
                jumping = trains[i].IsJump();
            }
            if (!jumping)
            {
                trains[0].Jump();
                CSoundManager.Instance.PlaySFX(SoundSFX.Jump);
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

        if (isDamage && !finish)
        {
            damageTime += Time.deltaTime;
            if (damageTime > damageEndTime)
            {
                damageTime = 0f;
                isDamage = false;

                kms = kmsLevel[level];
                KmToString();

                for (int i = 0; i < trains.Length; i++)
                {
                    trains[i].Damage(false);
                }
            }

            for (int i = 0; i < moveList.Count; i++)
            {
                moveList[i].Move(Time.deltaTime * spped * kms);
            }
            moveM += Time.deltaTime * speed * kms;
            KmToString();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            TrainDamage();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            trains[0].DangerShout();
        }

        if (finish)
        {
            for (int i = 0; i < trains.Length; i++)
            {
                trains[i].FinishToMove(kms * Time.deltaTime);
            }
        }

        loudness = GetAveragedVolume() * sensitivity;

        if (!finish && loudness > 1.5f)
        {
            Debug.Log(" loudness " + loudness);
            trains[0].DangerShout();
        }

    }

    public void KmToString()
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

    private bool isDamage = false;
    private float damageEndTime = 3f;
    private float damageTime = 0f;

    public void TrainDamage()
    {
        for (int i = 0; i < trains.Length; i++)
        {
            trains[i].Damage(true);
            damageTime = 0f;
            isDamage = true;
            kms = 1f;
            firstTrainMoveEnd = false;
        }
    }

    [SerializeField] private Animator sickAni;
    [SerializeField] private Image sickImage;

    public void SickAniStart(Sprite character)
    {
        if (sickAni.GetCurrentAnimatorStateInfo(0).IsName("SickWait"))
        {
            sickImage.sprite = character;
            sickAni.SetTrigger("Start");
        }
    }

    public void Finish()
    {
        isMove = !isMove;
        finish = true;
        bIsGameStart = false;


        JsonManager.Instance.SortUserList();
        MessageDispatcher.SendMessage("ShowRank_End");

        CSoundManager.Instance.GameEndBgmPlay();
    }

    public float GetAveragedVolume()
    {
        float[] data = new float[256];
        float a = 0;
        audioSource.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 256;
    }

    public void setPlay()
    {
        firstTrainMove = true;
    }
}
