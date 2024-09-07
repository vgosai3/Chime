using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Enemy class is an abstract class that all enemies
/// should inherit. It includes implementations for HP,
/// taking damage, and attacking, some of which can be
/// overridden.
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
    /// The MaxHitPoints variable indicates the max amount
    /// of HP the enemy can have, including healing.
    /// </summary>
    protected float MaxHitPoints = 100.0f;
    [SerializeField]
    /// <summary>
    /// The DamageReduction variable defines the amount of
    /// damage the enemy takes when the attack type is a mismatch.
    /// </summary>
    protected float DamageReduction = 0.5f;

    [Header("Type Settings")]
    [SerializeField]
    /// <summary>
    /// The enemy type. This will be used to account for damage reduction.
    /// By default, the type is set to None.
    /// </summary>
    protected Type EnemyType = Type.None;


    [Header("Attack/Damage Settings")]
    [SerializeField]
    ///<summary>
    /// The GameObject used to generate damage text that appears
    /// when the enemy takes damage.
    ///</summary>
    protected GameObject DamageText;
    [SerializeField]
    ///<summary>
    /// The distance from the enemy to the player at which the enemy will try to attack.
    ///</summary>
    protected float AttackRadius = 1.0f;
    [SerializeField]
    /// <summary>
    /// The speed at which the enemy will try to attack, in seconds per attack.
    /// </summary>
    protected float AttackSpeed = 1.0f;

    /// <summary>
    /// The Player object.
    /// </summary>
    protected GameObject _Player;

    /// <summary>
    /// The last time the enemy has attacked.
    /// </summary>
    private float _LastAttackTime;

    /// <summary>
    /// The Start function is called before the first frame update. We will use this to 
    /// </summary>
    void Start()
    {
        // Try to find the player object by the tag - we are using GameObject.FindWithTag
        // instead of GameObject.Find for efficiency
        _Player = GameObject.FindWithTag("Player");
    }

    /// <summary>
    /// The LateUpdate method is called after the Update method. In this case, we will
    /// use it to check if the player is within the attack radius, and attack at the specified
    /// attack speed.
    /// Use the Update method within all concrete implementations for movement, etc.
    /// </summary>
    private void LateUpdate()
    {
        // Check if Player is null; if it is, the Player is not found in the current scene
        if (_Player != null)
        {
            // Get the distance from the enemy to the player
            float dist = Vector3.Distance(_Player.transform.position, transform.position);
            //print(dist < AttackRadius);
            // If the distance is less than the attack radius
            if (dist < AttackRadius)
            {
                // If the last attack time is greater than the attack speed, attack
                if (Time.time - _LastAttackTime >= AttackSpeed)
                {
                    Attack();
                    _LastAttackTime = Time.time;
                }
            }
        }
        // Try to find the Player if it was not found previously
        else
        {
            _Player = GameObject.FindWithTag("Player");
        }
    }

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
    public void TakeDamage(float damage, Type enemyType)
    {
        // Check enemy type
        if (EnemyType == Type.None || enemyType.Equals(EnemyType))
        {
            // If attack type match, deal full damage
            HitPoints -= damage;
            GenerateDamageText(damage);
        }
        // Otherwise calculate reduced damage
        else
        {
            var reducedDamage = damage * DamageReduction;
            HitPoints -= reducedDamage;
            GenerateDamageText(reducedDamage);
        }
        // Remove enemy if HP is less than 0
        if (HitPoints <= 0)
        {
            Kill();
        }
    }
    /// <summary>
    /// The GenerateDamageText function generates a floating damage text over the enemy object when the enemy takes damage.
    /// </summary>
    /// <param name="damage">The amount of damage taken.</param>
    #warning Add additional damage text types for reduced damage and for finishing damage (damage that results in HP < 0)
    private void GenerateDamageText(float damage)
    {
        // Create new damage text object above enemy position
        GameObject damageText = Instantiate(DamageText, transform.position + Vector3.up * 3, Quaternion.identity);
        // Set damage number
        DamageNumber damageNumber = damageText.GetComponent<DamageNumber>();
        if (damageNumber != null)
        {
            damageNumber.Damage = damage;
        }
    }

    /// <summary>
    /// The Attack method is called when the enemy is in the radius of the player.
    /// </summary>
    protected abstract void Attack();

    /// <summary>
    /// The Kill function is called when Enemy HP
    /// is less than or equal to 0.
    /// The virtual keyword indicates that this
    /// function can be overriden.
    /// </summary>
    protected virtual void Kill()
    {
        Destroy(gameObject);
    }
}
