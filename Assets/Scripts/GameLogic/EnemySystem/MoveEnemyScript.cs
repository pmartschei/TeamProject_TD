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
        Debug.Log(pathMoves.Length);
        foreach (PathMoveScript pathMove in pathMoves)
        {
            GameObject obj = pathMove.gameObject;
            if (pathMove.m_CurrentIndex >= pathMove.m_CurrentList.Count)
                continue;
            GameObject target = pathMove.m_CurrentList[pathMove.m_CurrentIndex];
            Vector3 next = Vector3.MoveTowards(obj.transform.position, target.transform.position, 3 * Time.deltaTime);
            obj.transform.position = next;
            if ((obj.transform.position - target.transform.position).magnitude < 0.001)
            {
                pathMove.m_CurrentIndex++;
                if (pathMove.m_CurrentIndex >= pathMove.m_CurrentList.Count)
                {
                    PathTileScript script = pathMove.m_CurrentObject.GetComponent<PathTileScript>();
                    if (script.m_ParentObject.Count==0)
                        return;
                    pathMove.m_CurrentObject = script.m_ParentObject[0];
                    pathMove.m_CurrentIndex = 0;
                    PathTileScript nextScript=pathMove.m_CurrentObject.GetComponent<PathTileScript>();
                    pathMove.m_CurrentList = nextScript.GetPath(script.m_ParentDirection[0]);
                }
            }
        }
    }
}
