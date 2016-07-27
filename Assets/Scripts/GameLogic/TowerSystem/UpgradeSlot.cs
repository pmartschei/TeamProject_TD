using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.GameLogic.TowerSystem
{
    [System.Serializable]
    public class UpgradeSlot
    {
        public Upgrade m_Upgrade=null;
        public UpgradeSlot[] m_FollowingUpgrades;
    }
}
