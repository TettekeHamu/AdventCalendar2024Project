namespace GoodCode
{
    /// <summary>
    /// 色々なクラスで使用する定数をまとめたクラス
    /// </summary>
    public static class DefineValue
    {
        //スコア周り
        private static readonly int _bulletScore = 100;
        private static readonly int _damageScore = 1000;
        //HP周り
        private static readonly int _initialHp = 10;
        private static readonly int _damage = 1;
        private static readonly int _recovery = 2;
        
        public static int BulletScore => _bulletScore;
        public static int DamageScore => _damageScore;
        public static int InitialHp => _initialHp;
        public static int Damage => _damage;
        public static int Recovery => _recovery;
    }
}
