using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.GameLogic.TowerSystem
{
    [System.Serializable]
    public class Upgrade
    {
        public enum Type
        {
            Damage,Cooldown,Radius,Special
        }
        public Type m_Type;
        public int m_Cost;
        public bool m_Additive = true;
        public float m_Modifier;
        public int m_SpecialId;
        public string m_Text;
    }
}
