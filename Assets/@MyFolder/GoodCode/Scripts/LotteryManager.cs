using UnityEngine;

namespace GoodCode
{
    /// <summary>
    /// アイテムのドロップ判定を行う管理クラス
    /// </summary>
    public static class LotteryManager
    {
        /// <summary>
        /// アイテムのドロップ判定
        /// FIXME:ドロップするアイテムを返す
        /// param: ドロップ率（0~100）
        /// </summary>
        public static bool LotteryDropItem(double percentage)
        {
            // 確率範囲の生合成をチェック
            if(percentage is < 0 or > 100) throw new System.ArgumentOutOfRangeException(nameof(percentage));
            var random = Random.Range(0, 100);
            return random < percentage;
        }
    }
}