using System;
using System.Collections.Generic;
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
        private readonly List<BulletManager> _bullets = new List<BulletManager>();
        private UIManager _uiManager;
        private ScoreManager _scoreManager;
        private HpManager _hpManager;
        private float _moveSpeed;
        private Vector3 _direction;
        private event Action _onDeadAction;
        
        private void DestroyPlayer()
        {
            _onDeadAction = null;
            Destroy(gameObject);
        }
        
        public void Initialize(GameManager gameManager)
        {
            _uiManager = FindObjectOfType<UIManager>();
            _scoreManager = new ScoreManager();
            _scoreManager.Initialize(_uiManager);
            _hpManager = new HpManager(DefineValue.InitialHp);
            _hpManager.Initialize(_uiManager);
            _moveSpeed = 5f;
            _onDeadAction += gameManager.GameOver;
        }
        
        public void MyUpdate()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            _direction = new Vector3(x, y, 0);
            
            if(Input.GetKeyDown(KeyCode.Space))
            {
                var bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
                bullet.Initialize(this, _scoreManager, _moveSpeed, Vector3.up);
                _bullets.Add(bullet);
            }
        }

        public void MyFixedUpdate()
        {
            _rb2d.velocity = _direction.normalized * _moveSpeed;
            foreach (var bullet in _bullets)
            {
                bullet.MyFixedUpdate();
            }
        }
        
        public void RemoveBullet(BulletManager bullet)
        {
            _bullets.Remove(bullet);
        }
        
        public void TakeDamage(int damage)
        {
            _scoreManager.DecreaseScore(DefineValue.DamageScore);
            
            if(_hpManager.TakeDamage(damage))
            {
                _onDeadAction?.Invoke();
                DestroyPlayer();
            }
        }

        public void RecoveryPlayer(int recovery)
        {
            _hpManager.Recovery(recovery);
        }
    }    
}

