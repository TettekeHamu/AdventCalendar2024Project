using UnityEngine;

namespace BadCode
{
    public class EnemyManager : MonoBehaviour
    {
        float _speed;
        GameObject _player;
        public int _counter;
        private GameManager _gameManager;
        
        // Start is called before the first frame update
        void Start()
        {
            _speed = 0.01f;
        }
    
        // Update is called once per frame
        void Update()
        {
            //Update内でFindは重いのでStartで取得しておく
            //string型の比較はミスが起こりやすいのでNG
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            _player = GameObject.Find("Player(Clone)");
            
            _counter++;
            
            //出来れば略さない
            var d = _player.transform.position - transform.position;
            var a = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
            if (_counter < 60)
            {
                transform.rotation = Quaternion.Euler(0, 0, a - 90);
            }
            else
            {
                //elseの中身が何もないときは書かないこと
                //特別な理由がある際はちゃんとWhyを残すこと
            }
            //Update内で座標を加算するならdeltaTimeをつけること
            transform.position += transform.up * _speed;
        }
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            //string型の比較はミスが起こりやすいので型で判別
            //tagも同様の理由でNG
            if (other.gameObject.name == "Player(Clone)")
            {
                var pm = other.gameObject.GetComponent<PlayerManager>();
                pm.Damage(1);
                //Addなのに-1000はNG
                _gameManager.AddScore(-1000);
                Destroy(gameObject);
            }
        }
    }   
}
