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

    private float[] kmsLevel =    {   5f,   6f,   7f,   8f,   9f,   10f,   11f,   12f};
    private float[] kmsLevelUpM = { 100f, 200f, 300f, 400f, 600f, 800f, 1000f, 1200f};

    private float damageSpeed = 1f;

    private int level = 0;

    private float spped = 1f;

    private bool jump = false;

    private float graduationLeftMax = -142f;
    private float graduationRightMax = 142f;

    //private AudioSource audioSource;
    private AudioClip audioClip;

    [SerializeField] private bool firstTrainMove = false;
    [SerializeField] private bool firstTrainMoveEnd = false;
    [SerializeField] private float zPos = 0f;
    [SerializeField] private Text speedText;
    [SerializeField] private CTrainSpeedAnchor trainSpeedAnchor;


    [SerializeField] private Transform choChoEffectPos;
    [SerializeField] private GameObject choChoEffect;

    [SerializeField] private GameObject successEffect;


    private float firstSpeed = 0f;
    //private bool finish = false;

    private float GameTimer;
    private bool bIsGameStart;

    private float moveM = 0f;

    public Text TimerText;
    public TMPro.TMP_Text mText;

    public float sensitivity = 100;
    public float loudness = 0;

    [Header("----------------인게임 시간 필요 변수")]
    public Text TimeMinute;
    public int minute;

    [Header("----------------HP 변수")]
    private int HP;
    [SerializeField]
    private Image[] HP_Img;
    [SerializeField]
    private Sprite Broken_HP;

    public CFieldManager FieldMng;

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
    }


    // Start is called before the first frame update
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        if (FieldMng == null)
        {
            FieldMng = GameObject.FindGameObjectWithTag("FieldMng").GetComponent<CFieldManager>();
        }
        HP = 3;
    }

    // Update is called once per frame
    private void Update()
    {
        //HM_AdD
        //if (bIsGameStart)
        //{
        //    GameTimer += Time.deltaTime;
        //    TimerText.text = string.Format("{0:N2}", GameTimer);
        //    if (GameTimer >= 60)
        //    {
        //        GameTimer = 0;
        //        minute++;
        //        TimeMinute.text = minute.ToString();
        //    }
        //}

        if (moveM >= kmsLevelUpM[level] && level < kmsLevel.Length - 1)
        {
            level++;
            Debug.Log("빨라짐 이펙트");
        }


        if (isDamage)
        {
            damageTime += Time.deltaTime;
            if (damageTime > damageEndTime)
            {
                damageTime = 0f;
                isDamage = false;

                for (int i = 0; i < trains.Length; i++)
                {
                    trains[i].Damage(false);
                }
            }
        }


        if (isMove)
        {
            //float targetSpeed = isDamage ? damageSpeed : kmsLevel[level];

            if (kms < kmsLevel[level])
            {
                kms += Time.deltaTime;
            }
            else
            {
                kms = kmsLevel[level];
            }

            KmToString();

            // 일반적인 이동(맵이 이동)
            if (firstTrainMoveEnd)
            {
                float nextPos = Time.deltaTime * speed * kms;
                for (int i = 0; i < moveList.Count; i++)
                {
                    moveList[i].Move(nextPos);
                }
            }
            // 처음 이동(기차가 이동)
            else if (firstTrainMove && !firstTrainMoveEnd)
            {
                zPos += kms * Time.deltaTime;
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
            }
        }


        if (Input.GetKeyDown(KeyCode.Space) && !firstTrainMove && !firstTrainMoveEnd)
        {
            firstTrainMove = true;
            CSoundManager.Instance.InGameBgmPlay();
        }

        if (Input.GetKeyDown(KeyCode.X) && !isDamage)// && !jump)
        {

            JumpPlay();
        }
    

        if (Input.GetKeyDown(KeyCode.Z))
        {
            ChoCho();
        }


        loudness = GetAveragedVolume() * sensitivity;

    }

    public void KmToString()
    {
        float speedX;
        speedX = kms * 10f;
        speedText.text = speedX.ToString("#0") + "Km";
        trainSpeedAnchor.SetAng(speedX, 120f);
        trains[2].SpeedChange(kms);
        moveM += Time.deltaTime * speed * kms;
        mText.text = moveM.ToString("#,##0") + "M"; // string.Format("{0:N2}", GameTimer);
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
            firstTrainMoveEnd = false;
            kms = 0f;
        }

        HPMinus();
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

    public void GameOver()
    {
        bIsGameStart = false;
        CSoundManager.Instance.GameEndBgmPlay();
        MessageDispatcher.SendMessage("ShowRank_End");
    }


    public float GetAveragedVolume()
    {
        float[] data = new float[256];
        float a = 0;
        //audioSource.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 256;
    }

    public void setPlay()
    {
        firstTrainMove = true;
        isMove = true;
    }

    public void JumpPlay()
    {
        if (!trains[0].IsJump())
        {
            trains[0].Jump();
            CSoundManager.Instance.PlaySFX(SoundSFX.Jump);
        }
    }

    public void ChoCho()
    {
        trains[0].DangerShout();

        GameObject go = CObjectPool.instance.GetObject(choChoEffect);
        go.transform.position = choChoEffectPos.position;
    }

    public void SuccessEffect(Vector3 pos)
    {
        GameObject go = CObjectPool.instance.GetObject(successEffect);
        go.transform.position = pos;
    }

    public void HPMinus()
    {
        HP--;
        if (HP < 0)
        {
            GameOver();
        }
        else
        {
            HP_Img[HP].sprite = Broken_HP;
        }
    }

    public float GetTimer()
    {
        return GameTimer;
    }

}
