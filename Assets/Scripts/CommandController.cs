using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    // ステートの変異を示す変数
    public bool changeState = false;
    
    // コマンド選択画面のタイトルと背景画像
    public GameObject commandTitle;
    public GameObject commandBackground;
    
    // 各コマンドボタンのオブジェクトのリスト
    public GameObject[] commandButtons;
    public GameObject[] commandInformations;
    
    // 情報を出すときに一緒に表示するYes/Noボタン
    public GameObject yesButton;
    public GameObject noButton;
    
    // 現在タッチされているボタンのidを保持する変数
    public int curCommandID;
    
    // 現在のコマンドステートを返す
    public bool GetChangeState()
    {
        return changeState;
    }
    
    // コマンドステートから遷移しないように設定する
    public void NotChangetoShooting()
    {
        changeState = false;
    }
    
    void Start()
    {
        // ゲーム開始時は全て非表示にする
        commandTitle.SetActive(false);
        commandBackground.SetActive(false);
        SetButton(false);
        UnsetInformation();
        yesButton.SetActive(false);
        noButton.SetActive(false);
    }
    
    // コマンド選択ボタンを全て表示または非表示する
    public void SetButton(bool flag)
    {
        // flagがtrueなら表示、falseなら非表示
        for(int i = 0; i < commandButtons.Length; i++){
            commandButtons[i].SetActive(flag);
        }
        
    }
    
    // 情報を全て非表示にする
    public void UnsetInformation()
    {
        for(int i = 0; i < commandInformations.Length; i++){
            commandInformations[i].SetActive(false);
        }
    }
    
    // タイトルテキストと背景、コマンドボタンを全て表示する
    public void ShowCommandButton()
    {
        commandTitle.SetActive(true);
        commandBackground.SetActive(true);
        SetButton(true);
    }
    
    // ボタンがタッチされた時、それに対応した情報とYes/Noボタンを表示する
    public void TouchCommandButton(int id)
    {
        commandInformations[id].SetActive(true);
        yesButton.SetActive(true);
        noButton.SetActive(true);
        curCommandID = id;
    }
    
    // Yesボタンがタッチされたとき
    public void TouchYesButton()
    {
        // 全てのボタンを非表示にする
        commandTitle.SetActive(false);
        commandBackground.SetActive(false);
        SetButton(false);
        UnsetInformation();
        yesButton.SetActive(false);
        noButton.SetActive(false);
        // ステートを変える
        changeState = true;
    }
    
    // Noボタンがタッチされたら、コマンドボタンのみ表示する
    public void TouchNoButton()
    {
        UnsetInformation();
        yesButton.SetActive(false);
        noButton.SetActive(false);
    }
}
