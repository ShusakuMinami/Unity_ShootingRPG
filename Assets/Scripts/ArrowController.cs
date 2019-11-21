using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    // 1フレームで落下する距離
    public float downDistance;
    // オブジェクトを破棄する基準点
    public float minHeight;

    // Update is called once per frame
    void Update()
    {
        // フレームごとに等速で落下させる
        transform.Translate(0, -downDistance, 0);
        
        // 画面外に出たら矢を破棄する
        if(transform.position.y < minHeight){
            DestroyObstacle(gameObject);
        }
    }
    
    // プレイヤーに当たったとき
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            // 矢を破棄する
            DestroyObstacle(gameObject);
        }
    }
    
    // 障害物を破壊する
    public void DestroyObstacle(GameObject gameObject)
    {
        Destroy(gameObject);
    }
}
