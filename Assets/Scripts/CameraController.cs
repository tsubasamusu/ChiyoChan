using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform targetTran;//対象物

    [SerializeField]
    private float smooth;//視点移動の滑らかさ

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
    /// 一定時間ごとに呼び出される
    /// </summary>
    private void FixedUpdate()
    {
        //更新すべき位置を取得
        Vector3 pos = new Vector3(firstPos.x,firstPos.y,targetTran.position.z);

        //カメラを滑らかに移動させる
        transform.position = Vector3.Lerp(transform.position, pos, Time.fixedDeltaTime * smooth);
    }
}
