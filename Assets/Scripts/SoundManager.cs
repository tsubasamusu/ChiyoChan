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
    private List<SoundData> soundDatasList = new List<SoundData>();//音のデータのリスト

    [SerializeField]
    private AudioSource mainAud;//メインのAudioSource

    [SerializeField]
    private AudioSource subAud;//サブのAudioSource

    public static SoundManager instance;//インスタンス

    /// <summary>
    /// Startメソッドより前に呼び出される
    /// </summary>
    private void Awake()
    {
        //以下、シングルトンに必須の記述
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 指定した名前の音のクリップを取得する
    /// </summary>
    /// <param name="name">音の名前</param>
    /// <returns>音のクリップ</returns>
    public AudioClip GetAudioClip(SoundName name)
    {
        //指定した名前の音のクリップを取得する
        return soundDatasList.Find(x => x.name == name).clip;
    }

    /// <summary>
    /// 音を再生する
    /// </summary>
    /// <param name="clip">音のクリップ</param>
    /// <param name="loop">繰り返すかどうか</param>
    /// <returns>使用したAudioSource</returns>
    public AudioSource PlaySound(AudioClip clip,bool loop=false)
    {
        //繰り返すなら（BGMなら）
        if(loop)
        {
            //繰り返すように設定
            mainAud.loop = true;

            //音のクリップを設定
            mainAud.clip= clip;

            //音を再生する
            mainAud.Play();

            //使用したAudioSourceを返す
            return mainAud;
        }
        //繰り返さないなら（効果音なら
        else
        {
            //音を再生する
            subAud.PlayOneShot(clip);

            //使用したAudioSourceを返す
            return subAud;
        }
    }
}
