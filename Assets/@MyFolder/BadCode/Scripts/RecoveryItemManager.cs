using System.Collections;
using UnityEngine;

namespace BadCode
{
    public class RecoveryItemManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(DestroyCo());
        }

        // Update is called once per frame
        void Update()
        {
            //0.01fみたいな定数は変数にしておく
            transform.position += new Vector3(0, -1, 0) * 0.01f;
        }
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name == "Player(Clone)")
            {
                var p = other.gameObject.GetComponent<PlayerManager>();
                //回復アイテムなので-1はNG
                //別途回復用のメソッドを作成する
                p.Damage(-1);
                Destroy(gameObject);
            }
        }

        public IEnumerator DestroyCo()
        {
            yield return new WaitForSeconds(10f);
            Destroy(gameObject);
        }
    }   
}
