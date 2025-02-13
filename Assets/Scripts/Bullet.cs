using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float t = 3;
    [SerializeField] int _damage = 1;
    public int Damage => _damage;

    private void Start()
    {
        Destroy(gameObject, t);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
