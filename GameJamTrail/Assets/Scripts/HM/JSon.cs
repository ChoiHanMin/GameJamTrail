using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Linq;

//[System.Serializable]
//public class UserInfoClass
//{
//    public string UserName;
//    public float ClearTime;

//    public UserInfoClass(string userName, float time) {
//        this.UserName = userName;
//        this.ClearTime = time;
//    }
//}

public class JSon : MonoBehaviour
{
    public List<UserInfoClass> aaa = new List<UserInfoClass>();
    Dictionary<int, UserInfoClass> bbb = new Dictionary<int, UserInfoClass>();
    // Start is called before the first frame update
    void Start()
    {
       
        aaa.Add(new UserInfoClass("a1", 10f));
        aaa.Add(new UserInfoClass("b1", 20f));
        string TestString = aaa.ToJosnString();

        Debug.Log(TestString);
        aaa = TestString.ToJosnData<List<UserInfoClass>>();

       
        for (int i = 0; i < aaa.Count; i++)
        {
            Debug.Log(aaa[i].UserName + " " + aaa[i].ClearTime);
        }

        aaa = aaa.OrderByDescending(x => x.ClearTime).ToList();

        for (int i = 0; i < aaa.Count; i++)
        {
            Debug.Log(aaa[i].UserName + " " + aaa[i].ClearTime);
        }

        //bbb.Add(0, new JSonClass("c1", 30f));
        //bbb.Add(1, new JSonClass("d1", 40f));

        //TestString = bbb.ToJosnString();
        //Debug.Log(TestString);

        //foreach (var item in bbb)
        //{
        //    Debug.Log(item.Key + " " + item.Value.UserName + " " + item.Value.Time);
        //}

        //string jsonData = ObjectToJson(jtc);
        ////jtc.PrintInfo();
        //Debug.Log(jsonData);
        
        
        //CreateJsonFile(Application.dataPath, "JTestClass", TestString);

        var jtc2 = LoadJsonFile<List<UserInfoClass>>(Application.dataPath, "JTestClass");

        for (int i = 0; i < jtc2.Count; i++)
        {
            Debug.Log(jtc2[i].UserName + " " + jtc2[i].ClearTime);
        }

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
        return jsonData.ToJosnData<T>();
    }
}
