using System.Collections;//IEnumerator���g�p
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//LoadScene���\�b�h���g�p

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UIManager uIManager;//UIManager

    [SerializeField]
    private BodyController bodyController;//BodyController

    [SerializeField]
    private GoalController goalController;//GoalController

    [SerializeField]
    private KeyCode restartKey;//�Q�[���ăX�^�[�g�L�[

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
        SoundManager.instance.PlaySound(SoundManager.instance.GetAudioClip(SoundManager.SoundName.MainBGM),true);

        //�e�L�X�g�̍X�V���J�n����
        StartCoroutine(uIManager.StartUpdateText());

        //�L�����N�^�[�̃R���g���[�����J�n����
        StartCoroutine(bodyController.StartControlBody());

        //�Q�[���J�n��Ԃɐ؂�ւ���
        isGameStart = true;
    }

    /// <summary>
    /// ���t���[���Ăяo�����
    /// </summary>
    private void Update()
    {
        //�Q�[���ăX�^�[�g�L�[�������ꂽ��
        if (Input.GetKeyDown(restartKey))
        {
            //�L�����N�^�[�̏�Ԃ�����������
            bodyController.ResetCharacterCondition();

            //���ʉ����Đ�
            SoundManager.instance.PlaySound(SoundManager.instance.GetAudioClip(SoundManager.SoundName.RestartSE));
        }
    }

    /// <summary>
    /// �Q�[���̏����ݒ���s��
    /// </summary>
    private void SetUpGame()
    {
        //UI�̏����ݒ���s��
        uIManager.SetUpUI();

        //GoalController�̏����ݒ���s��
        goalController.SetUpGoalController(this);

        //BodyController�̏����ݒ���s��
        bodyController.SetUpBodyController();
    }

    /// <summary>
    /// �Q�[���N���A�̏������s��
    /// </summary>
    public void PrepareGameClear()
    {
        //�Q�[���N���A�������s��
        StartCoroutine(SetGameClear());
    }

    /// <summary>
    /// �Q�[���N���A�������s��
    /// </summary>
    /// <returns>�҂�����</returns>
    private IEnumerator SetGameClear()
    {
        //�Q�[���N���A��Ԃɐ؂�ւ���
        isGameClear = true;

        //�e�L�X�g�̍X�V���~�߂�
        uIManager.StopUpdateText = true;

        //BGM���~�߂�
        SoundManager.instance.StopMainSound(0.5f);

        //���ʉ����Đ�
        SoundManager.instance.PlaySound(SoundManager.instance.GetAudioClip(SoundManager.SoundName.GameClearSE));

        //�Q�[���N���A���o���I���܂ő҂�
        yield return StartCoroutine(uIManager.PlayGameClear());

        //Main�V�[����ǂݍ���
        SceneManager.LoadScene("Main");
    }
}
