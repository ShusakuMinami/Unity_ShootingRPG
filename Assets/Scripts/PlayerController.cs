using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // デフォルトのライフ
    const int DefaultLife = 3;
    // デフォルトのパワー
    const float DefaultPower = 0.21f;
    
    // 無敵時間の点滅のコントローラー
    public BlinkController blinkController;
    
    // 無敵時間
    float invincibleTime = 0.0f;
    
    // 各種コンポーネント
    Rigidbody2D rigid2D;
    Renderer render;
     
    // 移動距離、ライフ、パワー、バリアの有無
    public float moveForce;
    public int life = DefaultLife;
    public float power = DefaultPower;
    public bool barrior = false;
    
    void Start()
    {
        // 各種コンポーネントの獲得
        rigid2D = GetComponent<Rigidbody2D>();
        render = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float key;

        // 矢印キーを押したとき、左右移動
        if(Input.GetKey(KeyCode.LeftArrow)){
            key = -1.0f;
            MoveHorizontal(key);
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            key = 1.0f;
            MoveHorizontal(key);
        }
        // 矢印キーを離したとき、停止
        if(!Input.GetKey(KeyCode.LeftArrow) &&
            !Input.GetKey(KeyCode.RightArrow)){
            StopHorizontal();
        }
            
        // プレイヤーが無敵時間なら、その時間を減らす
        if(IsInvincible()){
            invincibleTime -= Time.deltaTime;
        }
    }
    
    // 現在のライフを返す
    public int Life()
    {
        return life;
    }
    
    // プレイヤーのパワーを返す
    public float Power()
    {
        return power;
    }
    
    // バリアの有無を返す
    public bool Barrior()
    {
        return barrior;
    }
    
    // 現在が無敵時間かどうかを返す
    public bool IsInvincible()
    {
        return invincibleTime > 0.0f;
    }
    
    // プレイヤーが死んでいるか否かを返す
    public bool IsDead()
    {
        if(life > 0){
            return false;
        }
        else{
            return true;
        }
    }
    
    // keyが正なら右に、負なら左に移動
    public void MoveHorizontal(float key)
    {
        rigid2D.velocity = new Vector2(key * moveForce, 0.0f);
    }
    
    // 停止する
    public void StopHorizontal()
    {
        rigid2D.velocity = new Vector2(0.0f, 0.0f);
    }
    
    // 衝突時の動作
    void OnTriggerEnter2D(Collider2D other)
    {
        // バリア時に当たった時
        if(Barrior()){
            // バリアを消す
            barrior = false;
            // 無敵時間を設定
            invincibleTime = blinkController.flashingTime;
            // 点滅のコルーチンを始める
            StartCoroutine(blinkController.Blink(render));
        }
        // 無敵でない時に障害物に当たったとき
        else if(other.gameObject.tag == "Obstacle" && !IsInvincible()){
            // ライフを減らす
            --life;
            // 無敵時間を設定
            invincibleTime = blinkController.flashingTime;
            // 点滅のコルーチンを始める
            StartCoroutine(blinkController.Blink(render));
        }
        
        // ライフが0になった時、消滅させる
        if(life <= 0){
            Destroy(gameObject);
        }
    }
    
    // 外部関数からプレイヤーの操作の可否を操作する
    public void SetSteerActive(bool active)
    {
        // Rigidbody2Dのオンオフを切り替える
        rigid2D.isKinematic = !active;
    }
}
