using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SoundSFX
{
    Hit1 = 0, // 몽물이 맞았을때
    Hit2 = 1, // 동물이 맞았을때 2
    Miss = 2, // 동물이 피했을때
    Glass_Crush = 3, // 유리창이 깨질대
    Huddle_Crush = 4, // 허들 부딧쳤을대 웅덩이
    Jump = 5,   // 점프
    Select = 6, // 닉네임 입력후 넘어갈때
    Cancel = 7, // 취소할때
    GetElectric = 8, // 전기를 먹었을때
}


public class CSoundManager : MonoBehaviour
{
    private static CSoundManager instance;
    public static CSoundManager Instance { get { return instance; } }

    [SerializeField] private AudioClip gameendBgx;
    [SerializeField] private AudioClip inGameBgm;

    [SerializeField] private AudioClip[] sfx;

    private AudioSource audioSource;


    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void InGameBgmPlay()
    {
        audioSource.clip = inGameBgm;
        audioSource.Play();
    }

    public void GameEndBgmPlay()
    {
        audioSource.clip = gameendBgx;
        audioSource.Play();
    }


    public void PlaySFX(SoundSFX soundSFX)
    {
        audioSource.PlayOneShot(sfx[(int)soundSFX]);
    }


}
