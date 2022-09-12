using System.Collections;
using System.Collections.Generic;//���X�g���g�p
using UnityEngine;
using System;//Serializable�������g�p
using DG.Tweening;//DOTween���g�p

public class SoundManager : MonoBehaviour
{
    /// <summary>
    /// ���̖��O
    /// </summary>
    public enum SoundName
    {
        GameStartSE,//�Q�[���J�n���ɍĐ��������ʉ�
        GameClearSE,//�Q�[���N���A���ɍĐ��������ʉ�
        RestartSE,//���X�^�[�g���鎞�ɍĐ��������ʉ�
        MainBGM,//�Q�[�����ɗ���郁�C����BGM
    }

    /// <summary>
    /// ���̃f�[�^
    /// </summary>
    [Serializable]
    public class SoundData
    {
        public SoundName name;//���O
        public AudioClip clip;//�N���b�v
    }

    [SerializeField]
    private List<SoundData> soundDatasList = new List<SoundData>();//���̃f�[�^�̃��X�g

    [SerializeField]
    private AudioSource mainAud;//���C����AudioSource

    [SerializeField]
    private AudioSource subAud;//�T�u��AudioSource

    public static SoundManager instance;//�C���X�^���X

    /// <summary>
    /// Start���\�b�h���O�ɌĂяo�����
    /// </summary>
    private void Awake()
    {
        //�ȉ��A�V���O���g���ɕK�{�̋L�q
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// �w�肵�����O�̉��̃N���b�v���擾����
    /// </summary>
    /// <param name="name">���̖��O</param>
    /// <returns>���̃N���b�v</returns>
    public AudioClip GetAudioClip(SoundName name)
    {
        //�w�肵�����O�̉��̃N���b�v���擾����
        return soundDatasList.Find(x => x.name == name).clip;
    }

    /// <summary>
    /// �����Đ�����
    /// </summary>
    /// <param name="clip">���̃N���b�v</param>
    /// <param name="loop">�J��Ԃ����ǂ���</param>
    public void PlaySound(AudioClip clip,bool loop=false)
    {
        //�J��Ԃ��Ȃ�iBGM�Ȃ�j
        if(loop)
        {
            //�J��Ԃ��悤�ɐݒ�
            mainAud.loop = true;

            //���̃N���b�v��ݒ�
            mainAud.clip= clip;

            //�����Đ�����
            mainAud.Play();
        }
        //�J��Ԃ��Ȃ��Ȃ�i���ʉ��Ȃ�
        else
        {
            //�����Đ�����
            subAud.PlayOneShot(clip);
        }
    }

    /// <summary>
    /// ���C����AusioSource�ōĐ����Ă��鉹���~�߂�
    /// </summary>
    /// <param name="fadeTime">�t�F�[�h�A�E�g����</param>
    public void StopMainSound(float fadeTime=0f)
    {
        //�����t�F�[�h�A�E�g������
        mainAud.DOFade(0f,fadeTime);
    }
}
