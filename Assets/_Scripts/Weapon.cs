﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType{
    none,
    blaster,
    spread,
    phaser,
    missle,
    laser,
    shield
}

[System.Serializable]

public class WeaponDefinition {
    public WeaponType typ = WeaponType.none;
    public string letter;
    public Color color = Color.white;
    public GameObject projectilePrefab;
    public Color projectileColor = Color.white;
    public float damageOnHit = 0;
    public float continuousDamage = 0;
    public float delayBetweenShots = 0;
    public float velocity = 20;
}

public class Weapon : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
