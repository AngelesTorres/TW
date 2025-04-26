using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

[RequireComponent(typeof(Player))]
public class PlayerView : NetworkBehaviour
{
    [SerializeField] private ParticleSystem _shootParticle;

    private NetworkMecanimAnimator _networkMecanimAnimator;

    public override void Spawned()
    {
        _networkMecanimAnimator = GetComponentInChildren<NetworkMecanimAnimator>();

        var p = GetComponent<Player>();

        p.OnShoot += TriggerShootParticles;

        p.OnMove += MoveAnimation;
    }

    private void TriggerShootParticles()
    {
        _shootParticle.Play();
    }

    private void MoveAnimation(float xAxis)
    {
        _networkMecanimAnimator.Animator.SetFloat("xAxis", Mathf.Abs(xAxis));
    }
}
