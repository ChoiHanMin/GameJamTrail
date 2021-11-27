using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

[System.Serializable]

public class JSonClass
{
   public  struct UserInfo
    {
        public string UserName;
        public float Time;
    }

    public string UserName;
    public float Time;

    public List<UserInfo> UserList = new List<UserInfo>();

    UserInfo info;

    public JSonClass() { }
    public JSonClass(bool bIsSet)
    {
        if(bIsSet)
        {
            UserName = "asdfasdf";
            Time = 3.0f;
            //info.UserName = "aaaa";
            //info.Time = 30.0f;
            //UserList.Add(info);
        }
    }

    public void PrintInfo()
    {
        //for(int i=0;i<UserList.Count;i++)
        //{
        //    Debug.Log(UserList[i].UserName);
        //    Debug.Log(UserList[i].Time);
        //}

        Debug.Log(UserName);
        Debug.Log(Time);
        
    }
}

public class JSon : MonoBehaviour
{    
    // Start is called before the first frame update
    void Start()
    {
        JSonClass jtc = new JSonClass(true);
        //string jsonData = ObjectToJson(jtc);
        ////jtc.PrintInfo();
        //Debug.Log(jsonData);
        //CreateJsonFile(Application.dataPath, "JTestClass", jsonData);
        var jtc2 = LoadJsonFile<JSonClass>(Application.dataPath, "JTestClass");
        jtc2.PrintInfo();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string ObjectToJson(object obj)
    {
        return JsonUtility.ToJson(obj);
    }

    public T JsonToOject<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
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
        return JsonUtility.FromJson<T>(jsonData);
    }
}
