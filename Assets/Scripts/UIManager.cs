using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UI���g�p

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

    private float timer;//�o�ߎ��Ԍv���p
}
