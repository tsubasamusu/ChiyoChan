using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform targetTran;//�Ώە�

    [SerializeField]
    private float smooth;//���_�ړ��̊��炩��

    private Vector3 firstPos;//�J�����̏����ʒu

    /// <summary>
    /// �Q�[���J�n����ɌĂяo�����
    /// </summary>
    private void Start()
    {
        //�J�����̏����ʒu���擾
        firstPos = transform.position;
    }

    /// <summary>
    /// ��莞�Ԃ��ƂɌĂяo�����
    /// </summary>
    private void FixedUpdate()
    {
        //�X�V���ׂ��ʒu���擾
        Vector3 pos = new Vector3(firstPos.x,firstPos.y,targetTran.position.z);

        //�J���������炩�Ɉړ�������
        transform.position = Vector3.Lerp(transform.position, pos, Time.fixedDeltaTime * smooth);
    }
}
