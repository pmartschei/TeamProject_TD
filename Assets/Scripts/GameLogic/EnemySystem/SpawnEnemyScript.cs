using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.GameLogic.EnemySystem;

public class SpawnEnemyScript : MonoBehaviour
{


    public FieldScript m_FieldSystem;

    private GameObject m_EndTile;
    public TilePos m_SpawnPos;
    public GameObject m_EnemyTest;
    public float m_Timer;

    private List<TilePos> m_ToProcess = new List<TilePos>();
    private List<TilePos> m_AlreadyChecked = new List<TilePos>();
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_Timer += Time.deltaTime;
        if (m_Timer > 1)
        {
            m_SpawnPos = GetSpawn();
            SpawnEnemyOn(m_SpawnPos);
            m_Timer = 0;
        }
    }
    private TilePos GetSpawn()
    {
        m_EndTile = GameObject.FindObjectOfType<EndPoint>().gameObject;
        if (m_EndTile == null)
            return null;

        int x;
        int y;
        m_FieldSystem.GetTilePos(m_EndTile, out x, out y);
        if (x == -1 || y == -1)
            return null;

        m_ToProcess = processTiles(x, y);

        if (m_ToProcess.Count == 0)
            return null;
        return m_ToProcess[0];
    }
    private void SpawnEnemyOn(TilePos spawnPos)
    {
        GameObject spawn = m_FieldSystem.GetTileFrom(spawnPos.x, spawnPos.y);
        PathTileScript pathScript = spawn.GetComponent<PathTileScript>();
        if (pathScript.m_north && m_FieldSystem.GetTileFrom(spawnPos.x, spawnPos.y + 1)==null)
        {
            GameObject copy = GameObject.Instantiate(m_EnemyTest);
            copy.transform.position = pathScript.m_PathNorth[0].transform.position;
            PathMoveScript pathMove = copy.GetComponent<PathMoveScript>();
            if (pathMove == null)
                return;
            pathMove.m_CurrentIndex = 1;
            pathMove.m_CurrentList = pathScript.m_PathNorth;
            pathMove.m_CurrentObject = spawn;
        }

        if (pathScript.m_south && m_FieldSystem.GetTileFrom(spawnPos.x, spawnPos.y - 1) == null)
        {
            GameObject copy = GameObject.Instantiate(m_EnemyTest);
            copy.transform.position = pathScript.m_PathSouth[0].transform.position;
            PathMoveScript pathMove = copy.GetComponent<PathMoveScript>();
            if (pathMove == null)
                return;
            pathMove.m_CurrentIndex = 1;
            pathMove.m_CurrentList = pathScript.m_PathSouth;
            pathMove.m_CurrentObject = spawn;
        }

        if (pathScript.m_east && m_FieldSystem.GetTileFrom(spawnPos.x+1, spawnPos.y) == null)
        {
            GameObject copy = GameObject.Instantiate(m_EnemyTest);
            copy.transform.position = pathScript.m_PathEast[0].transform.position;
            PathMoveScript pathMove = copy.GetComponent<PathMoveScript>();
            if (pathMove == null)
                return;
            pathMove.m_CurrentIndex = 1;
            pathMove.m_CurrentList = pathScript.m_PathEast;
            pathMove.m_CurrentObject = spawn;
        }

        if (pathScript.m_west && m_FieldSystem.GetTileFrom(spawnPos.x-1, spawnPos.y) == null)
        {
            GameObject copy = GameObject.Instantiate(m_EnemyTest);
            copy.transform.position = pathScript.m_PathWest[0].transform.position;
            PathMoveScript pathMove = copy.GetComponent<PathMoveScript>();
            if (pathMove == null)
                return;
            pathMove.m_CurrentIndex = 1;
            pathMove.m_CurrentList = pathScript.m_PathWest;
            pathMove.m_CurrentObject = spawn;
        }
    }

    private List<TilePos> processTiles(int x, int y)
    {
        PathTileScript[] scripts = GameObject.FindObjectsOfType<PathTileScript>();
        foreach (PathTileScript script in scripts)
        {
            script.m_ParentObject.Clear();
            script.m_ParentDirection.Clear();
        }
        m_AlreadyChecked.Clear();
        List<TilePos> list = new List<TilePos>();
        List<TilePos> spawnPos = new List<TilePos>();
        list.Add(new TilePos(x, y));
        while (list.Count > 0)
        {
            TilePos pos = list[0];
            m_AlreadyChecked.Add(pos);
            list.RemoveAt(0);
            GameObject objToCheck = m_FieldSystem.GetTileFrom(pos.x, pos.y);
            PathTileScript pathScript = objToCheck.GetComponent<PathTileScript>();
            if (pathScript == null)
                continue;
            if (pathScript.m_north)
            {
                if (m_FieldSystem.GetTileFrom(pos.x, pos.y + 1) == null)
                {
                    spawnPos.Add(pos);
                }
                else
                {
                    TilePos newPos = new TilePos(pos.x, pos.y + 1);
                    if (!m_AlreadyChecked.Contains(newPos))
                    {
                        GameObject tile = m_FieldSystem.GetTileFrom(newPos.x, newPos.y);
                        PathTileScript script = tile.GetComponent<PathTileScript>();
                        script.m_ParentObject.Add(objToCheck);
                        script.m_ParentDirection.Add(PathTileScript.NORTH);
                        list.Add(newPos);
                    }
                }
            }

            if (pathScript.m_south)
            {
                if (m_FieldSystem.GetTileFrom(pos.x, pos.y - 1) == null)
                {
                    spawnPos.Add(pos);
                }
                else
                {
                    TilePos newPos = new TilePos(pos.x, pos.y - 1);
                    if (!m_AlreadyChecked.Contains(newPos))
                    {
                        GameObject tile = m_FieldSystem.GetTileFrom(newPos.x, newPos.y);
                        PathTileScript script = tile.GetComponent<PathTileScript>();
                        script.m_ParentObject.Add(objToCheck);
                        script.m_ParentDirection.Add(PathTileScript.SOUTH);
                        list.Add(newPos);
                    }
                }
            }

            if (pathScript.m_east)
            {
                if (m_FieldSystem.GetTileFrom(pos.x + 1, pos.y) == null)
                {
                    spawnPos.Add(pos);
                }
                else
                {
                    TilePos newPos = new TilePos(pos.x + 1, pos.y);
                    if (!m_AlreadyChecked.Contains(newPos))
                    {
                        GameObject tile = m_FieldSystem.GetTileFrom(newPos.x, newPos.y);
                        PathTileScript script = tile.GetComponent<PathTileScript>();
                        script.m_ParentObject.Add(objToCheck);
                        script.m_ParentDirection.Add(PathTileScript.EAST);
                        list.Add(newPos);
                    }
                }
            }

            if (pathScript.m_west)
            {
                if (m_FieldSystem.GetTileFrom(pos.x - 1, pos.y) == null)
                {
                    spawnPos.Add(pos);
                }
                else
                {
                    TilePos newPos = new TilePos(pos.x - 1, pos.y);
                    if (!m_AlreadyChecked.Contains(newPos))
                    {
                        GameObject tile = m_FieldSystem.GetTileFrom(newPos.x, newPos.y);
                        PathTileScript script = tile.GetComponent<PathTileScript>();
                        script.m_ParentObject.Add(objToCheck);
                        script.m_ParentDirection.Add(PathTileScript.WEST);
                        list.Add(newPos);
                    }
                }
            }
        }
        return spawnPos;
    }
}
