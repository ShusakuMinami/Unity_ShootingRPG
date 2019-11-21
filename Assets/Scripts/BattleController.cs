using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// バトルのコマンドでの能力の繁栄をコントロールするクラス
public class BattleController : MonoBehaviour
{
    // 各種コントローラーオブジェクト
    public CommandController command;
    public PlayerController player;
    
    // ステータスを変更
    public void ChangeStatus()
    {
        switch(command.curCommandID)
        {
            // 攻撃力UPボタンが押された時
            case 0:
                player.power = 0.21f * 1.5f;
                player.barrior = false;
                break;
            // バリアボタンが押された時
            case 1:
                player.power = 0.21f;
                player.barrior = true;
                break;
            // 回復ボタンが押された時
            case 2:
                player.power = 0.21f;
                player.barrior = false;
                if(player.life < 3){
                    player.life += 1;
                }
                break;
        }
    }
}
