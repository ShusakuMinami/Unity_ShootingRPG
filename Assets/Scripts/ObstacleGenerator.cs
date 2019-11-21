using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public PlayerController player;
    
    // 生成する障害物オブジェクトの配列
    public GameObject[] obstaclePrefabs;
    // 障害物を生成するスパン
    public float span;
    // 経過した時間を記録する
    float delta;
    
    // 障害物の生成箇所の最左値、最右値、上端
    public float maxLeft;
    public float maxRight;
    public float start;
    
    // Update is called once per frame
    public void ObstacleGenerate()
    {
        // playerのライフがまだある時、生成を行う
        if(!player.IsDead()){
            delta += Time.deltaTime;
            // スパンの時間だけ経過したとき、生成を行う
            if(delta > span){
                delta = 0.0f;
                GameObject obstacle = null;
                float px;
            
                // obstaclePrefabsの中からランダムに生成する
                int index = Random.Range(0, obstaclePrefabs.Length);
                obstacle = Instantiate(obstaclePrefabs[index])
                    as GameObject;
            
                // ランダムに生成するポジションを決定する
                px = Random.Range(maxLeft, maxRight);
            
                // obstacleを生成する
                obstacle.transform.position =
                    new Vector3(px, start, 0);
            }
        }
    }
}
