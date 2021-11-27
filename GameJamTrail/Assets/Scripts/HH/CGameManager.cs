using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//HM_ADD
using System.IO;
using System.Text;
using System.Linq;

[System.Serializable]
public class UserInfoClass
{
    public string UserName;
    public float ClearTime;

    public UserInfoClass(string userName, float time)
    {
        this.UserName = userName;
        this.ClearTime = time;
    }
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
    [SerializeField] private float kms = 10;
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
    private float firstSpeed = 0f;

    //HM_ADD
    [Header("HM_ADD=====================")]
    // HM_ADD
    public List<UserInfoClass> UserList;
    private float GameTimer;
    private bool bIsGameStart;
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
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();

        //HM_ADD
        var SaveData = LoadJsonFile<List<UserInfoClass>>(Application.dataPath, "JTestClass");

        if (SaveData == null)
        {
            UserList = new List<UserInfoClass>();
        }
        else
        {
            UserList = SaveData;
        }

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
            zPos += firstSpeed * Time.deltaTime;
            Debug.Log(" firstSpeed : " + firstSpeed);
            if (firstSpeed < 10f)
            {
                firstSpeed += 0.07f;
            }
            else
            {
                firstSpeed = 10f;
            }
            if (zPos >= 3f)
            {
                zPos = 3f;
                isMove = true;
                firstTrainMoveEnd = true;
            }
            for (int i = 0; i < trains.Length; i++)
            {
                trains[i].FirstMove(zPos, i);
            }

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

    public void NextJump(int num)
    {
        if (num + 1 < trains.Length)
        {
            trains[num + 1].Jump();
        }
    }



    //HM_ADD

    private void OnDestroy()
    {
        string SaveString = UserList.ToJosnString();
        CreateJsonFile(Application.dataPath, "JTestClass", SaveString);
    }

    void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    T LoadJsonFile<T>(string loadPath, string fileName)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return jsonData.ToJosnData<T>();
    }


}
