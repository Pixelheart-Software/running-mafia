using Enemy;
using UnityEngine;

namespace Player
{
    public class PunchPointController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<EnemyController>().ReceivePunch();
            }
        }
    }
}
