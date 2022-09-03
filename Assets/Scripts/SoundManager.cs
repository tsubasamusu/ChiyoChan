using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;//Serializable�������g�p

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
    private List<SoundData> sounds = new List<SoundData>();//���̃f�[�^�̃��X�g
}
