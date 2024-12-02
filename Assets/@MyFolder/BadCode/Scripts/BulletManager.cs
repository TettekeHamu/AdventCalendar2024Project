using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BadCode
{
    public class BulletManager : MonoBehaviour
    {
        //ローマ字でもいいがプロジェクトで統一すること
        public GameObject _kaihukuItem;
        //命名ルールも統一すること
        public GameManager gm;
        
        void Start()
        {
            StartCoroutine(DestroyCo());
        }
        
        void Update()
        {
            //Update内でFindは重いのでStartで取得しておく
            //string型の比較はミスが起こりやすいのでNG
            gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            //0.02fみたいな定数は変数にしておく
            transform.position += new Vector3(0, 1, 0)  * 0.02f;
        }
    
        public IEnumerator DestroyCo()
        {
            yield return new WaitForSeconds(10f);
            Destroy(gameObject);
        }
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            //string型の比較はミスが起こりやすいので型で判別
            //tagも同様の理由でNG
            if(other.gameObject.name == "Enemy(Clone)")
            {
                gm.AddScore(100);
                var e = other.gameObject.GetComponent<EnemyManager>();
                Destroy(e.gameObject);
                //弾が爆発したらアイテム生成で本当に大丈夫？
                //敵側に持たせたい
                var n = Random.Range(0, 5);
                if (n == 0)
                {
                    Debug.Log("回復アイテムを生成");
                    Instantiate(_kaihukuItem, other.transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }
    }   
}
