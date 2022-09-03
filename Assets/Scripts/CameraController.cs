using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform targetTran;//�Ώە�

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
    /// ���t���[���Ăяo�����
    /// </summary>
    private void Update()
    {
        //�J�����̈ʒu���X�V
        transform.position = new Vector3(targetTran.position.x,firstPos.y,firstPos.z);
    }
}
