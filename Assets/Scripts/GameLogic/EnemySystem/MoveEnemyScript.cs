using UnityEngine;
using System.Collections;

public class MoveEnemyScript : MonoBehaviour
{

    private Animation anim;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PathMoveScript[] pathMoves = GameObject.FindObjectsOfType<PathMoveScript>();
        foreach (PathMoveScript pathMove in pathMoves)
        {
            GameObject obj = pathMove.gameObject;//get gameobjekt
            if (pathMove.m_CurrentIndex >= pathMove.m_CurrentList.Count)//schon zuende gelaufen
                continue;
            GameObject target = pathMove.m_CurrentList[pathMove.m_CurrentIndex];//wohin soll ich laufen
            float speed = 1;
            EnemyScript enemy = obj.GetComponent<EnemyScript>();
            enemy.RecalculateAttributes();
            if (enemy != null)
            {
                speed = enemy.m_Speed;
            }
            Vector3 next = Vector3.MoveTowards(obj.transform.position, target.transform.position, speed * Time.deltaTime);//vector dahin
            obj.transform.position = next;//position setzen

            Vector3 nextRotation = pathMove.m_CurrentList[pathMove.m_CurrentIndex].transform.position-pathMove.m_rotationPos;

            float rotate = Vector3.Angle(pathMove.m_rotationToNext,nextRotation);
            Vector3 cross = Vector3.Cross(pathMove.m_rotationToNext, nextRotation);
            if (cross.y < 0) rotate = -rotate;
            if (rotate != 0.0f)
            {
                pathMove.transform.Rotate(Vector3.up, rotate);
                pathMove.m_rotationToNext = nextRotation;
                pathMove.m_rotationPos = obj.transform.position;
            }

            if ((obj.transform.position - target.transform.position).magnitude < 0.001)//wenn auf dem punkt
            {
                pathMove.m_CurrentIndex++;
                if (pathMove.m_CurrentIndex >= pathMove.m_CurrentList.Count)//wenn letzer punkt 
                {
                    PathTileScript script = pathMove.m_CurrentObject.GetComponent<PathTileScript>();
                    if (script.m_ParentObject.Count == 0)
                    {
                        enemy.DoDamage(enemy.m_MaxHp, true);
                        return;
                    }
                    pathMove.m_CurrentObject = script.m_ParentObject[0];
                    pathMove.m_CurrentIndex = 0;
                    PathTileScript nextScript=pathMove.m_CurrentObject.GetComponent<PathTileScript>();
                    pathMove.m_CurrentList = nextScript.GetPath(script.m_ParentDirection[0]);
                }
            }
        }
    }
}
