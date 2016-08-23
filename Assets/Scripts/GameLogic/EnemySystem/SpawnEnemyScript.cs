using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.GameLogic.EnemySystem;

public class SpawnEnemyScript : MonoBehaviour
{


    public FieldScript m_FieldSystem;

    private GameObject m_EndTile;
    public TilePos m_SpawnPos;
	public List<IWave> m_Waves=new List<IWave>();
    public GameObject m_firstEnemy;
    public GameObject m_secondEnemy;
    public float m_Timer;

    private List<TilePos> m_ToProcess = new List<TilePos>();
    private List<TilePos> m_AlreadyChecked = new List<TilePos>();
    // Use this for initialization
    void Start()
    {
		Wave second = new Wave (1000, m_firstEnemy, 0.3f);
		Wave first = new Wave (5, m_secondEnemy, 1);
		WaveGroup wg = new WaveGroup ();
		wg.m_Waves.Add (first);
		m_Waves.Add (wg);
		m_Waves.Add (second);
    }

    // Update is called once per frame
    void Update()
    {
		if (m_Waves.Count > 0) {
			//hole alle GameObjekte von den Wellen
			GameObject[] gos = m_Waves [0].Update ();
			if (gos != null)
            {
                m_SpawnPos = GetSpawn();//Spawnpunkt holen
                m_FieldSystem.LevelFinished(m_SpawnPos.y);
                foreach (GameObject go in gos) {
					SpawnEnemyOn (go, m_SpawnPos);//Alle GameObjekte Spawnen
				}
				if (m_Waves [0].Clear ()) {//Wenn Welle fertig
					m_Waves.RemoveAt (0);
				}
			}
		}
    }
    private TilePos GetSpawn()
    {
        m_EndTile = GameObject.FindObjectOfType<EndPoint>().gameObject;
        if (m_EndTile == null)
            return null;

        m_ToProcess = processTiles(m_FieldSystem.m_LevelWidth/2,0);
        
        //endtile hinzufügen
        GameObject first = m_FieldSystem.GetTileFrom(m_FieldSystem.m_LevelWidth / 2,0);
        PathTileScript firstScript = first.GetComponent<PathTileScript>();
        firstScript.m_ParentDirection.Add(PathTileScript.NORTH);
        firstScript.m_ParentObject.Add(m_EndTile);

        if (m_ToProcess.Count == 0)
            return null;
        return m_ToProcess[0];
    }
    private void SpawnEnemyOn(GameObject go,TilePos spawnPos)
    {
        //if (spawnPos == null) return;
        GameObject spawn = m_FieldSystem.GetTileFrom(spawnPos.x, spawnPos.y);
        PathTileScript pathScript = spawn.GetComponent<PathTileScript>();
        GameObject copy = GameObject.Instantiate(go);
        copy.transform.parent = this.transform;
        PathMoveScript pathMove = copy.GetComponent<PathMoveScript>();
        if (pathMove == null)
            return;
        pathMove.m_CurrentObject = spawn;
        pathMove.m_CurrentIndex = 1;
        if (pathScript.m_north && m_FieldSystem.GetTileFrom(spawnPos.x, spawnPos.y + 1)==null)
        {
            copy.transform.position = pathScript.m_PathNorth[0].transform.position;
            pathMove.m_CurrentList = pathScript.m_PathNorth;
        }

        if (pathScript.m_south && m_FieldSystem.GetTileFrom(spawnPos.x, spawnPos.y - 1) == null)
        {
            copy.transform.position = pathScript.m_PathSouth[0].transform.position;
            pathMove.m_CurrentList = pathScript.m_PathSouth;
        }

        if (pathScript.m_east && m_FieldSystem.GetTileFrom(spawnPos.x+1, spawnPos.y) == null)
        {
            copy.transform.position = pathScript.m_PathEast[0].transform.position;
            pathMove.m_CurrentList = pathScript.m_PathEast;
        }

        if (pathScript.m_west && m_FieldSystem.GetTileFrom(spawnPos.x-1, spawnPos.y) == null)
        {
            copy.transform.position = pathScript.m_PathWest[0].transform.position;
            pathMove.m_CurrentList = pathScript.m_PathWest;
        }
        pathMove.m_rotationToNext = pathMove.m_CurrentList[pathMove.m_CurrentIndex].transform.position- pathMove.transform.position;
        pathMove.m_rotationPos = pathMove.transform.position;
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
                if (pos.y != 0 && pos.x != m_FieldSystem.m_LevelWidth / 2)
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
