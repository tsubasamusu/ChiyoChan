using System.Collections;//IEnumeratorを使用
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//LoadSceneメソッドを使用

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UIManager uIManager;//UIManager

    private AudioSource aud;//AudioSource

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
        aud= SoundManager.instance.PlaySound(SoundManager.instance.GetAudioClip(SoundManager.SoundName.MainBGM),true);

        //テキストの更新を開始する
        StartCoroutine(uIManager.StartUpdateText());

        //ゲーム開始状態に切り替える
        isGameStart = true;
    }

    /// <summary>
    /// ゲームの初期設定を行う
    /// </summary>
    private void SetUpGame()
    {
        //UIの初期設定を行う
        uIManager.SetUpUI();
    }

    /// <summary>
    /// ゲームクリア処理を行う
    /// </summary>
    /// <returns>待ち時間</returns>
    public IEnumerator SetGameClear()
    {
        //ゲームクリア状態に切り替える
        isGameClear = true;

        //BGMを止める
        aud.Stop();

        //効果音を再生
        SoundManager.instance.PlaySound(SoundManager.instance.GetAudioClip(SoundManager.SoundName.GameClearSE));

        //ゲームクリア演出が終わるまで待つ
        yield return StartCoroutine(uIManager.PlayGameClear());

        //Mainシーンを読み込む
        SceneManager.LoadScene("Main");
    }
}
