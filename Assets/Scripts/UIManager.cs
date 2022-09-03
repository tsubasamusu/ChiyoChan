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

    [SerializeField]
    private Transform playerTran;//プレーヤーの位置情報

    [SerializeField]
    private Transform goalTran;//ゴールの位置情報

    [SerializeField]
    private GameManager gameManager;//GameManager

    private float timer;//経過時間計測用

    /// <summary>
    /// UIの初期設定を行う
    /// </summary>
    public void SetUpUI()
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
        //「ゴールまでの距離」のテキストを非活性化する
        txtLength.enabled = false;

        //ロゴを「GameClear」に設定
        imgLogo.sprite = sprGameClear;

        //背景を表示
        imgBackground.DOFade(1f, 1f);

        //ロゴを表示
        imgLogo.DOFade(1f, 1f);

        //経過時間を適切な位置に移動させる
        txtTime.transform.DOMoveX(900f, 1f);

        //テキストの色を変化させる
        txtTime.DOColor(Color.blue, 1f);

        //2秒待つ
        yield return new WaitForSeconds(2f);

        //ロゴを非表示にする
        imgLogo.DOFade(0f, 1f);

        //経過時間を非表示にする
        txtTime.DOFade(0f, 1f);

        //2秒待つ
        yield return new WaitForSeconds(2f);
    }

    /// <summary>
    /// 「経過時間」「ゴールまでの距離」のテキストの更新を開始する
    /// </summary>
    /// <returns>待ち時間</returns>
    public IEnumerator StartUpdateText()
    {
        //無限に繰り返す
        while(true)
        {
            //ゲームが終了したら
            if(gameManager.IsGameClear)
            {
                //繰り返し処理を終了する
                break;
            }

            //ゴールまでの距離を計算して表示
            txtLength.text = (goalTran.position.z - playerTran.position.z).ToString("F2")+"m\nTo Goal";

            //経過時間を計測
            timer+=Time.deltaTime;

            //経過時間を表示
            txtTime.text=timer.ToString("F2")+ "\nSecond";

            //次のフレームへ飛ばす（実質、Updateメソッド）
            yield return null;
        }
    }

    /// <summary>
    /// 経過時間を初期化する
    /// </summary>
    public void ResetTimer()
    {
        //経過時間を0秒にする
        timer = 0f;
    }
}
