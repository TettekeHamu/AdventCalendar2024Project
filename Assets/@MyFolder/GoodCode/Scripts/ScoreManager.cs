using System;
using UnityEngine;

namespace GoodCode
{
    /// <summary>
    /// スコア管理クラス
    /// </summary>
    public class ScoreManager 
    {
        private int _score;
        private event Action<int> OnChangeScoreAction; 

        public ScoreManager()
        {
            _score = 0;
        }
        
        public void Initialize(UIManager uiManager)
        {
            OnChangeScoreAction += uiManager.UpdateScoreText;
            OnChangeScoreAction?.Invoke(_score);
        }
        
        public void AddScore(int addScore)
        {
            if (addScore < 0)
            {
                Debug.LogWarning("スコアに負の値は加算できません");
                return;
            }
            
            _score += addScore;
            OnChangeScoreAction?.Invoke(_score);
        }

        public void DecreaseScore(int decreaseScore)
        {
            if (decreaseScore < 0)
            {
                Debug.LogWarning("スコアに負の値は減算できません");
                return;
            }

            _score -= decreaseScore;
            if (_score <= 0)
            {
                _score = 0;
            }
            OnChangeScoreAction?.Invoke(_score);
        }
    }   
}
