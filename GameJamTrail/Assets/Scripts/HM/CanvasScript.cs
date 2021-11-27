using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CanvasScript : MonoBehaviour
{
    public GameObject RankingUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GameOverUI()
    {
        RankingUI.GetComponent<DOTweenAnimation>().DOPlay();
    }
}
