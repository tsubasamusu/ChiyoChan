using System.Collections;//IEnumeratorを使用
using System.Collections.Generic;//リストを使用
using UnityEngine;
using System;//Serializable属性を使用

public class BodyController : MonoBehaviour
{
    /// <summary>
    /// 体の部位の名前
    /// </summary>
    public enum PartsName
    {
        右腕,
        左腕,
        右足,
        右膝,
        左足,
        左膝,
    }

    /// <summary>
    /// 体の部位のデータ
    /// </summary>
    [Serializable]
    public class PartsData
    {
        public PartsName partsName;//体の部位の名前
        public Transform partsTran;//体の部位の位置情報
    }

    [SerializeField]
    private List<PartsData> partsDataList = new List<PartsData>();//体の部位のデータのリスト

    [SerializeField,Header("一定時間（約0.02秒）ごとに回転させる足の角度"),Range(0f,10f)]
    private float rotAngle;//一定時間（約0.02秒）ごとに回転させる足の角度

    [SerializeField,Range(0f,80f)]
    private float maxLegAngle;//足の関節の最大角度

    [SerializeField,Range(-80f,0f)]
    private float minLegAngle;//足の関節の最小角度

    [SerializeField, Range(0f, 180f)]
    private float maxArmAngle;//腕の関節の最大角度

    [SerializeField,Range(0f,10f)]
    private float gravity;//重力

    [SerializeField]
    private Rigidbody rb;//Rigidbody

    [SerializeField]
    private UIManager uIManager;//UIManager

    private List<Vector3> firstPartsLocalEulerAnglesList = new List<Vector3>();//各部位の初期の角度

    private int rightLegCount;//右足を前に動かす処理が呼び出された回数

    private int leftLegCount;//左足を前に動かす処理が呼び出された回数

