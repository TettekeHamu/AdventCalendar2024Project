using System;
using System.Collections;
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
        private event Action<BulletManager> _onDestroyAction;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            //書き方②
            if (other.TryGetComponent(out ICollisionBullet collision))
            {
                _scoreManager.AddScore(DefineValue.BulletScore);
                collision.CollisionBullet();
            }
        }

        private IEnumerator BulletCoroutine()
        {
            yield return new WaitForSeconds(10f);
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _onDestroyAction?.Invoke(this);
        }

        public void Initialize(PlayerManager player, ScoreManager scoreManager, float moveSpeed, Vector3 direction)
        {
            _onDestroyAction += player.RemoveBullet;
            _scoreManager = scoreManager;
            _moveSpeed = moveSpeed;
            _direction = direction;
            StartCoroutine(BulletCoroutine());
        }

        public void MyFixedUpdate()
        {
            _rb2d.velocity = _direction * _moveSpeed;
        }
    }   
}
