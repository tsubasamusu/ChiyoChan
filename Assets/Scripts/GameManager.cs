using System.Collections;//IEnumerator���g�p
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//LoadScene���\�b�h���g�p

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UIManager uIManager;//UIManager

    private AudioSource aud;//AudioSource

    private bool isGameStart;//�Q�[�����n�܂������ǂ���

    private bool isGameClear;//�Q�[���N���A�������ǂ���

    /// <summary>
    /// �Q�[���J�n����擾�p
    /// </summary>
    public bool IsGameStart
    { 
        get { return isGameStart; }
    }

    /// <summary>
    /// �Q�[���I������擾�p
    /// </summary>
    public bool IsGameClear
    {
        get { return isGameClear; }
    }

    /// <summary>
    /// �Q�[���J�n����ɌĂяo�����
    /// </summary>
    /// <returns>�҂�����</returns>
    private IEnumerator Start()
    {
        //�Q�[���̏����ݒ���s��
        SetUpGame();

        //���ʉ����Đ�
        SoundManager.instance.PlaySound(SoundManager.instance.GetAudioClip(SoundManager.SoundName.GameStartSE));

        //�Q�[���X�^�[�g���o���I���܂ő҂�
        yield return StartCoroutine(uIManager.PlayGameStart());

        //BGM���Đ�
        aud= SoundManager.instance.PlaySound(SoundManager.instance.GetAudioClip(SoundManager.SoundName.MainBGM),true);

        //�e�L�X�g�̍X�V���J�n����
        StartCoroutine(uIManager.StartUpdateText());

        //�Q�[���J�n��Ԃɐ؂�ւ���
        isGameStart = true;
    }

    /// <summary>
    /// �Q�[���̏����ݒ���s��
    /// </summary>
    private void SetUpGame()
    {
        //UI�̏����ݒ���s��
        uIManager.SetUpUI();
    }

    /// <summary>
    /// �Q�[���N���A�������s��
    /// </summary>
    /// <returns>�҂�����</returns>
    public IEnumerator SetGameClear()
    {
        //�Q�[���N���A��Ԃɐ؂�ւ���
        isGameClear = true;

        //BGM���~�߂�
        aud.Stop();

        //���ʉ����Đ�
        SoundManager.instance.PlaySound(SoundManager.instance.GetAudioClip(SoundManager.SoundName.GameClearSE));

        //�Q�[���N���A���o���I���܂ő҂�
        yield return StartCoroutine(uIManager.PlayGameClear());

        //Main�V�[����ǂݍ���
        SceneManager.LoadScene("Main");
    }
}
