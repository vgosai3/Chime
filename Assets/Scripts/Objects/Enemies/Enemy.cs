using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Enemy class is an abstract class that all enemies
/// should inherit.
/// </summary>
public abstract class Enemy : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField]
    /// <summary>
    /// The HitPoints variable indicates how much HP a
    /// newly instantiated instance of the Enemy should
    /// have.
    /// </summary>
    protected float HitPoints = 100.0f;
    [SerializeField]
    /// <summary>
    /// The MaxHitPoints variable indicates
    /// </summary>
    protected float MaxHitPoints = 100.0f;

    [Header("Type Settings")]
    [SerializeField]
    protected string EnemyType = "None";


    [Header("Damage Settings")]
    [SerializeField]
    protected float DamageReduction = 0.5f;
    [SerializeField]
    protected GameObject DamageText;

    /// <summary>
    /// Heals the Enemy for the specified amount.
    /// If the resulting HP is larger than the max
    /// HP, then set HP to max HP.
    /// </summary>
    /// <param name="hitPoints">Amount of HP healed.</param>
    public void Heal(int hitPoints)
    {
        HitPoints += hitPoints;
        if (HitPoints > MaxHitPoints)
        {
            HitPoints = MaxHitPoints;
        }
    }

    /// <summary>
    /// Take damage for the specified amount. If
    /// the resulting HP is less than or equal to
    /// 0, then start the kill sequence for the
    /// Enemy.
    /// </summary>
    /// <param name="damage">Amount of damage dealt.</param>
    public void TakeDamage(float damage, string enemyType)
    {
        if (EnemyType.Equals("None") || enemyType.Equals(EnemyType))
        {
            HitPoints -= damage;
            GenerateDamageText(damage);
        }
        else
        {
            var reducedDamage = damage * DamageReduction;
            HitPoints -= reducedDamage;
            GenerateDamageText(reducedDamage);
        }
        if (HitPoints <= 0)
        {
            Kill();
        }
    }

    private void GenerateDamageText(float damage)
    {
        GameObject damageText = Instantiate(DamageText, transform.position + Vector3.up * 3, Quaternion.identity);
        DamageNumber damageNumber = damageText.GetComponent<DamageNumber>();
        if (damageNumber != null)
        {
            damageNumber.Damage = damage;
        }
    }

    /// <summary>
    /// The Kill function is called when Enemy HP
    /// is less than or equal to 0. By default, the
    /// function destroys the instance, but can be
    /// overriden to include animations as well.
    /// </summary>
    protected virtual void Kill()
    {
        Destroy(gameObject);
    }
}
