using System;
using UnityEngine;

namespace GoodCode
{
    /// <summary>
    /// プレイヤー管理クラス
    /// </summary>
    public class PlayerManager : MonoBehaviour, ICollisionEnemy, ICollisionItem
    {
        [SerializeField] private BulletManager _bulletPrefab;
        [SerializeField] private Rigidbody2D _rb2d;
        private UIManager _uiManager;
        private ScoreManager _scoreManager;
        private HpManager _hpManager;
        private float _moveSpeed;
        private Vector3 _direction;

        public event Action OnDeadAction;

        private void Update()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            _direction = new Vector3(x, y, 0);
            
            if(Input.GetKeyDown(KeyCode.Space))
            {
                var bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
                bullet.Initialize(_scoreManager, _moveSpeed, Vector3.up);
            }
        }

        private void FixedUpdate()
        {
            _rb2d.velocity = _direction.normalized * _moveSpeed;
        }
        
        public void Initialize()
        {
            _uiManager = FindObjectOfType<UIManager>();
            _scoreManager = new ScoreManager();
            _scoreManager.OnChangeScoreAction += _uiManager.UpdateScoreText;
            _scoreManager.Initialize();
            _hpManager = new HpManager(10);
            _hpManager.OnChangeHpAction += _uiManager.UpdateHpText;
            _hpManager.Initialize();
            _moveSpeed = 5f;
        }
        
        public void TakeDamage(int damage)
        {
            _scoreManager.DecreaseScore(1000);
            
            if(_hpManager.TakeDamage(damage))
            {
                OnDeadAction?.Invoke();
                Destroy(gameObject);
            }
        }

        public void RecoveryPlayer(int recovery)
        {
            _hpManager.Recovery(recovery);
        }
    }    
}

