using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySciptableObject : ScriptableObject, ISpawnableEnemy
{

    [SerializeField]
    protected int health;
    public int Health { get { return health; } }

    [SerializeField]
    protected GameObject prefab;
    public GameObject Prefab { get { return prefab; } }



    [SerializeField]
    protected int damage;
    public int Damage { get { return damage; } }

    public int ChallengeRating
    {
        get
        {
            return health / 10 + 2 * damage;
        }
    }

    public GameObject Spawn(Vector3 position)
    {
        GameObject gameObject = Instantiate(prefab, position, Prefab.transform.rotation);
        BaseHealth health = gameObject.GetComponent<BaseHealth>();
        health.ResetWithMaxHealth(Health);
        return gameObject;
    }
}