    /// <summary>
    /// キャラクターのコントロールを開始する
    /// </summary>
    /// <returns>待ち時間</returns>
    public IEnumerator StartControlBody()
    {
        //無限に繰り返す
        while (true)
        {
            //「Q」を押されている間
            if (Input.GetKey(KeyCode.Q))
            {
                //処理を実行
                PlayQ();
            }
            //「W」を押されている間
            else if (Input.GetKey(KeyCode.W))
            {
                //処理を実行
                PlayW();
            }

            //「O」を押されている間
            if (Input.GetKey(KeyCode.O))
            {
                //処理を実行
                PlayO();
            }
            //「P」を押されている間
            else if (Input.GetKey(KeyCode.P))
            {
                //処理を実行
                PlayP();
            }

            //一定時間待つ（実質、FixedUpdateメソッド）
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
    }

    /// <summary>
    /// BodyControllerの初期設定を行う
    /// </summary>
    public void SetUpBodyController()
    {
        //体の部位のデータのリストの要素数だけ繰り返す
        for (int i = 0; i < partsDataList.Count; i++)
        {
            //繰り返し処理で得た要素の角度をリストに登録
            firstPartsLocalEulerAnglesList.Add(partsDataList[i].partsTran.localEulerAngles);
        }

        //重力を設定
        Physics.gravity = new Vector3(0f, -gravity, 0f);
    }

    /// <summary>
    /// 指定した部位の位置情報を取得する
    /// </summary>
    /// <param name="partsName">部位の名前</param>
    /// <returns>部位の位置情報</returns>
    private Transform GetPartsTran(PartsName partsName)
    {
        //受け取った名前と同じ名前の部位の位置情報を返す
        return partsDataList.Find(x => x.partsName == partsName).partsTran;
    }

    /// <summary>
    /// 「Q」が押されている間の処理（左足を後ろに）
    /// </summary>
    private void PlayQ()
    {
        //角度制限を付ける
        if (GetLeftLegAngle()>=minLegAngle)
        {
            //動かした回数を記録する
            leftLegCount--;

            //右腕の角度を更新する
            UpdateRightArmAngle();

            //左膝の角度を更新する
            UpdateLeftKneeAngle();

            //動かす
            GetPartsTran(PartsName.左足).Rotate(rotAngle, 0f, 0f);
        }
    }

    /// <summary>
    /// 「W」が押されている間の処理（左足を前に）
    /// </summary>
    private void PlayW()
    {
        //角度制限を付ける
        if (GetLeftLegAngle()<= maxLegAngle)
        {
            //動かした回数を記録する
            leftLegCount++;

            //右腕の角度を更新する
            UpdateRightArmAngle();

            //左膝の角度を更新する
            UpdateLeftKneeAngle();

            //動かす
            GetPartsTran(PartsName.左足).Rotate(-rotAngle, 0f, 0f);
        }
    }

    /// <summary>
    /// 「O」が押されている間の処理（右足を後ろに）
    /// </summary>
    private void PlayO()
    {
        //角度制限を付ける
        if (GetRightLegAngle()>=minLegAngle)
        {
            //動かした回数を記録する
            rightLegCount--;

            //左腕の角度を更新する
            UpdateLeftArmAngle();

            //右膝の角度を更新する
            UpdateRightKneeAngle();

            //動かす
            GetPartsTran(PartsName.右足).Rotate(rotAngle, 0f, 0f);
        }
    }

    /// <summary>
    /// 「P」が押されている間の処理（右足を前に）
    /// </summary>
    private void PlayP()
    {
        //角度制限を付ける
        if (GetRightLegAngle() <= maxLegAngle)
        {
            //動かした回数を記録する
            rightLegCount++;

            //左腕の角度を更新する
            UpdateLeftArmAngle();

            //右膝の角度を更新する
            UpdateRightKneeAngle();

            //動かす
            GetPartsTran(PartsName.右足).Rotate(-rotAngle, 0f, 0f);
        }
    }

    /// <summary>
    /// 右足の角度を取得する
    /// </summary>
    /// <returns>右足の角度</returns>
    private float GetRightLegAngle()
    {
        //右足の角度を返す
        return rightLegCount * rotAngle;
    }

    /// <summary>
    /// 左足の角度を取得する
    /// </summary>
    /// <returns>左足の角度</returns>
    private float GetLeftLegAngle()
    {
        //左足の角度を返す
        return leftLegCount * rotAngle;
    }

    /// <summary>
    /// 右腕の角度を更新する
    /// </summary>
    private void UpdateRightArmAngle()
    {
        //始点（足の最小角度）を基準にした現在の足の角度を取得（必ず正になる）
        float currentLegAngleValue = GetLeftLegAngle() < 0f ? Math.Abs(minLegAngle - GetLeftLegAngle()) : GetLeftLegAngle() + Math.Abs(minLegAngle);

        //足の可動域（合計角度）を取得
        float totalLegAngle = maxLegAngle + Math.Abs(minLegAngle);

        //割合を取得
        float currentRatio = currentLegAngleValue / totalLegAngle;

        //腕の可動域（合計角度）を取得
        float totalArmAngle = maxArmAngle * 2;

        //腕の始点の角度を取得
        float firstArmAngle = 180f - maxArmAngle;

        //更新すべき角度を取得
        float angleZ = firstArmAngle + (totalArmAngle * currentRatio);

        //角度を設定
        GetPartsTran(PartsName.右腕).localEulerAngles = 
            new Vector3(GetPartsTran(PartsName.右腕).localEulerAngles.x, GetPartsTran(PartsName.右腕).localEulerAngles.y,angleZ);
    }

    /// <summary>
    /// 左腕の角度を更新する
    /// </summary>
    private void UpdateLeftArmAngle()
    {
        //始点（足の最小角度）を基準にした現在の足の角度を取得（必ず正になる）
        float currentLegAngleValue = GetRightLegAngle() < 0f ? Math.Abs(minLegAngle - GetRightLegAngle()) : GetRightLegAngle() + Math.Abs(minLegAngle);

        //足の可動域（合計角度）を取得
        float totalLegAngle = maxLegAngle + Math.Abs(minLegAngle);

        //割合を取得
        float currentRatio = currentLegAngleValue / totalLegAngle;

        //腕の可動域（合計角度）を取得
        float totalArmAngle = maxArmAngle * 2;

        //腕の始点の角度を取得
        float firstArmAngle = 180f - maxArmAngle;

        //更新すべき角度を取得
        float angleZ = firstArmAngle + (totalArmAngle * currentRatio);

        //角度を設定
        GetPartsTran(PartsName.左腕).localEulerAngles =
            new Vector3(GetPartsTran(PartsName.左腕).localEulerAngles.x, GetPartsTran(PartsName.左腕).localEulerAngles.y, angleZ);
    }

    /// <summary>
    /// 右膝の角度を更新する
    /// </summary>
    private void UpdateRightKneeAngle()
    {
        //始点（足の最小角度）を基準にした現在の足の角度を取得（必ず正になる）
        float currentLegAngleValue=GetRightLegAngle()<0f? Math.Abs( minLegAngle - GetRightLegAngle()):GetRightLegAngle()+Math.Abs(minLegAngle);
        
        //割合を取得
        float currentRatio = GetRightLegAngle()<0f  ? 
            (Math.Abs(minLegAngle) - currentLegAngleValue) / Math.Abs(minLegAngle) : (currentLegAngleValue- Math.Abs(minLegAngle) )/ maxLegAngle;
        
        //更新すべき角度を取得
        float angleX = 89f * currentRatio;

        //角度を設定
        GetPartsTran(PartsName.右膝).localEulerAngles = 
            new Vector3(angleX, GetPartsTran(PartsName.右膝).localEulerAngles.y, GetPartsTran(PartsName.右膝).localEulerAngles.z);
    }

    /// <summary>
    /// 左膝の角度を更新する
    /// </summary>
    private void UpdateLeftKneeAngle()
    {
        //始点（足の最小角度）を基準にした現在の足の角度を取得（必ず正になる）
        float currentLegAngleValue = GetLeftLegAngle() < 0f ? Math.Abs(minLegAngle - GetLeftLegAngle()) : GetLeftLegAngle() + Math.Abs(minLegAngle);

        //割合を取得
        float currentRatio = GetLeftLegAngle() < 0f ?
            (Math.Abs(minLegAngle) - currentLegAngleValue) / Math.Abs(minLegAngle) : (currentLegAngleValue - Math.Abs(minLegAngle)) / maxLegAngle;

        //更新すべき角度を取得
        float angleX = 89f * currentRatio;

        //角度を設定
        GetPartsTran(PartsName.左膝).localEulerAngles =
            new Vector3(angleX, GetPartsTran(PartsName.左膝).localEulerAngles.y, GetPartsTran(PartsName.左膝).localEulerAngles.z);
    }

    /// <summary>
    /// キャラクターの状態を初期化する
    /// </summary>
    public void ResetCharacterCondition()
    {
        //一旦、物理演算をクリアにする
        rb.isKinematic = true;

        //各部位のデータのリストの要素数だけ繰り返す
        for (int i = 0; i < partsDataList.Count; i++)
        {
            //体の各部位の角度を初期状態に設定
            partsDataList[i].partsTran.localEulerAngles = firstPartsLocalEulerAnglesList[i];
        }

        //キャラクターの位置と角度を初期状態に戻す
        transform.position = transform.eulerAngles = Vector3.zero;

        //カウントを初期化
        rightLegCount= leftLegCount = 0;

        //経過時間を初期化する
        uIManager.ResetTimer();

        //物理演算を再開
        rb.isKinematic = false;
    }
}
