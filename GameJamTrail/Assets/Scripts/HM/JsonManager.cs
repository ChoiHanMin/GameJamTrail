using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using com.ootii.Messages;

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


public class JsonManager : MonoBehaviour
{
    private static JsonManager instance;
    public static JsonManager Instance { get { return instance; } }

    // HM_ADD
    public List<UserInfoClass> UserList;
    public string UserName;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);

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
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            MessageDispatcher.SendMessage("ShowRank");
        }
    }

    private void OnDestroy()
    {
        string SaveString = UserList.ToJosnString();
        CreateJsonFile(Application.dataPath, "JTestClass", SaveString);
    }

    void SortUserList()
    {
        UserList.Add(new UserInfoClass(UserName, CGameManager.Instance.GetGameTime()));
        UserList = UserList.OrderBy(x => x.ClearTime).ToList();
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
