using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    private GameManager gameManager;//GameManager

    /// <summary>
    /// GoalController�̏����ݒ���s��
    /// </summary>
    /// <param name="gameManager">GameManager</param>
    public void SetUpGoalController(GameManager gameManager)
    {
        //GameManager���擾
        this.gameManager = gameManager;
    }

    /// <summary>
    /// ���̃R���C�_�[�����蔲�����ۂɌĂяo�����
    /// </summary>
    /// <param name="other">�ڐG����</param>
    private void OnTriggerEnter(Collider other)
    {
        //�ڐG���肩��BodyController���擾�o������i�ڐG���肪�L�����N�^�[�Ȃ�j
        if(other.TryGetComponent(out BodyController bodyController))
        {
            //�Q�[���N���A�̏������s��
            gameManager.PrepareGameClear();

            //BodyController��񊈐�������i�d�������h�~�j
            bodyController.enabled = false;
        }
    }
}
