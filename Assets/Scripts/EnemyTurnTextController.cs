using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTurnTextController : MonoBehaviour
{
    public GameObject text;
    
    // 開始時はテキストを非表示にする
    void Start()
    {
        StopPlaying();
    }
    
    // アニメーションを動作させる
    public void StartPlaying()
    {
        text.SetActive(true);
    }
    
    // アニメーションを停止させる
    public void StopPlaying()
    {
        text.SetActive(false);
    }
}
