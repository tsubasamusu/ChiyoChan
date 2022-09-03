using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UIを使用

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
}
