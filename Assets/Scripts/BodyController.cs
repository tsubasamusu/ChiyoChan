using System.Collections;//IEnumerator���g�p
using System.Collections.Generic;//���X�g���g�p
using UnityEngine;
using System;//Serializable�������g�p

public class BodyController : MonoBehaviour
{
    /// <summary>
    /// �̂̕��ʂ̖��O
    /// </summary>
    public enum PartsName
    {
        �E�r,
        ���r,
        �E��,
        �E�G,
        ����,
        ���G,
    }

    /// <summary>
    /// �̂̕��ʂ̃f�[�^
    /// </summary>
    [Serializable]
    public class PartsData
    {
        public PartsName partsName;//�̂̕��ʂ̖��O
        public Transform partsTran;//�̂̕��ʂ̈ʒu���
    }

    [SerializeField]
    private List<PartsData> partsDataList = new List<PartsData>();//�̂̕��ʂ̃f�[�^�̃��X�g

    [SerializeField,Header("��莞�ԁi��0.02�b�j���Ƃɉ�]�����鑫�̊p�x"),Range(0f,10f)]
    private float rotAngle;//��莞�ԁi��0.02�b�j���Ƃɉ�]�����鑫�̊p�x

    [SerializeField,Range(0f,80f)]
    private float maxLegAngle;//���̊֐߂̍ő�p�x

    [SerializeField,Range(-80f,0f)]
    private float minLegAngle;//���̊֐߂̍ŏ��p�x

    [SerializeField, Range(0f, 180f)]
    private float maxArmAngle;//�r�̊֐߂̍ő�p�x

    [SerializeField,Range(0f,10f)]
    private float gravity;//�d��

    [SerializeField]
    private Rigidbody rb;//Rigidbody

    [SerializeField]
    private UIManager uIManager;//UIManager

    private List<Vector3> firstPartsLocalEulerAnglesList = new List<Vector3>();//�e���ʂ̏����̊p�x

    private int rightLegCount;//�E����O�ɓ������������Ăяo���ꂽ��

    private int leftLegCount;//������O�ɓ������������Ăяo���ꂽ��

