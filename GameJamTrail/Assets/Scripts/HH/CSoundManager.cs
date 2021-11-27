using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SoundSFX
{
    Hit1 = 0, // ������ �¾�����
    Hit2 = 1, // ������ �¾����� 2
    Miss = 2, // ������ ��������
    Glass_Crush = 3, // ����â�� ������
    Huddle_Crush = 4, // ��� �ε������� ������
    Jump = 5,   // ����
    Select = 6, // �г��� �Է��� �Ѿ��
    Cancel = 7, // ����Ҷ�
    GetElectric = 8, // ���⸦ �Ծ�����
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
