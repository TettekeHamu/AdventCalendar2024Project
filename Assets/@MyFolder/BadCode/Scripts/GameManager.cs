using System.Collections;
using UnityEngine;

namespace BadCode
{
    public class GameManager : MonoBehaviour
    {
        //変数はpublicにしない
        //基本privateで宣言して、必要な場合は[SerializeField]を使う
        //取得はgetter、設定はメソッド経由でおこなうとよい
        //Prefabであることを明記する
        public GameObject _player;
        public GameObject _enemy;
        public GameObject[] _enemyCreatPoints;
        //privateを明記する
        //命名ルールは決めておくこと
        int score;
    
        void Start()
        {
            score = 0;
            Instantiate(_player, transform.position, Quaternion.identity);
            StartCoroutine(EnemyCreateCo());
        }
    

        public IEnumerator EnemyCreateCo()
        {
            while (true)
            {
                var n = Random.Range(0, _enemyCreatPoints.Length);
                Instantiate(_enemy, _enemyCreatPoints[n].transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.25f);
            }
        }
    
        public void GameOver()
        {
            Debug.Log("GameOver");
        }
    
        public void AddScore(int a)
        {
            //負の値が入るといけないので早期リターンをおこなう
            score += a;
            Debug.Log("Score is " + score);
        }
    }   
}
