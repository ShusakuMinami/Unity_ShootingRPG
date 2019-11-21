using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPController : MonoBehaviour
{
    public GameObject hpGauge;
    
    public float GetEnemyHP()
    {
        return hpGauge.GetComponent<Image>().fillAmount;
    }
    
    public void UpdateEnemyHP(float damage)
    {
        // HPが残っているとき、ダメージ分だけHPゲージを減らす
        if(hpGauge.GetComponent<Image>().fillAmount > 0){
            hpGauge.GetComponent<Image>().fillAmount -= damage;
        }
    }
}
