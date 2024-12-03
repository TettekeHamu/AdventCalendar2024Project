using System;
using System.Collections;
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

        private void Awake()
        {
            Application.targetFrameRate = 60; 
        }

        private void Start()
        {
            var player = Instantiate(_playerPrefab);
            player.Initialize();
            player.OnDeadAction += GameOver;
            StartCoroutine(EnemyCreateCoroutine());
        }
        
        private void GameOver()
        {
            Debug.Log("ゲームオーバー");
        }
        
        private IEnumerator EnemyCreateCoroutine()
        {
            while (true)
            {
                var random = Random.Range(0, _enemyCreatePoints.Length);
                Instantiate(_enemyPrefab, _enemyCreatePoints[random].transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }   
}
