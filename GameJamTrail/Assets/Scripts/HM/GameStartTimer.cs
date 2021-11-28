using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameStartTimer : MonoBehaviour
{
    public Sprite[] GameStartSprite;
    public Image GameStartImg;
    public GameObject GameStartText;
    public int Spritenum;
    private DOTweenAnimation MyAnimation;

    // Start is called before the first frame update
    void Start()
    {
        Spritenum = 0;
        MyAnimation = this.GetComponent<DOTweenAnimation>();
    }


    public void GameStartTimerImageChange()
    {

        if (Spritenum < GameStartSprite.Length)
        {
            Debug.Log("Image Change"); 
          
            GameStartImg.sprite = GameStartSprite[Spritenum];
            Spritenum++;
            MyAnimation.DORewind();
            MyAnimation.DOPlayForward();
        }
        else
        {
            this.gameObject.SetActive(false);
            GameStartText.SetActive(true);
        }

    }
}
