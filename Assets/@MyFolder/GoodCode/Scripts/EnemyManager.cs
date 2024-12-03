using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GoodCode
{
    /// <summary>
    /// 敵管理クラス
    /// </summary>
    public class EnemyManager : MonoBehaviour, ICollisionBullet
    {
        [SerializeField] private RecoveryItemManager _recoveryItemPrefab;
        [SerializeField] private Rigidbody2D _rb2d;
        private Transform _player;
        private float _moveSpeed;
        private float _counter;
        private event Action<EnemyManager> _onDestroyAction;

        private void OnTriggerEnter2D(Collider2D other)
        {
            //書き方①
            var player = other.GetComponent<ICollisionEnemy>();
            if (player == null) return;
            player.TakeDamage(DefineValue.Damage);
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _onDestroyAction?.Invoke(this);
        }

        public void Initialize(GameManager gameManager)
        {
            _onDestroyAction += gameManager.RemoveEnemy;
            _player = FindObjectOfType<PlayerManager>().transform;
            _moveSpeed = 3;
        }
        
        public void MyUpdate()
        {
            if (_counter < 1)
            {
                _counter += Time.deltaTime;
                var direction = _player.transform.position - transform.position;
                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            }
        }
        
        public void MyFixedUpdate()
        {
            _rb2d.velocity= transform.up * _moveSpeed;
        }

        public void CollisionBullet()
        {
            var dropRate = 20;
            var random = Random.Range(0, 100);
            if(dropRate < random) Instantiate(_recoveryItemPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }   
}
