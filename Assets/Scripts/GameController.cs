using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // ゲームステート
    enum State
    {
        Command,
        Ready,
        Shooting,
        Attack,
        Clear,
        GameOver
    }
    
    // ステートを管理する変数
    State state;
    
    // 各種コントローラーオブジェクト
    public PlayerController player;
    public ObstacleGenerator obstacle;
    public LifeController lifeController;
    public EnemyHPController enemyHP;
    public CommandController command;
    public EnemyTurnTextController enemyTurnText;
    public BattleController battle;
    
    // UIオブジェクトたち
    public GameObject attackText;
    public GameObject clearText;
    public GameObject gameOverText;
    
    // 準備時、シューティング時の経過時間の管理
    public float readySpanTime;
    public float shootingSpanTime;
    float delta;

    // スタート時、ゲームを開始する
    void Start()
    {
        GameStart();
    }
    
    // Update is called once per frame
    public void Update()
    {
        // ライフアイコンを更新
        lifeController.UpdateLife(player.Life());
        
        switch(state)
        {
            // コマンド選択時
            case State.Command:
                Command();
                break;
            
            // シューティング準備時
            case State.Ready:
                Ready();
                break;
            
            // シューティングゲーム時
            case State.Shooting:
                Shooting();
                break;
            
            // プレイヤーの反撃時
            case State.Attack:
                Attack();
                break;
            
            // クリア時
            case State.Clear:
                Clear();
                break;
                
            // ゲームオーバー時
            case State.GameOver:
                GameOver();
                break;
        }
    }

    // ゲーム開始
    void GameStart()
    {
        // デバッグ用のテキストを非表示
        attackText.SetActive(false);
        clearText.SetActive(false);
        gameOverText.SetActive(false);
        
        // 時間管理変数を0に
        delta = 0.0f;
        // コマンド選択フェーズへ移行
        state = State.Command;
    }
    
    // コマンド選択
    void Command()
    {
        // 現在のコマンドステートを取得
        bool ableChangeState = command.GetChangeState();
        
        // コマンドステートが「遷移」になった場合、
        // プレイヤーを操作可能にし、シューティングフェーズ準備へ移行
        if(ableChangeState){
            state = State.Ready;
            player.SetSteerActive(true);
            // ステータスの変化の影響を与える
            battle.ChangeStatus();
            // 次にコマンドフェーズに居座るために、ステート変数を変える
            command.NotChangetoShooting();
            
        }
        // そうでない場合は、コマンドボタンを表示し、プレイヤーを操作しない
        else{
            command.ShowCommandButton();
            player.SetSteerActive(false);
        }
    }
    
    // シューティング準備
    void Ready()
    {
        // 時間を加算
        delta += Time.deltaTime;
        
        // Enemy Turnのテキストを表示
        enemyTurnText.StartPlaying();
        
        // アニメーションが終了するとシューティングフェーズへ
        if(delta > readySpanTime){
            delta = 0.0f;
            enemyTurnText.StopPlaying();
            state = State.Shooting;
        }
    }
    
    // シューティング
    void Shooting()
    {
        // 時間を加算
        delta += Time.deltaTime;
        // 障害物を生成
        obstacle.ObstacleGenerate();
        
        // 一定時間経過したらプレイヤーを操作不能にし、反撃フェーズに移る
        if(delta > shootingSpanTime){
            delta = 0.0f;
            player.SetSteerActive(false);
            state = State.Attack;
        }
        
        // プレイヤーが死亡したらゲームオーバー
        if(player.IsDead()){
            state = State.GameOver;
        }
    }
    
    //プレイヤーの反撃
    void Attack()
    {
        // デバッグとして攻撃のテキストを表示
        attackText.SetActive(true);
        
        // プレイヤーが死亡していたらゲームオーバーの処理を行う
        if(player.IsDead()){
            state = State.GameOver;
        }
        else{
            // クリックしたら攻撃の処理を行う
            if(Input.GetButtonDown("Fire1")){
                // 設定した量(0~1)のダメージ分だけHPゲージを減らす
                enemyHP.UpdateEnemyHP(player.power);
                
                // テキストを非表示にする
                attackText.SetActive(false);
                
                // 敵を倒したらクリアフェーズに移る
                float curEnemyHP = enemyHP.GetEnemyHP();
                if(curEnemyHP <= 0){
                    state = State.Clear;
                }
                else{
                // コマンド選びのフェーズに戻る
                    state = State.Command;
                }
            }
        }
    }
    
    // クリア
    void Clear()
    {
        // デバッグとしてクリアのテキストを表示
        clearText.SetActive(true);
        // クリックしたら再開する
        if(Input.GetButtonDown("Fire1")){
            Restart();
        }
    }
    
    // ゲームオーバー
    void GameOver()
    {
        // デバッグとしてゲームオーバーのテキストを表示
        gameOverText.SetActive(true);
        // クリックしたら再開する
        if(Input.GetButtonDown("Fire1")){
            Restart();
        }
    }
    
    // 再スタート
    void Restart()
    {
        // 現在のシーンを再読み込み
        Scene loadScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(loadScene.name);
    }
}
