using UnityEngine;

namespace GoodCode
{
    /// <summary>
    /// 回復アイテム
    /// </summary>
    public class RecoveryItemManager : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb2d;
        private float _moveSpeed;

        private void Awake()
        {
            _moveSpeed = 2;
        }

        private void FixedUpdate()
        {
            _rb2d.velocity = Vector2.down * _moveSpeed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ICollisionItem collision))
            {
                collision.RecoveryPlayer(DefineValue.Recovery);
                Destroy(gameObject);
            }
        }
    }   
}
