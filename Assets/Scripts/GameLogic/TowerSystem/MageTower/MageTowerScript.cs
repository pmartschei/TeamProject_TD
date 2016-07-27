using UnityEngine;
using System.Collections;
using Assets.Scripts.GameLogic.TowerSystem;

public class MageTowerScript : TowerScript
{

    public float m_travelTime;
    public GameObject m_bulletSpawn;

    /// <summary>
    /// Schießt auf ein Target sofern der Turm keinen Cooldown besitzt
    /// </summary>
    /// <param name="target">Gameobject das angezielt werden soll </param>
    /// <returns>True wenn geschossen wurde oder nicht mehr weiter geschossen werden soll, False wenn schiessen nicht möglich.</returns>
    public override bool Shoot(EnemyScript target)
    {
        Vector3 start = this.gameObject.transform.position + m_bulletSpawn.transform.localPosition;//start vom tower+bulletSpawn
        Object bullet = Instantiate(m_Bullet,start,Quaternion.Euler(Vector3.zero));//neue Bullet erstellen
        if (!(bullet is GameObject)) return true;//wenn falsches Objekt, eig unmöglich
        MageBulletScript mageBullet = ((GameObject)bullet).GetComponent<MageBulletScript>();
        if (mageBullet == null) return true;//wenn kein Script vorhanden
        if (!CanShoot()) return false;//wenn nicht geschossen werden kann
        Reload();
        mageBullet.m_startPosition = start;
        mageBullet.m_travelTime = m_travelTime;//werte setzen von dem script
        mageBullet.m_target = target;
        mageBullet.m_damage = Damage;
        return true;
    }
}
