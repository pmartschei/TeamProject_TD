using UnityEngine;
using System.Collections;
using Assets.Scripts.GameLogic.TowerSystem;

public class IceTowerScript : TowerScript
{

    public float m_MovementMultiplier = 0.9f;
    /// <summary>
    /// Schießt auf ein Target sofern der Turm keinen Cooldown besitzt
    /// </summary>
    /// <param name="target">Gameobject das angezielt werden soll </param>
    /// <returns>True wenn geschossen wurde oder nicht mehr weiter geschossen werden soll, False wenn schiessen nicht möglich.</returns>
    public IceTowerScript()
    {
    }
    
    public override void Start()
    {
        RecalculateAttributes();
    }
    public override bool Shoot(EnemyScript target)
    {
        target.DoDamage(m_Damage * 1 / m_Cooldown * Time.deltaTime);
        target.m_AdditionalSpeedMultiplier = m_MovementMultiplier;
        return false;
    }
    public override void RecalculateAttributes()
    {
        base.RecalculateAttributes();
        ParticleSystem ps = this.transform.Find("IceParticels").GetComponent<ParticleSystem>();
        if (ps == null) return;
        ParticleSystem.ShapeModule sm = ps.shape;
        sm.radius = this.m_Radius;
        ps.maxParticles = (int)(60 * this.m_Radius);
        ParticleSystem.EmissionModule em = ps.emission;
        em.rate = (int)(60 * this.m_Radius / 5);
    }
}
