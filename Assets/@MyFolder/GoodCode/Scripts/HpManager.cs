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
        private readonly int _maxHp;
        private event Action<int> OnChangeHpAction;
        
        public HpManager(int hp)
        {
            _hp = hp;
            _maxHp = hp;
        }
        
        public void Initialize(UIManager uiManager)
        {
            OnChangeHpAction += uiManager.UpdateHpText;
            OnChangeHpAction?.Invoke(_hp);
        }
        
        public bool TakeDamage(int damage)
        {
            if(damage < 0)
            {
                Debug.LogWarning("ダメージに負の値は設定できません");
                return false;
            }
            
            _hp -= damage;
            
            if(_hp <= 0)
            {
                OnChangeHpAction?.Invoke(0);
                Debug.LogWarning("Hpが0以下になりました");
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
            
            if(_hp > _maxHp)
            {
                _hp = _maxHp;
            }
            
            OnChangeHpAction?.Invoke(_hp);
        }
    }   
}
