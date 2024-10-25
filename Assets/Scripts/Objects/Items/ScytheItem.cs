using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScytheItem : AItem
{
    public GameObject Scythe;
    private GameObject currentScythe;

    GameObject _Player;

    public float AttackRadius = 0.2f;
    public override void PrimaryAction()
    {
        Debug.Log("Primary action called");
        _Player = GameObject.FindWithTag("Player");
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (Vector3.Distance(_Player.transform.position, enemy.transform.position) < AttackRadius)
            {
                enemy.GetComponent<Enemy>().TakeDamage(20, Type.None);
            }
        }
    }

    public override void Equip()
    {
        base.Equip();
        _Player = GameObject.FindWithTag("Player");
        if (currentScythe != null)
        {
            Debug.Log("Equip existing scythe");
            currentScythe.SetActive(true);
        }
        else
        {
            Debug.Log("Make new scythe");
            currentScythe = Instantiate(Scythe, _Player.transform.position, _Player.transform.rotation * Quaternion.Euler(0, 0, -90));
            currentScythe.transform.parent = _Player.transform;
        }
    }

    public override void Unequip()
    {
        base.Unequip();
        if (currentScythe != null)
        {
            currentScythe.SetActive(false);
        }
    }
}
