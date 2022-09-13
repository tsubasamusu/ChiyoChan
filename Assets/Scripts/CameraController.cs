using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tsubasa//���O��Ԑ錾
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Transform lookTran;//�Ώە�

        [SerializeField]
        private float smooth;//���_�ړ��̊��炩��

        private Vector3 firstPos;//�J�����̏����ʒu

        private bool set;//�����ݒ���s�������ǂ���

        /// <summary>
        /// CameraController�̏����ݒ���s��
        /// </summary>
        public void SetUpCameraController()
        {
            //�J�����̏����ʒu���擾
            firstPos = transform.position;

            //�����ݒ芮����Ԃɐ؂�ւ���
            set = true;
        }

        /// <summary>
        /// ��莞�Ԃ��ƂɌĂяo�����
        /// </summary>
        private void FixedUpdate()
        {
            //�����ݒ肪�������Ă��Ȃ��Ȃ�
            if(!set)
            {
                //�ȍ~�̏������s��Ȃ�
                return;
            }

            //�X�V���ׂ��ʒu���擾
            Vector3 pos = new Vector3(firstPos.x, firstPos.y, lookTran.position.z);

            //�J���������炩�Ɉړ�������
            transform.position = Vector3.Lerp(transform.position, pos, Time.fixedDeltaTime * smooth);
        }
    }
}
