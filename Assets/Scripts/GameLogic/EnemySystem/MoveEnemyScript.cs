using UnityEngine;
using System.Collections;

public class MoveEnemyScript : MonoBehaviour
{

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
            Vector3 next = Vector3.MoveTowards(obj.transform.position, target.transform.position, 3 * Time.deltaTime);//vector dahin
            obj.transform.position = next;//position setzen
            if ((obj.transform.position - target.transform.position).magnitude < 0.001)//wenn auf dem punkt
            {
                pathMove.m_CurrentIndex++;
                if (pathMove.m_CurrentIndex >= pathMove.m_CurrentList.Count)//wenn letzer punkt 
                {
                    PathTileScript script = pathMove.m_CurrentObject.GetComponent<PathTileScript>();
                    if (script.m_ParentObject.Count == 0)
                    {
                        Destroy(obj);
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
