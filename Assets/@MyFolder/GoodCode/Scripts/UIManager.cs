using System;
using UnityEngine;
using UnityEngine.UI;

namespace GoodCode
{
    /// <summary>
    /// UI管理クラス
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Text _scoreText;
        [SerializeField] private Text _hpText;
        
        public void UpdateScoreText(int score)
        {
            _scoreText.text = $"Score : {score}";
        }
        
        public void UpdateHpText(int hp)
        {
            _hpText.text = $"HP : {hp}";
        }
    }
}
