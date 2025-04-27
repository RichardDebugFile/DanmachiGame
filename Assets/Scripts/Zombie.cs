using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public GameObject BulletPrefab;
    public GameObject Murcielago;

    private float LasShoot;
    private int Health = 3;

    private void Update()
    {
        if (Murcielago == null) return;

        Vector3 direction = Murcielago.transform.position - transform.position;

        // Voltear sprite según posición del jugador
        if (direction.x >= 0.0f)
            transform.localScale = new Vector3(16.0f, 16.0f, 16.0f);
        else
            transform.localScale = new Vector3(-16.0f, 16.0f, 16.0f);

        float distance = Mathf.Abs(Murcielago.transform.position.x - transform.position.x);

        if (distance < 4.0f && Time.time > LasShoot + 1.0f)
        {
            Shoot();
            LasShoot = Time.time;
        }
    }

    private void Shoot()
    {
        Vector3 direction;

        // Revisar hacia qué lado está mirando
        if (transform.localScale.x > 0)
            direction = Vector3.right;
        else
            direction = Vector3.left;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.4f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    public void Hit()
    {
        Health -= 1;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Detectar golpe de espada
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Espada"))
        {
            Hit();
        }
    }
}
