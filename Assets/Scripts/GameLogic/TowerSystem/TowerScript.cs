using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.GameLogic.TowerSystem
{
    public abstract class TowerScript : MonoBehaviour
    {
        public int m_Lvl = 1;
        public float m_BaseDamage;
        public float m_BaseCooldown;
        public float m_BaseRadius;
        public List<int> m_UpdateIndices;
        private float m_Damage;
        private float m_Radius;
        private float m_Cooldown;
        private float m_CurrentCooldown;
        public GameObject m_Bullet;

        public virtual void Start()
        {
            m_Damage = m_BaseDamage;
            m_Radius = m_BaseRadius;
            m_Cooldown = m_BaseCooldown;
        }

        public abstract bool Shoot(EnemyScript target);

        public virtual void Update()
        {
            //cooldown abziehen anhand der zeit
            m_CurrentCooldown = Math.Max(m_CurrentCooldown - Time.deltaTime, 0.0f);
        }

        public Upgrade[] GetNextUpgrades()
        {
            UpgradeSlot baseSlot = FindObjectOfType<TowerSystemScript>().GetUpgradeSlotFor(this.GetType());
            Upgrade[] nextUpgrades = new Upgrade[baseSlot.m_FollowingUpgrades.Length];
            for (int i=0;i<baseSlot.m_FollowingUpgrades.Length;i++)
            {
                nextUpgrades[i] = baseSlot.m_FollowingUpgrades[i].m_Upgrade;
            }
            return nextUpgrades;
        }

        public void AddUpgrade(int index){
            m_UpdateIndices.Add(index);
            RecalculateAttributes();
        }

        private void RecalculateAttributes()
        {
            float currentDamage = m_BaseDamage;
            float currentCooldown = m_BaseCooldown;
            float currentRadius = m_BaseRadius;
            UpgradeSlot baseSlot = FindObjectOfType<TowerSystemScript>().GetUpgradeSlotFor(this.GetType());
            foreach (int updateIndex in m_UpdateIndices)
            {
                Upgrade upgrade = baseSlot.m_Upgrade;
                if (upgrade != null)
                {
                    ProcessUpgrade(upgrade, ref currentDamage, ref currentCooldown, ref currentRadius);
                }
                if (updateIndex > 0 && updateIndex < baseSlot.m_FollowingUpgrades.Length)
                {
                    baseSlot = baseSlot.m_FollowingUpgrades[updateIndex];
                }
                else
                {
                    break;//invalid updateIndex
                }
            }
        }

        private void ProcessUpgrade(Upgrade upgrade,ref float damage,ref float cooldown,ref float radius){
            switch (upgrade.m_Type)
            {
                case Upgrade.Type.Damage:
                    {
                        if (upgrade.m_Additive)
                        {
                            damage += m_BaseDamage * upgrade.m_Modifier;
                        }
                        else//multiplikativ
                        {
                            damage *= upgrade.m_Modifier;
                        }
                        break;
                    }
                case Upgrade.Type.Cooldown:
                    {
                        if (upgrade.m_Additive)
                        {
                            cooldown += m_BaseCooldown * upgrade.m_Modifier;
                        }
                        else//multiplikativ
                        {
                            cooldown *= upgrade.m_Modifier;
                        }
                        break;
                    }
                case Upgrade.Type.Radius:
                    {
                        if (upgrade.m_Additive)
                        {
                            radius += m_BaseRadius * upgrade.m_Modifier;
                        }
                        else//multiplikativ
                        {
                            radius *= upgrade.m_Modifier;
                        }
                        break;
                    }
                case Upgrade.Type.Special:
                    {
                        //todo specialid
                        break;
                    }
            }
        }

        public void Reload()
        {
            m_CurrentCooldown = m_Cooldown;
        }
    
        public bool CanShoot() {
            return m_CurrentCooldown <= 0.0f;
        }
        public float Damage
        {
            get { return m_Damage; }
            set { this.m_Damage = value; }
        }
        public float Radius
        {
            get { return m_Radius; }
            set { this.m_Radius = value; }
        }
        public float Cooldown
        {
            get { return m_Cooldown; }
            set { this.m_Cooldown = value; }
        }
    }
}
