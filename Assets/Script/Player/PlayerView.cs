using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Movility))]
[RequireComponent(typeof(torreta))]
public class PlayerView : NetworkBehaviour
{    
    [SerializeField] private ParticleSystem _shootParticle;

    private NetworkMecanimAnimator _networkMecanimAnimator;

    public override void Spawned()
    {
        _networkMecanimAnimator = GetComponentInChildren<NetworkMecanimAnimator>();

        var p = GetComponent<Player>();
        var m = GetComponent<Movility>();
        var t = GetComponent<torreta>();

        t.OnShoot += TriggerShootParticles;

        m.OnMove += MoveAnimation;
    }

    private void TriggerShootParticles()
    {
        _networkMecanimAnimator.Animator.SetBool("IsShooting", true);
        _shootParticle.Play();
    }

    private void MoveAnimation(float xAxis)
    {
        _networkMecanimAnimator.Animator.SetFloat("xAxis", Mathf.Abs(xAxis));
    }    
}
