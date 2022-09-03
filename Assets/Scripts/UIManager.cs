using System.Collections;//IEnumeratorを使用
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UIを使用
using DG.Tweening;//DOTweenを使用

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Image imgBackground;//背景

    [SerializeField]
    private Image imgLogo;//ロゴ

    [SerializeField]
    private Text txtTime;//経過時間

    [SerializeField]
    private Text txtLength;//ゴールまでの距離

    [SerializeField]
    private Sprite sprGameStart;//ゲームスタートロゴ

    [SerializeField]
    private Sprite sprGameClear;//ゲームクリアロゴ

    private float timer;//経過時間計測用

    /// <summary>
    /// ゲーム開始直後に呼び出される
    /// </summary>
    /// <returns>待ち時間</returns>
    private IEnumerator Start()
    {
        //UIの初期設定を行う
        SetUpUI();

        yield return StartCoroutine(PlayGameStart());

        yield return StartCoroutine(PlayGameClear());

        Debug.Log("End");
    }

    /// <summary>
    /// UIの初期設定を行う
    /// </summary>
    private void SetUpUI()
    {
        //背景を白色に設定
        imgBackground.color = Color.white;

        //テキストを空にする
        txtLength.text=txtTime.text=string.Empty;
    }

    /// <summary>
    /// ゲームスタート演出を行う
    /// </summary>
    /// <returns>待ち時間</returns>
    public IEnumerator PlayGameStart()
    {
        //演出が終わったかどうか
        bool end = false;

        //背景を表示
        imgBackground.DOFade(1f, 0f);

        //ロゴを「GameStart」に設定
        imgLogo.sprite = sprGameStart;

        //ロゴを非表示にする
        imgLogo.DOFade(0f, 0f);

        //Sequenceを登録
        Sequence sequence = DOTween.Sequence();

        //ロゴのアニメーションを行う
        sequence.Append(imgLogo.DOFade(1f, 1f).SetLoops(2, LoopType.Yoyo));

        //背景を非表示にする
        sequence.Append(imgBackground.DOFade(0f, 1f)).OnComplete(()=>end=true);

        //演出が終わるまで待つ
        yield return new WaitUntil(()=>end);
    }

    /// <summary>
    /// ゲームクリア演出を行う
    /// </summary>
    /// <returns>待ち時間</returns>
    public IEnumerator PlayGameClear()
    {
        //演出が終わったかどうか
        bool end = false;

        //ロゴを「GameClear」に設定
        imgLogo.sprite = sprGameClear;

        //背景を表示
        imgBackground.DOFade(1f, 1f);

        //Sequenceを登録
        Sequence sequence = DOTween.Sequence();

        //ロゴを表示
        sequence.Append(imgLogo.DOFade(1f, 1f));

        //１秒待つ
        sequence.AppendInterval(1f);

        //ロゴを非表示にする
        sequence.Append(imgLogo.DOFade(0f, 1f)).OnComplete(()=>end=true);

        //演出が終わるまで待つ
        yield return new WaitUntil(() => end);
    }
}
