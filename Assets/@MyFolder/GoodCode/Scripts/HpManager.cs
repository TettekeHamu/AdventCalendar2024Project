using System;
using UnityEngine;

namespace GoodCode
{
    /// <summary>
    /// Hp管理クラス
    /// </summary>
    public class HpManager
    {
        private int _hp;
        private int _maxHp;
        
        public event Action<int> OnChangeHpAction;
        
        public HpManager(int hp)
        {
            _hp = hp;
            _maxHp = hp;
        }
        
        public void Initialize()
        {
            OnChangeHpAction?.Invoke(_hp);
        }
        
        /// <summary>
        /// ダメージを受ける
        /// </summary>
        /// <returns>死亡したかどうか</returns>
        public bool TakeDamage(int damage)
        {
            if(damage < 0)
            {
                Debug.Log("ダメージに負の値は設定できません");
                return false;
            }
            
            _hp -= damage;
            
            if(_hp <= 0)
            {
                OnChangeHpAction?.Invoke(0);
                Debug.Log("Hpが0以下になりました");
                return true;
            }

            OnChangeHpAction?.Invoke(_hp);
            return false;
        }
        
        public void Recovery(int recovery)
        {
            if(recovery < 0)
            {
                Debug.Log("回復量に負の値は設定できません");
                return;
            }
            
            _hp += recovery;
            // 最大値の制限はclampで行う
            _hp = Mathf.Clamp(_hp, 0, _maxHp);
            
            OnChangeHpAction?.Invoke(_hp);
        }
    }   
}
