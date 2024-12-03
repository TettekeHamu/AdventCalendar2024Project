using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GoodCode
{
    /// <summary>
    /// ゲーム全体の管理クラス
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerManager _playerPrefab;
        [SerializeField] private EnemyManager _enemyPrefab;
        [SerializeField] private EnemyCreatePoint[] _enemyCreatePoints;
        private PlayerManager _player;
        private readonly List<EnemyManager> _enemies = new List<EnemyManager>(); 
        private Coroutine _enemyCreateCoroutine;
            
        private void Awake()
        {
            Application.targetFrameRate = 60; 
        }

        private void Start()
        {
            _player = Instantiate(_playerPrefab);
            _player.Initialize(this);
            _enemyCreateCoroutine = StartCoroutine(EnemyCreateCoroutine());
        }
        
        private void Update()
        {
            if (_player == null) return;
            _player.MyUpdate();
            foreach (var enemy in _enemies)
            {
                enemy.MyUpdate();
            }
        }

        private void FixedUpdate()
        {
            if (_player == null) return;
            _player.MyFixedUpdate();
            foreach (var enemy in _enemies)
            {
                enemy.MyFixedUpdate();
            }
        }

        private IEnumerator EnemyCreateCoroutine()
        {
            while (true)
            {
                var random = Random.Range(0, _enemyCreatePoints.Length);
                var enemy = Instantiate(_enemyPrefab, _enemyCreatePoints[random].transform.position, Quaternion.identity);
                enemy.Initialize(this);
                _enemies.Add(enemy);
                yield return new WaitForSeconds(0.5f);
            }
        }
        
        public void RemoveEnemy(EnemyManager enemy)
        {
            _enemies.Remove(enemy);
        }
        
        public void GameOver()
        {
            StopCoroutine(_enemyCreateCoroutine);
            Debug.Log("ゲームオーバー");
        }
    }   
}
