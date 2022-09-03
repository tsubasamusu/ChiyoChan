using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;//Serializable属性を使用

public class SoundManager : MonoBehaviour
{
    /// <summary>
    /// 音の名前
    /// </summary>
    public enum SoundName
    {
        GameStartSE,//ゲーム開始時に再生される効果音
        GameClearSE,//ゲームクリア時に再生される効果音
        RestartSE,//リスタートする時に再生される効果音
        MainBGM,//ゲーム中に流れるメインのBGM
    }

    /// <summary>
    /// 音のデータ
    /// </summary>
    [Serializable]
    public class SoundData
    {
        public SoundName name;//名前
        public AudioClip clip;//クリップ
    }

    [SerializeField]
    private List<SoundData> sounds = new List<SoundData>();//音のデータのリスト
}
