using System.Collections;
using System.Collections.Generic;
using Testing;
using UnityEngine;

[TestMonoBehaviour]
public class Projectile : MonoBehaviour, IProjectile
{

    public Vector3 TargetPosition
    {
        set
        {
            targetDirection = (value - transform.position).normalized * PROJECTILE_SPEED; 
        }
    }

    public Vector3 TargetDirection
    {
        set
        {
            targetDirection = value.normalized * PROJECTILE_SPEED;
        }
    }

    public const float PROJECTILE_SPEED = 10f;
    public const int PROJECTILE_DAMAGE = 15;

    protected Vector3 targetDirection = Vector3.zero;
    protected float aliveTime = 0;

    protected const float MAX_ALIVE_TIME = 5.0f;

    protected void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Hit " + other.gameObject.name);
        Destroy(gameObject);
        PlayerHealth health = other.gameObject.GetComponent<PlayerHealth>();
        if (health != null)
        {
            OnHealthHit(health);
        }
    }

    public void OnHealthHit(BaseHealth health)
    {
        health.TakeDamage(PROJECTILE_DAMAGE);
    }

    public void Move()
    {
        transform.position += targetDirection * Time.deltaTime;
    }

    void FixedUpdate()
    {
        aliveTime += Time.deltaTime;
        if (aliveTime > MAX_ALIVE_TIME)
            Destroy(gameObject);

        Move();
    }

    #region Tests

    [TestEnumerator]
    public IEnumerator TestSpeed() // fails; probably because not in intial scene
    {
        var positionBefore = transform.position;
        yield return new WaitForSeconds(1f);
        var positionAfter = transform.position;

        var distanceTravelled = Vector3.Distance(positionBefore, positionAfter);
        Assert.ApproxEqual(distanceTravelled, PROJECTILE_SPEED);
    }

    [Test]
    public void TestDamage()
    {
        var health = TestPipeline.CreateNewInstanceOf<PlayerHealth>();
        health.currentHealth = health.maxHealth;

        var healthBefore = health.currentHealth;
        OnHealthHit(health);
        var healthAfter = health.currentHealth;

        Assert.AreEqual(healthBefore - healthAfter == PROJECTILE_DAMAGE, true);
    }

    #endregion
}