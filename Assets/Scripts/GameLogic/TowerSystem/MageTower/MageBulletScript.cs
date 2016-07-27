using UnityEngine;
using System.Collections;

public class MageBulletScript : MonoBehaviour
{

    public Vector3 m_startPosition;
    public EnemyScript m_target;
    public float m_travelTime;
    public float m_damage;
    private bool m_destroying;
    private float m_currentTravelTime;
    // Use this for initialization
    void Start()
    {
    }

    void OnTriggerEnter(Collider obj)
    {
        EnemyScript enemy = obj.gameObject.GetComponent<EnemyScript>();
        if (enemy != null)//wenn enemy
        {
            //enemy beschädigen
            enemy.DoDamage(m_damage);
            Hit(enemy);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (m_target == null)//wenn enemy nicht schon tot
        {
            if (!m_destroying)
            {
                m_destroying = true;
                Hit(m_target);
            }
            return;
        }
        m_currentTravelTime += Time.deltaTime;//vergange zeit erhöhen
        float percent = 0.0f;//prozent für zeit ausrechnen
        if (m_travelTime > 0)
        {
            percent = m_currentTravelTime / m_travelTime;
        }
        else
        {
            percent = 1;
        }
        if (percent > 1)
        {
            percent = 1;
        }
        Vector3 diff = m_target.transform.position-m_startPosition;//differenz der beiden Punkte
        this.transform.position = m_startPosition + diff * percent;//setzen der neuen Position
    }

    private void Hit(EnemyScript target){
        float duration = 0.0f;

        Transform trail = this.gameObject.transform.FindChild("MageTrail");
        if (trail != null)
        {
            if (target.Dead())
            {
                trail.parent = null;
            }
            ParticleSystem particleSystem = trail.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                particleSystem.loop = false;
                duration = particleSystem.duration;
                //trail zerstören
                Destroy(particleSystem.gameObject, duration);
            }
        }
        if (target.Dead())
        {
            //bullet zerstören
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject, duration);
        }
    }
}