    /// <summary>
    /// �L�����N�^�[�̃R���g���[�����J�n����
    /// </summary>
    /// <returns>�҂�����</returns>
    public IEnumerator StartControlBody()
    {
        //�����ɌJ��Ԃ�
        while (true)
        {
            //�uQ�v��������Ă����
            if (Input.GetKey(KeyCode.Q))
            {
                //���������s
                PlayQ();
            }
            //�uW�v��������Ă����
            else if (Input.GetKey(KeyCode.W))
            {
                //���������s
                PlayW();
            }

            //�uO�v��������Ă����
            if (Input.GetKey(KeyCode.O))
            {
                //���������s
                PlayO();
            }
            //�uP�v��������Ă����
            else if (Input.GetKey(KeyCode.P))
            {
                //���������s
                PlayP();
            }

            //��莞�ԑ҂i�����AFixedUpdate���\�b�h�j
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
    }

    /// <summary>
    /// BodyController�̏����ݒ���s��
    /// </summary>
    public void SetUpBodyController()
    {
        //�̂̕��ʂ̃f�[�^�̃��X�g�̗v�f�������J��Ԃ�
        for (int i = 0; i < partsDataList.Count; i++)
        {
            //�J��Ԃ������œ����v�f�̊p�x�����X�g�ɓo�^
            firstPartsLocalEulerAnglesList.Add(partsDataList[i].partsTran.localEulerAngles);
        }

        //�d�͂�ݒ�
        Physics.gravity = new Vector3(0f, -gravity, 0f);
    }

    /// <summary>
    /// �w�肵�����ʂ̈ʒu�����擾����
    /// </summary>
    /// <param name="partsName">���ʂ̖��O</param>
    /// <returns>���ʂ̈ʒu���</returns>
    private Transform GetPartsTran(PartsName partsName)
    {
        //�󂯎�������O�Ɠ������O�̕��ʂ̈ʒu����Ԃ�
        return partsDataList.Find(x => x.partsName == partsName).partsTran;
    }

    /// <summary>
    /// �uQ�v��������Ă���Ԃ̏����i���������Ɂj
    /// </summary>
    private void PlayQ()
    {
        //�p�x������t����
        if (GetLeftLegAngle()>=minLegAngle)
        {
            //���������񐔂��L�^����
            leftLegCount--;

            //�E�r�̊p�x���X�V����
            UpdateRightArmAngle();

            //���G�̊p�x���X�V����
            UpdateLeftKneeAngle();

            //������
            GetPartsTran(PartsName.����).Rotate(rotAngle, 0f, 0f);
        }
    }

    /// <summary>
    /// �uW�v��������Ă���Ԃ̏����i������O�Ɂj
    /// </summary>
    private void PlayW()
    {
        //�p�x������t����
        if (GetLeftLegAngle()<= maxLegAngle)
        {
            //���������񐔂��L�^����
            leftLegCount++;

            //�E�r�̊p�x���X�V����
            UpdateRightArmAngle();

            //���G�̊p�x���X�V����
            UpdateLeftKneeAngle();

            //������
            GetPartsTran(PartsName.����).Rotate(-rotAngle, 0f, 0f);
        }
    }

    /// <summary>
    /// �uO�v��������Ă���Ԃ̏����i�E�������Ɂj
    /// </summary>
    private void PlayO()
    {
        //�p�x������t����
        if (GetRightLegAngle()>=minLegAngle)
        {
            //���������񐔂��L�^����
            rightLegCount--;

            //���r�̊p�x���X�V����
            UpdateLeftArmAngle();

            //�E�G�̊p�x���X�V����
            UpdateRightKneeAngle();

            //������
            GetPartsTran(PartsName.�E��).Rotate(rotAngle, 0f, 0f);
        }
    }

    /// <summary>
    /// �uP�v��������Ă���Ԃ̏����i�E����O�Ɂj
    /// </summary>
    private void PlayP()
    {
        //�p�x������t����
        if (GetRightLegAngle() <= maxLegAngle)
        {
            //���������񐔂��L�^����
            rightLegCount++;

            //���r�̊p�x���X�V����
            UpdateLeftArmAngle();

            //�E�G�̊p�x���X�V����
            UpdateRightKneeAngle();

            //������
            GetPartsTran(PartsName.�E��).Rotate(-rotAngle, 0f, 0f);
        }
    }

    /// <summary>
    /// �E���̊p�x���擾����
    /// </summary>
    /// <returns>�E���̊p�x</returns>
    private float GetRightLegAngle()
    {
        //�E���̊p�x��Ԃ�
        return rightLegCount * rotAngle;
    }

    /// <summary>
    /// �����̊p�x���擾����
    /// </summary>
    /// <returns>�����̊p�x</returns>
    private float GetLeftLegAngle()
    {
        //�����̊p�x��Ԃ�
        return leftLegCount * rotAngle;
    }

    /// <summary>
    /// �E�r�̊p�x���X�V����
    /// </summary>
    private void UpdateRightArmAngle()
    {
        //�n�_�i���̍ŏ��p�x�j����ɂ������݂̑��̊p�x���擾�i�K�����ɂȂ�j
        float currentLegAngleValue = GetLeftLegAngle() < 0f ? Math.Abs(minLegAngle - GetLeftLegAngle()) : GetLeftLegAngle() + Math.Abs(minLegAngle);

        //���̉���i���v�p�x�j���擾
        float totalLegAngle = maxLegAngle + Math.Abs(minLegAngle);

        //�������擾
        float currentRatio = currentLegAngleValue / totalLegAngle;

        //�r�̉���i���v�p�x�j���擾
        float totalArmAngle = maxArmAngle * 2;

        //�r�̎n�_�̊p�x���擾
        float firstArmAngle = 180f - maxArmAngle;

        //�X�V���ׂ��p�x���擾
        float angleZ = firstArmAngle + (totalArmAngle * currentRatio);

        //�p�x��ݒ�
        GetPartsTran(PartsName.�E�r).localEulerAngles = 
            new Vector3(GetPartsTran(PartsName.�E�r).localEulerAngles.x, GetPartsTran(PartsName.�E�r).localEulerAngles.y,angleZ);
    }

    /// <summary>
    /// ���r�̊p�x���X�V����
    /// </summary>
    private void UpdateLeftArmAngle()
    {
        //�n�_�i���̍ŏ��p�x�j����ɂ������݂̑��̊p�x���擾�i�K�����ɂȂ�j
        float currentLegAngleValue = GetRightLegAngle() < 0f ? Math.Abs(minLegAngle - GetRightLegAngle()) : GetRightLegAngle() + Math.Abs(minLegAngle);

        //���̉���i���v�p�x�j���擾
        float totalLegAngle = maxLegAngle + Math.Abs(minLegAngle);

        //�������擾
        float currentRatio = currentLegAngleValue / totalLegAngle;

        //�r�̉���i���v�p�x�j���擾
        float totalArmAngle = maxArmAngle * 2;

        //�r�̎n�_�̊p�x���擾
        float firstArmAngle = 180f - maxArmAngle;

        //�X�V���ׂ��p�x���擾
        float angleZ = firstArmAngle + (totalArmAngle * currentRatio);

        //�p�x��ݒ�
        GetPartsTran(PartsName.���r).localEulerAngles =
            new Vector3(GetPartsTran(PartsName.���r).localEulerAngles.x, GetPartsTran(PartsName.���r).localEulerAngles.y, angleZ);
    }

    /// <summary>
    /// �E�G�̊p�x���X�V����
    /// </summary>
    private void UpdateRightKneeAngle()
    {
        //�n�_�i���̍ŏ��p�x�j����ɂ������݂̑��̊p�x���擾�i�K�����ɂȂ�j
        float currentLegAngleValue=GetRightLegAngle()<0f? Math.Abs( minLegAngle - GetRightLegAngle()):GetRightLegAngle()+Math.Abs(minLegAngle);
        
        //�������擾
        float currentRatio = GetRightLegAngle()<0f  ? 
            (Math.Abs(minLegAngle) - currentLegAngleValue) / Math.Abs(minLegAngle) : (currentLegAngleValue- Math.Abs(minLegAngle) )/ maxLegAngle;
        
        //�X�V���ׂ��p�x���擾
        float angleX = 89f * currentRatio;

        //�p�x��ݒ�
        GetPartsTran(PartsName.�E�G).localEulerAngles = 
            new Vector3(angleX, GetPartsTran(PartsName.�E�G).localEulerAngles.y, GetPartsTran(PartsName.�E�G).localEulerAngles.z);
    }

    /// <summary>
    /// ���G�̊p�x���X�V����
    /// </summary>
    private void UpdateLeftKneeAngle()
    {
        //�n�_�i���̍ŏ��p�x�j����ɂ������݂̑��̊p�x���擾�i�K�����ɂȂ�j
        float currentLegAngleValue = GetLeftLegAngle() < 0f ? Math.Abs(minLegAngle - GetLeftLegAngle()) : GetLeftLegAngle() + Math.Abs(minLegAngle);

        //�������擾
        float currentRatio = GetLeftLegAngle() < 0f ?
            (Math.Abs(minLegAngle) - currentLegAngleValue) / Math.Abs(minLegAngle) : (currentLegAngleValue - Math.Abs(minLegAngle)) / maxLegAngle;

        //�X�V���ׂ��p�x���擾
        float angleX = 89f * currentRatio;

        //�p�x��ݒ�
        GetPartsTran(PartsName.���G).localEulerAngles =
            new Vector3(angleX, GetPartsTran(PartsName.���G).localEulerAngles.y, GetPartsTran(PartsName.���G).localEulerAngles.z);
    }

    /// <summary>
    /// �L�����N�^�[�̏�Ԃ�����������
    /// </summary>
    public void ResetCharacterCondition()
    {
        //��U�A�������Z���N���A�ɂ���
        rb.isKinematic = true;

        //�e���ʂ̃f�[�^�̃��X�g�̗v�f�������J��Ԃ�
        for (int i = 0; i < partsDataList.Count; i++)
        {
            //�̂̊e���ʂ̊p�x��������Ԃɐݒ�
            partsDataList[i].partsTran.localEulerAngles = firstPartsLocalEulerAnglesList[i];
        }

        //�L�����N�^�[�̈ʒu�Ɗp�x��������Ԃɖ߂�
        transform.position = transform.eulerAngles = Vector3.zero;

        //�J�E���g��������
        rightLegCount= leftLegCount = 0;

        //�o�ߎ��Ԃ�����������
        uIManager.ResetTimer();

        //�������Z���ĊJ
        rb.isKinematic = false;
    }
}
