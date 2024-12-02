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
        
        private Action<int> _onChangeScoreAction;
        
        public void AddChangeScoreAction(Action<int> action)
        {
            _onChangeScoreAction += action;
        }

        public ScoreManager()
        {
            _score = 0;
        }
        
        public void Initialize(Action<int> action)
        {
            _score = 0;
            _onChangeScoreAction += action;
            // Initialize時に_onChangeScoreActionがあるか不明なので引数でActionを受け取る
            _onChangeScoreAction?.Invoke(_score);
        }
        
        public void AddScore(int addScore)
        {
            if (addScore < 0)
            {
                Debug.Log("スコアに負の値は加算できません");
                return;
            }
            
            _score += addScore;
            _onChangeScoreAction?.Invoke(_score);
        }

        public void DecreaseScore(int decreaseScore)
        {
            if (decreaseScore < 0)
            {
                Debug.Log("スコアに負の値は減算できません");
                return;
            }

            _score -= decreaseScore;
            _score = Mathf.Max(0, _score);
            _onChangeScoreAction?.Invoke(_score);
        }
        
        ~ScoreManager()
        {
            _onChangeScoreAction = null;
        }
    }   
}
