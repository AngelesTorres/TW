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
        if (!HasStateAuthority)
            return;

        _networkMecanimAnimator = GetComponentInChildren<NetworkMecanimAnimator>();

        var m = GetComponent<Movility>();
        var t = GetComponent<torreta>();

        t.OnShoot += TriggerShootParticles;

        m.OnMove += MoveAnimation;
    }

    void Update()
    {
        if (!HasStateAuthority)
            return;
    }

    private void TriggerShootParticles()
    {
        _networkMecanimAnimator.Animator.SetBool("IsShooting", true);
        _shootParticle.Play();
    }

    private void MoveAnimation(float zAxis)
    {
        _networkMecanimAnimator.Animator.SetFloat("zAxis", Mathf.Abs(zAxis));
    }    
}
