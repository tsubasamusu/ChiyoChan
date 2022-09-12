using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    private GameManager gameManager;//GameManager

    /// <summary>
    /// GoalControllerの初期設定を行う
    /// </summary>
    /// <param name="gameManager">GameManager</param>
    public void SetUpGoalController(GameManager gameManager)
    {
        //GameManagerを取得
        this.gameManager = gameManager;
    }

    /// <summary>
    /// 他のコライダーがすり抜けた際に呼び出される
    /// </summary>
    /// <param name="other">接触相手</param>
    private void OnTriggerEnter(Collider other)
    {
        //接触相手からBodyControllerを取得出来たら（接触相手がキャラクターなら）
        if(other.TryGetComponent(out BodyController bodyController))
        {
            //ゲームクリアの準備を行う
            gameManager.PrepareGameClear();

            //BodyControllerを非活性化する（重複処理防止）
            bodyController.enabled = false;
        }
    }
}
