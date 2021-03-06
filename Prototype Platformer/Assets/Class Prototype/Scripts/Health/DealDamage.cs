﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour {

    [Tooltip("What tag does the recipient of the damage have? (Could be Player, Enemy, etc.")]
    public string targetTag = "Player";

    public int damageAmount = 10;

    [Tooltip("How long in seconds between when damage can be applied.")]
    public float damageTickRate = 0.5f;


    [Tooltip("Sends message to 'Function Name' on the game object that interacts with this collider.")]
    public string damageFunctionName = "DealDamage";

    [Header("Trigger Collider Only")]
    public bool continuousDamage = false;


    private bool _canDamage = true;

    public void OnTriggerEnter(Collider other)
    {
        if (!_canDamage) return;

        if (other.CompareTag(targetTag))
        {
            other.SendMessageUpwards(damageFunctionName, damageAmount, SendMessageOptions.DontRequireReceiver);
            _canDamage = false;
            StartCoroutine(ResetDamage());
        }
    }

    public void OnTriggerStay (Collider other)
    {
        if (!_canDamage) return;

        if (continuousDamage && other.CompareTag(targetTag))
        {
            other.SendMessageUpwards(damageFunctionName, damageAmount, SendMessageOptions.DontRequireReceiver);
            _canDamage = false;
            StartCoroutine(ResetDamage()); 
        }
    }

    public void OnCollisionEnter(Collision col)
    {
        if (!_canDamage) return;

        if (col.gameObject.CompareTag(targetTag))
        {
            col.gameObject.SendMessageUpwards(damageFunctionName, damageAmount, SendMessageOptions.DontRequireReceiver);
            _canDamage = false;
            StartCoroutine(ResetDamage());
        }
    }

    IEnumerator ResetDamage ()
    {
        yield return new WaitForSeconds(damageTickRate);

        _canDamage = true;
    }
}
