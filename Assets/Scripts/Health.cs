using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int _hp = 5;
    [SerializeField] GameObject _deathEffect;

    private void OnCollisionEnter(Collision collision)
    {
        _hp -= collision.gameObject.GetComponent<Bullet>().Damage;
        if (_hp <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        if (_deathEffect) Instantiate(_deathEffect, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }
}
