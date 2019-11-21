using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkController : MonoBehaviour
{
    // 点滅時間
    public float flashingTime = 1.0f;
    // 点滅の間隔
    float interval = 0.1f;
    
    // 点滅を開始させるコルーチン
    public IEnumerator Blink(Renderer render)
    {
        float counter = flashingTime;
        
        while(counter > 0.0f){
            // オブジェクトの表示・非表示を切り替える
            render.enabled = !render.enabled;
            // インターバルの時間分だけ、次の処理を待つ
            yield return new WaitForSeconds(interval);
            // カウンターの値を減らす
            counter -= interval;
        }
    }
}
