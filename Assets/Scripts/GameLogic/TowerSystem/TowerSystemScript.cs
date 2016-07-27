using UnityEngine;
using System.Collections;
using Assets.Scripts.GameLogic.TowerSystem;
using SimpleJSON;

public class TowerSystemScript : MonoBehaviour
{
    public GameObject m_tower1;
    public TextAsset m_tower1UpgradeAsset;
    public GameObject m_tower2;
    public GameObject m_tower3;
    public GameObject m_tower4;

    private Hashtable m_UpgradeTrees = new Hashtable();

	// Use this for initialization
	void Start ()
    {
        LoadUpgradeTree(m_tower1,m_tower1UpgradeAsset);
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

        foreach (TowerScript tower in towers)
        {
            if (!tower.CanShoot()) break;//wenn der Tower nicht schießen kann dann zum nächsten
            GameObject towerObj = tower.gameObject;
            Collider[] colliders = Physics.OverlapSphere(towerObj.transform.position, tower.Radius);//alle Collider im Radius erhalten
            foreach (Collider collider in colliders)
            {
                EnemyScript enemy = collider.GetComponent<EnemyScript>();
                if (enemy != null)//prüfen ob der Collider nen Enemy ist
                {
                    //TODO: Farest,Highest Hp, Lowest Hp wie die taktik vom turm ist

                    if (tower.Shoot(enemy))//Auf den Enemy schießen
                    {
                        break;
                    }
                }
            }
        }
	}
}
