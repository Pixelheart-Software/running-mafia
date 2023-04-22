using UnityEngine;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        private readonly int _attackTriggerAnimatorHash = Animator.StringToHash("AttackTrigger");

        private Animator _animator;

        private bool _dead;

        void Awake()
        {
            _animator = GetComponent<Animator>();

            foreach (var rb in GetComponentsInChildren<Rigidbody>())
            {
                rb.isKinematic = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _animator.SetTrigger(_attackTriggerAnimatorHash);
            }
        }

        public bool IsDead()
        {
            return _dead;
        }

        public void ReceivePunch()
        {
            _animator.enabled = false;
            foreach (var rb in GetComponentsInChildren<Rigidbody>())
            {
                rb.isKinematic = false;
            }
            _dead = true;
        }

        public void DoAttack(Collider other)
        {
            _animator.SetTrigger(_attackTriggerAnimatorHash);
        }
    }
}

