using System.Collections;//IEnumerator���g�p
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UI���g�p
using DG.Tweening;//DOTween���g�p

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Image imgBackground;//�w�i

    [SerializeField]
    private Image imgLogo;//���S

    [SerializeField]
    private Text txtTime;//�o�ߎ���

    [SerializeField]
    private Text txtLength;//�S�[���܂ł̋���

    [SerializeField]
    private Sprite sprGameStart;//�Q�[���X�^�[�g���S

    [SerializeField]
    private Sprite sprGameClear;//�Q�[���N���A���S

    [SerializeField]
    private Transform playerTran;//�v���[���[�̈ʒu���

    [SerializeField]
    private Transform goalTran;//�S�[���̈ʒu���

    [SerializeField]
    private GameManager gameManager;//GameManager

    private float timer;//�o�ߎ��Ԍv���p

    /// <summary>
    /// UI�̏����ݒ���s��
    /// </summary>
    public void SetUpUI()
    {
        //�w�i�𔒐F�ɐݒ�
        imgBackground.color = Color.white;

        //�e�L�X�g����ɂ���
        txtLength.text=txtTime.text=string.Empty;
    }

    /// <summary>
    /// �Q�[���X�^�[�g���o���s��
    /// </summary>
    /// <returns>�҂�����</returns>
    public IEnumerator PlayGameStart()
    {
        //���o���I��������ǂ���
        bool end = false;

        //�w�i��\��
        imgBackground.DOFade(1f, 0f);

        //���S���uGameStart�v�ɐݒ�
        imgLogo.sprite = sprGameStart;

        //���S���\���ɂ���
        imgLogo.DOFade(0f, 0f);

        //Sequence��o�^
        Sequence sequence = DOTween.Sequence();

        //���S�̃A�j���[�V�������s��
        sequence.Append(imgLogo.DOFade(1f, 1f).SetLoops(2, LoopType.Yoyo));

        //�w�i���\���ɂ���
        sequence.Append(imgBackground.DOFade(0f, 1f)).OnComplete(()=>end=true);

        //���o���I���܂ő҂�
        yield return new WaitUntil(()=>end);
    }

    /// <summary>
    /// �Q�[���N���A���o���s��
    /// </summary>
    /// <returns>�҂�����</returns>
    public IEnumerator PlayGameClear()
    {
        //�u�S�[���܂ł̋����v�̃e�L�X�g��񊈐�������
        txtLength.enabled = false;

        //���S���uGameClear�v�ɐݒ�
        imgLogo.sprite = sprGameClear;

        //�w�i��\��
        imgBackground.DOFade(1f, 1f);

        //���S��\��
        imgLogo.DOFade(1f, 1f);

        //�o�ߎ��Ԃ�K�؂Ȉʒu�Ɉړ�������
        txtTime.transform.DOMoveX(900f, 1f);

        //�e�L�X�g�̐F��ω�������
        txtTime.DOColor(Color.blue, 1f);

        //2�b�҂�
        yield return new WaitForSeconds(2f);

        //���S���\���ɂ���
        imgLogo.DOFade(0f, 1f);

        //�o�ߎ��Ԃ��\���ɂ���
        txtTime.DOFade(0f, 1f);

        //2�b�҂�
        yield return new WaitForSeconds(2f);
    }

    /// <summary>
    /// �u�o�ߎ��ԁv�u�S�[���܂ł̋����v�̃e�L�X�g�̍X�V���J�n����
    /// </summary>
    /// <returns>�҂�����</returns>
    public IEnumerator StartUpdateText()
    {
        //�����ɌJ��Ԃ�
        while(true)
        {
            //�Q�[�����I��������
            if(gameManager.IsGameClear)
            {
                //�J��Ԃ��������I������
                break;
            }

            //�S�[���܂ł̋������v�Z���ĕ\��
            txtLength.text = (goalTran.position.z - playerTran.position.z).ToString("F2")+"m\nTo Goal";

            //�o�ߎ��Ԃ��v��
            timer+=Time.deltaTime;

            //�o�ߎ��Ԃ�\��
            txtTime.text=timer.ToString("F2")+ "\nSecond";

            //���̃t���[���֔�΂��i�����AUpdate���\�b�h�j
            yield return null;
        }
    }

    /// <summary>
    /// �o�ߎ��Ԃ�����������
    /// </summary>
    public void ResetTimer()
    {
        //�o�ߎ��Ԃ�0�b�ɂ���
        timer = 0f;
    }
}
