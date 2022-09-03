using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//UIを使用

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Image imgBackGround;//背景

    [SerializeField]
    private Image imgLogo;//ロゴ

    [SerializeField]
    private Text txtTime;//経過時間

    private float timer;//経過時間計測用
}
