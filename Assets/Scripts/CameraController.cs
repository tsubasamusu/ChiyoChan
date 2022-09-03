using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform targetTran;//対象物

    private Vector3 firstPos;//カメラの初期位置

    /// <summary>
    /// ゲーム開始直後に呼び出される
    /// </summary>
    private void Start()
    {
        //カメラの初期位置を取得
        firstPos = transform.position;
    }

    /// <summary>
    /// 毎フレーム呼び出される
    /// </summary>
    private void Update()
    {
        //カメラの位置を更新
        transform.position = new Vector3(targetTran.position.x,firstPos.y,firstPos.z);
    }
}
