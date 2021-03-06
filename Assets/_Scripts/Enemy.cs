﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("Set in Inspector: Enemy")]
    public float speed = 10f;
    public float fireRate = .3f;
    public float health = 10;
    public int score = 100;

    public float showDamageDuration = 0.1f;
    public float powerUpDropChance = 1f;

    [Header("Set Dynamically: Enemy")]
    public Color[] originalColors;
    public Material[] materials;
    public bool showingDamage = false;
    public float damageDomeTime;
    public bool notifiedOfDestruction = false;

    protected BoundsCheck bndCheck;

    void Awake()    {

        bndCheck = GetComponent<BoundsCheck>();

        materials = Utils.GetAllMaterials(gameObject);
        originalColors = new Color[materials.Length];
        for (int i = 0; i < materials.Length; i++)        {
            originalColors[i] = materials[i].color;
        }

    }

    public Vector3 pos    {
        get        {
            return (this.transform.position);
        }
        set        {
            this.transform.position = value;
        }
    }

	
	
	// Update is called once per frame
	void Update () {
        Move();

        if( showingDamage && Time.time > damageDomeTime)        {
            UnShowDamage();
        }

        if (bndCheck != null && bndCheck.offDown) {
            
            Destroy(gameObject);
        
        }
	}

    public virtual void Move()    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    void OnCollisionEnter (Collision coll)    {
        Debug.Log("in on collision enter");
        GameObject otherGO = coll.gameObject;
        switch (otherGO.tag)        {
            case "ProjectileHero":
                Projectile p = otherGO.GetComponent<Projectile>();
                if (!bndCheck.isOnScreen)                {
                    Destroy(otherGO);
                    break;
                }
            health -= Main.GetWeaponDefinition(p.type).damageOnHit;
                ShowDamage();

                if (health <= 0)                {
                    Debug.Log("calling main ship destroyed");
                    if (!notifiedOfDestruction)
                    {
                        
                        Main.S.shipDestroyed(this);
                    }
                    notifiedOfDestruction = true;
                    Destroy(this.gameObject);
                }
                Destroy(otherGO);
                break;

            default:
                print("Enemy hit by non-ProjectileHero: " + otherGO.tag);
                break;
        }



    /*    if (otherGO.tag =="ProjectileHero")        {
            Destroy(otherGO);
            Destroy(gameObject);
        } else        {
            print("Enemy hit by non-ProjectileHero: " + otherGO.name);
        }
*/
    }

    void ShowDamage ()    {
        foreach (Material m in materials)         {
            m.color = Color.red;
        }
        showingDamage = true;
        damageDomeTime = Time.time + showDamageDuration;
    }

    void UnShowDamage ()    {
        for (int i=0; i < materials.Length; i++)        {
            materials[i].color = originalColors[i];
        }
        showingDamage = false;
    }

}
