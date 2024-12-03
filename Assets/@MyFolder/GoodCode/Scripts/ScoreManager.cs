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
        private event Action<int> _onChangeScoreAction; 

        public ScoreManager()
        {
            _score = 0;
        }
        
        public void Initialize(UIManager uiManager)
        {
            _onChangeScoreAction += uiManager.UpdateScoreText;
            _onChangeScoreAction?.Invoke(_score);
        }
        
        public void AddScore(int addScore)
        {
            if (addScore < 0)
            {
                Debug.LogWarning("スコアに負の値は加算できません");
                return;
            }
            
            _score += addScore;
            _onChangeScoreAction?.Invoke(_score);
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
            _onChangeScoreAction?.Invoke(_score);
        }
    }   
}
