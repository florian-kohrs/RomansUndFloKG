using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Behaviour controller of a ranged weapon
/// </summary>
public class EquippedRangedWeapon : EquippedWeapon<EquippedRangedWeapon, InventoryRangedWeapon>
{
    public GameObject projectilePrefab;
    public float force = 10;
    public Transform shotPoint;
    public float xArrowScale = (float)0.95578;
    public float yArrowScale = (float)0.01943;
    public float zArrowScale = (float)0.00069;

    private void Update()
    {
        Vector3 weaponPosition = transform.position;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.LookAt(worldPosition);
        transform.Rotate(0, -90, 0);


        //Vector3 direction = worldPosition - weaponPosition;
        //Debug.Log($"pos:{worldPosition}");
        ////Debug.Log($"viewport:{Camera.main.ViewportToScreenPoint(mousePosition)}");
        //transform.forward = direction;
    }

    void InstantiateArrow()
    {
        GameObject newArrow = Instantiate(projectilePrefab, shotPoint.position, shotPoint.rotation, transform);
        newArrow.transform.localScale = new Vector3(xArrowScale, yArrowScale, zArrowScale);
        Rigidbody r = newArrow.GetComponent<Rigidbody>();
        r.isKinematic = false;
        r.velocity = transform.right * force;
    }

    void Shoot()
    {
        if (!canAttack)
            return;

        InstantiateArrow();
        canAttack = false;
        this.DoDelayed(attackCooldown, () => canAttack = true);
    }


    protected float attackCooldown = 1;

    protected bool canAttack = true;

    public void Attack(Func<IHealth, bool> healthDamageFilter)
    {
        this.healthDamageFilter = healthDamageFilter;
        Shoot();
    }

    /// <summary>
    /// when the projectile of a ranged weapon encounters a collision with an entitiy which has IHealth attached to any of its scripts
    /// this function will determine if the entity will get damage on contact
    /// </summary>
    protected Func<IHealth, bool> healthDamageFilter;


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Hit " + other.gameObject.name);
        Destroy(gameObject);
        EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();
        if (health != null && healthDamageFilter(health))
        {
            OnEnemyHit(health);
        }
    }

    private void OnEnemyHit(EnemyHealth health)
    {
        health.TakeDamage(weapon.Damage);
    }
}