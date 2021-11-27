using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.ootii.Messages;

public class SpawnSpark : MonoBehaviour
{
    public GameObject Spark;

    public List<GameObject> SparkObjs;
    public List<GameObject> SpawnPoints;
    public List<Rect> rtPoints;

    public int spawnedNum;
    public int SpawnListNum;

    private void Awake()
    {
        SparkObjs = new List<GameObject>();
        rtPoints = new List<Rect>();

        spawnedNum = 0;
        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            rtPoints.Add(SpawnPoints[i].GetComponent<RectTransform>().rect);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        MessageDispatcher.AddListener("SpawnNumMinus", SpawnNumMinus);
        StartCoroutine(SpawnSparkObjs());
    }

    private void OnDestroy()
    {
        StopCoroutine(SpawnSparkObjs());
        MessageDispatcher.RemoveListener("SpawnNumMinus", SpawnNumMinus);
    }



    private IEnumerator SpawnSparkObjs()
    {
        while (true)
        {
            if (spawnedNum < 3)
            {
                int a = Random.RandomRange(0, SpawnPoints.Count - 1);

                SparkObjs.Add(Instantiate(Spark, new Vector3(0, 0, 0), Quaternion.identity, SpawnPoints[a].transform));
                SparkObjs[SpawnListNum].transform.localPosition = new Vector3(0, 0, 0);

                SpawnListNum++;
                spawnedNum++;
            }

            yield return new WaitForSeconds(1.0f);
        }
    }

    private void SpawnNumMinus(IMessage rMessage)
    {
        spawnedNum--;
    }
}
