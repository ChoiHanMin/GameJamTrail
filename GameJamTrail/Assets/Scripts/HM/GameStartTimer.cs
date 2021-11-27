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

    // Start is called before the first frame update
    void Start()
    {
        Spritenum = 0;
        GameStartTimerImageChange();
    }


    private void GameStartTimerImageChange()
    {
        GameStartImg.transform.DOScale(2.2f, 0.5f).OnComplete(() =>
        {
            if (Spritenum < GameStartSprite.Length - 1)
            {
                GameStartImg.sprite = GameStartSprite[Spritenum];
                Spritenum++;
                GameStartTimerImageChange();
            }
            else
            {
                GameStartText.SetActive(true);
                this.gameObject.SetActive(false);
            }

        }).SetDelay(0.5f).SetEase(Ease.InOutQuart);
    }
}
