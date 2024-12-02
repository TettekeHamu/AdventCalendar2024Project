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
            // 数字の表示はLabelとValueを分ける
            _scoreText.text = score.ToString();
        }
        
        public void UpdateHpText(int hp)
        {
            // 数字の表示はLabelとValueを分ける
            _hpText.text = hp.ToString();
        }
    }
}
