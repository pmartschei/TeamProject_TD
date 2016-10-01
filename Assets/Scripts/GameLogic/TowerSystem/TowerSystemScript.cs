using UnityEngine;
using System.Collections;
using Assets.Scripts.GameLogic.TowerSystem;
using SimpleJSON;

public class TowerSystemScript : MonoBehaviour
{
    public GameObject m_tower1;
    public TextAsset m_tower1UpgradeAsset;
    public GameObject m_tower2;
    public TextAsset m_tower2UpgradeAsset;
    public GameObject m_tower3;
    public TextAsset m_tower3UpgradeAsset;

    private Hashtable m_UpgradeTrees = new Hashtable();

	// Use this for initialization
	void Start ()
    {
        LoadUpgradeTree(m_tower1,m_tower1UpgradeAsset);
        LoadUpgradeTree(m_tower2, m_tower2UpgradeAsset);
        LoadUpgradeTree(m_tower3, m_tower3UpgradeAsset);
    }

    private void LoadUpgradeTree(GameObject go,TextAsset upgradeAsset)
    {
        TowerScript tower = go.GetComponent<TowerScript>();
        if (tower != null)
        {
            UpgradeSlot startSlot = JsonUtility.FromJson<UpgradeSlot>(upgradeAsset.text);
            m_UpgradeTrees.Add(tower.GetType(), startSlot);
        }
    }

    public UpgradeSlot GetUpgradeSlotFor(System.Type type)
    {
        if (m_UpgradeTrees.Contains(type))
        {
            return (UpgradeSlot)m_UpgradeTrees[type];
        }
        return new UpgradeSlot();
    }
	// Update is called once per frame
	void Update ()
    {
        TowerScript[] towers = GetComponentsInChildren<TowerScript>();//alle Türme abfragen

        EnemyScript[] enemies = GameObject.FindObjectsOfType<EnemyScript>();
        foreach(EnemyScript enemy in enemies)
        {
            enemy.m_AdditionalSpeedMultiplier = 1.0f;
        }

        foreach (TowerScript tower in towers)
        {
			if (!tower.CanShoot()) continue;//wenn der Tower nicht schießen kann dann zum nächsten
            GameObject towerObj = tower.gameObject;
            Collider[] colliders = Physics.OverlapSphere(towerObj.transform.position, tower.Radius);//alle Collider im Radius erhalten
            float lowestHp = 1.1f;
            EnemyScript lowestEnemy = null;
            foreach (Collider collider in colliders)
            {
                EnemyScript enemy = collider.GetComponent<EnemyScript>();
                if (enemy != null)
                {
                    float percentage = enemy.m_Hp / enemy.m_MaxHp;
                    if (percentage < lowestHp)
                    {
                        lowestHp = percentage;
                        lowestEnemy = enemy;
                    }
                }
            }
            if (lowestEnemy != null)
            {
                if (tower.Shoot(lowestEnemy))
                {
                    lowestEnemy.RecalculateAttributes();
                    return;
                }
            }
            foreach (Collider collider in colliders)
            {
                EnemyScript enemy = collider.GetComponent<EnemyScript>();
                if (enemy != null && enemy!=lowestEnemy)
                {
                    tower.Shoot(enemy);
                    enemy.RecalculateAttributes();
                }
            }
        }
	}
    public void RecalculateAttributes()
    {
        TowerScript[] towers = GetComponentsInChildren<TowerScript>();//alle Türme abfragen
        foreach(TowerScript tower in towers)
        {
            tower.RecalculateAttributes();
        }
    }
}
