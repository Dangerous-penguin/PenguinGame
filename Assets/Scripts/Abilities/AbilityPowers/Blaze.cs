using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DangerousPenguin.Abilities.AbilityPowers
{

public class Blaze : MonoBehaviour
{
    [SerializeField] private Transform pivot;
    [SerializeField] private float     duration   = 5.0f;
    [SerializeField] private float     damageTick = 0.5f;
    [SerializeField] private float     damage     = 3f;

    [HideInInspector] public Transform player;

    private Vector3      _startingPosition;
    private float        _startTime;
    private float        _lastTick;
    private List<Health> unitsToAttack = new();

    private void Start()
    {
        _startingPosition = transform.position;
        _startTime        = Time.time;
        _lastTick         = 0;
    }

    private void Update()
    {
        if (Time.time - _startTime < 1.0f)
        {
            var scale = pivot.localScale;
            scale.z          = (player.transform.position - _startingPosition).magnitude;
            pivot.localScale = scale;
        }

        //stop damage after 5s
        if (Time.time - _startTime < duration)
        {
            if (unitsToAttack.Any())
            {
                foreach (var unit in unitsToAttack)
                {
                    unit.TakeDamage(damage);
                }

                unitsToAttack.Clear();
                _lastTick = Time.time;
            }
        }

        if (Time.time - _startTime > duration) Destroy(gameObject, 2.0f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!(Time.time - _lastTick > damageTick)) return;
        if (other.CompareTag("Player")) return;
        
        var unit = other.gameObject.GetComponent<Health>();
        if(unit) unitsToAttack.Add(unit);
    }
}

}