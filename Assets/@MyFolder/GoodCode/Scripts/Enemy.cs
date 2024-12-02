using UnityEngine;

namespace GoodCode
{
    /// <summary>
    /// 敵クラス
    /// </summary>
    public class Enemy : MonoBehaviour, ICollisionBullet
    {
        [SerializeField] private RecoveryItem _recoveryItemPrefab;
        [SerializeField] private Rigidbody2D _rb2d;

        [Header("アイテムのドロップ率")] [SerializeField]
        private float dropRate = 20;

        [Header("回転するフレーム数")] [SerializeField] private int lookAtFrame = 60;
        [Header("接触時のダメージ")] [SerializeField] private int takeDamage = 1;
        private Transform _player;
        private float _moveSpeed;
        private int _counter;

        private void Awake()
        {
            _player = FindObjectOfType<PlayerManager>().transform;
            _moveSpeed = 3;
        }

        private void Update()
        {
            if (_counter >= lookAtFrame) return;
            _counter++;
            var direction = _player.transform.position - transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }

        private void FixedUpdate()
        {
            _rb2d.velocity = transform.up * _moveSpeed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // TryGetComponentでインターフェースを持っているか確認
            if (!other.TryGetComponent(out ICollisionEnemy collision)) return;
            collision.TakeDamage(takeDamage);
            Destroy(gameObject);
        }

        /// <summary>
        /// 衝突時の処理
        /// </summary>
        public void OnCollisionBullet()
        {
            Death();
        }

        /// <summary>
        /// 死亡時の判定
        /// </summary>
        private void Death()
        {
            // Itemのドロップ判定は別クラスに任せる
            var isDrop = LotteryManager.LotteryDropItem(dropRate);
            if (isDrop)
            {
                Instantiate(_recoveryItemPrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}