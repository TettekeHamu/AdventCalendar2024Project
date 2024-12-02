using UnityEngine;

namespace GoodCode
{
    /// <summary>
    /// 弾の管理クラス
    /// </summary>
    public class BulletManager : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb2d;
        private ScoreManager _scoreManager;
        private Vector3 _direction;
        private float _moveSpeed;
        
        public void Initialize(ScoreManager scoreManager, float moveSpeed, Vector3 direction)
        {
            _scoreManager = scoreManager;
            _moveSpeed = moveSpeed;
            _direction = direction;
        }

        private void FixedUpdate()
        {
            _rb2d.velocity = _direction * _moveSpeed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ICollisionBullet collision))
            {
                // スコアの加算の数字は管理しやすいように定数化する
                _scoreManager.AddScore(DefineValues.BulletScore);
                collision.OnCollisionBullet();
            }
        }
    }
}
