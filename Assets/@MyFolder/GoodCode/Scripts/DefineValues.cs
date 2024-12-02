namespace GoodCode
{
    /// <summary>
    /// 定義した数字の管理クラス
    /// 必要ならMonoBehaviourのクラスやScriptableObjectにする
    /// </summary>
    public static class DefineValues
    {
        // Scoreの定数
        public const int BulletScore = 100;
        public const int TakeDamageDecreaseScore = 10;
        
        // Playerの定数
        public const int PlayerHp = 10;
        public const float PlayerMoveSpeed = 5f;
        
        // 回復アイテムの定数
        public const float RecoveryItemMoveSpeed = 2f;
        public const int RecoveryItemRecoveryValue = 2;
    }
}