using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tsubasa//名前空間宣言
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Transform lookTran;//対象物

        [SerializeField]
        private float smooth;//視点移動の滑らかさ

        private Vector3 firstPos;//カメラの初期位置

        private bool set;//初期設定を行ったかどうか

        /// <summary>
        /// CameraControllerの初期設定を行う
        /// </summary>
        public void SetUpCameraController()
        {
            //カメラの初期位置を取得
            firstPos = transform.position;

            //初期設定完了状態に切り替える
            set = true;
        }

        /// <summary>
        /// 一定時間ごとに呼び出される
        /// </summary>
        private void FixedUpdate()
        {
            //初期設定が完了していないなら
            if(!set)
            {
                //以降の処理を行わない
                return;
            }

            //更新すべき位置を取得
            Vector3 pos = new Vector3(firstPos.x, firstPos.y, lookTran.position.z);

            //カメラを滑らかに移動させる
            transform.position = Vector3.Lerp(transform.position, pos, Time.fixedDeltaTime * smooth);
        }
    }
}
