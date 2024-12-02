using UnityEngine;

namespace BadCode
{
    public class PlayerManager : MonoBehaviour
    {
        //変数はpublicにしない
        //基本privateで宣言して、必要な場合は[SerializeField]を使う
        //取得はgetter、設定はメソッド経由でおこなうとよい
        public GameManager _gameManager;
        public GameObject _bulletPrefab;
        //初期値はAwake()/Start()/フィールドどこで設定するか決めておくこと
        private int _hp = 10;
        //変な略語は使わない
        private float _s;
        
        void Start()
        {
           _s = 0.01f;
        }
    
        // Update is called once per frame
        void Update()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            //ベクトルはノーマライズをわすれないこと
            var direction = new Vector3(x, y,0);
            //定数は変数にしておく
            //Update内で座標を加算するならdeltaTimeをつけること
            transform.position += direction *  0.01f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
            }
            
            //Update内でFindは重いのでStartで取得しておく
            //string型の比較はミスが起こりやすいのでNG
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            //HPが減るのはDamage内なのでそっちにやられた判定は持たせるべき
            if(_hp <= 0)
            {
                _gameManager.GameOver();
                Destroy(gameObject);
            }
        }
    
        public void Damage(int a)
        {
            //負の値が入るといけないので早期リターンをおこなう
            _hp -= a;
        }
    }   
}
