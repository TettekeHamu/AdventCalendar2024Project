using UnityEngine;

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

        private void Awake()
        {
            _player = FindObjectOfType<PlayerManager>().transform;
            _moveSpeed = 3;
        }

        private void Update()
        {
            if (_counter < 1)
            {
                _counter += Time.deltaTime;
                var direction = _player.transform.position - transform.position;
                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            }
        }
        
        private void FixedUpdate()
        {
            _rb2d.velocity= transform.up * _moveSpeed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //書き方①
            var player = other.GetComponent<ICollisionEnemy>();
            if (player == null) return;
            player.TakeDamage(DefineValue.Damage);
            Destroy(gameObject);
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
