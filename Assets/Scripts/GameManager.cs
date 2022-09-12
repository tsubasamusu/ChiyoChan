using System.Collections;//IEnumeratorを使用
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//LoadSceneメソッドを使用

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UIManager uIManager;//UIManager

    [SerializeField]
    private BodyController bodyController;//BodyController

    [SerializeField]
    private GoalController goalController;//GoalController

    [SerializeField]
    private KeyCode restartKey;//ゲーム再スタートキー

    private bool isGameStart;//ゲームが始まったかどうか

    private bool isGameClear;//ゲームクリアしたかどうか

    /// <summary>
    /// ゲーム開始判定取得用
    /// </summary>
    public bool IsGameStart
    { 
        get { return isGameStart; }
    }

    /// <summary>
    /// ゲーム終了判定取得用
    /// </summary>
    public bool IsGameClear
    {
        get { return isGameClear; }
    }

    /// <summary>
    /// ゲーム開始直後に呼び出される
    /// </summary>
    /// <returns>待ち時間</returns>
    private IEnumerator Start()
    {
        //ゲームの初期設定を行う
        SetUpGame();

        //効果音を再生
        SoundManager.instance.PlaySound(SoundManager.instance.GetAudioClip(SoundManager.SoundName.GameStartSE));

        //ゲームスタート演出が終わるまで待つ
        yield return StartCoroutine(uIManager.PlayGameStart());

        //BGMを再生
        SoundManager.instance.PlaySound(SoundManager.instance.GetAudioClip(SoundManager.SoundName.MainBGM),true);

        //テキストの更新を開始する
        StartCoroutine(uIManager.StartUpdateText());

        //キャラクターのコントロールを開始する
        StartCoroutine(bodyController.StartControlBody());

        //ゲーム開始状態に切り替える
        isGameStart = true;
    }

    /// <summary>
    /// 毎フレーム呼び出される
    /// </summary>
    private void Update()
    {
        //ゲーム再スタートキーが押されたら
        if (Input.GetKeyDown(restartKey))
        {
            //キャラクターの状態を初期化する
            bodyController.ResetCharacterCondition();

            //効果音を再生
            SoundManager.instance.PlaySound(SoundManager.instance.GetAudioClip(SoundManager.SoundName.RestartSE));
        }
    }

    /// <summary>
    /// ゲームの初期設定を行う
    /// </summary>
    private void SetUpGame()
    {
        //UIの初期設定を行う
        uIManager.SetUpUI();

        //GoalControllerの初期設定を行う
        goalController.SetUpGoalController(this);

        //BodyControllerの初期設定を行う
        bodyController.SetUpBodyController();
    }

    /// <summary>
    /// ゲームクリアの準備を行う
    /// </summary>
    public void PrepareGameClear()
    {
        //ゲームクリア処理を行う
        StartCoroutine(SetGameClear());
    }

    /// <summary>
    /// ゲームクリア処理を行う
    /// </summary>
    /// <returns>待ち時間</returns>
    private IEnumerator SetGameClear()
    {
        //ゲームクリア状態に切り替える
        isGameClear = true;

        //テキストの更新を止める
        uIManager.StopUpdateText = true;

        //BGMを止める
        SoundManager.instance.StopMainSound(0.5f);

        //効果音を再生
        SoundManager.instance.PlaySound(SoundManager.instance.GetAudioClip(SoundManager.SoundName.GameClearSE));

        //ゲームクリア演出が終わるまで待つ
        yield return StartCoroutine(uIManager.PlayGameClear());

        //Mainシーンを読み込む
        SceneManager.LoadScene("Main");
    }
}
