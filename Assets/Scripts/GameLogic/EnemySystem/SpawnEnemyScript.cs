using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.GameLogic.EnemySystem;

public class SpawnEnemyScript : MonoBehaviour
{


    public FieldScript m_FieldSystem;

    private GameObject m_EndTile;
    public TilePos m_SpawnPos;
    public List<IWave> m_Waves = new List<IWave>();
    public GameObject m_firstEnemy;
    public GameObject m_secondEnemy;
    public float m_Timer;

    private List<TilePos> m_ToProcess = new List<TilePos>();
    private List<TilePos> m_AlreadyChecked = new List<TilePos>();

    public int m_WaveDifficultyPoints = 100;
    public int m_WavesStored = 5;
    public int m_CurrentWaveIndex = 0;
    [Range(0.01f, 1.00f)]
    public float m_Difficulty = 0.05f;
    public Enemies m_Enemies;

    public int m_WaveDelayBetween = 5;
    private float m_CurrentDelayBetween = 0.0f;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < m_WavesStored; i++)
        {
            m_Waves.Add(CreateNewWave(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
		if (m_Waves.Count > 0) {
            //hole alle GameObjekte von den Wellen
            if (m_CurrentDelayBetween <= 0.0f)
            {
                GameObject[] gos = m_Waves[0].Update();
                if (gos != null)
                {
                    m_SpawnPos = GetSpawn();//Spawnpunkt holen
                    if (m_SpawnPos == null) return;
                    m_FieldSystem.LevelFinished(m_SpawnPos.y);
                    foreach (GameObject go in gos)
                    {
                        SpawnEnemyOn(go, m_SpawnPos);//Alle GameObjekte Spawnen
                    }
                    if (m_Waves[0].Clear())
                    {//Wenn Welle fertig
                        m_Waves.RemoveAt(0);
                        m_Waves.Add(CreateNewWave(m_CurrentWaveIndex + m_WavesStored));
                        m_CurrentWaveIndex++;
                        m_CurrentDelayBetween = m_WaveDelayBetween;
                    }
                }
            }
            else
            {
                m_CurrentDelayBetween -= Time.deltaTime;
            }
		}
    }

    private IWave CreateNewWave(int difficulty,int usedWavePoints = 0,GameObject enemy = null)
    {
        float multiplier = 1.0f + difficulty * m_Difficulty;
        float wavePoints = m_WaveDifficultyPoints * multiplier;
        if (usedWavePoints != 0)
        {
            wavePoints = usedWavePoints * multiplier;
        }
        if (Random.Range(0f,1f)<0.9f || enemy!=null)
        {
            if (enemy == null)
            {
                enemy = m_Enemies.GetRandomEnemy();
            }
            enemy.transform.SetParent(this.transform);
            EnemyScript es = enemy.GetComponent<EnemyScript>();
            es.IncreaseDifficulty(difficulty);
            if (es == null) throw new System.Exception("ERROR : No EnemyScript");
            int count = (int)(wavePoints / es.m_WavePoints) + 1;
            float delay = Random.Range(0.75f / es.m_Speed, 2f / es.m_Speed);
            Wave w = new Wave(count,enemy,delay);
            return w;
        }
        else
        {
            GameObject[] enemies = m_Enemies.GetRandomEnemies(2);
            WaveGroup wg = new WaveGroup();
            for (int i = 0; i < 2; i++)
            {
                wg.m_Waves.Add(CreateNewWave(difficulty, (int)(usedWavePoints / 2.0f), enemies[i]));
            }
            return wg;
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
    private void SpawnEnemyOn(GameObject go, TilePos spawnPos)
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
        if (pathScript.m_north && m_FieldSystem.GetTileFrom(spawnPos.x, spawnPos.y + 1) == null)
        {
            copy.transform.position = pathScript.m_PathNorth[0].transform.position;
            pathMove.m_CurrentList = pathScript.m_PathNorth;
        }

        if (pathScript.m_south && m_FieldSystem.GetTileFrom(spawnPos.x, spawnPos.y - 1) == null)
        {
            copy.transform.position = pathScript.m_PathSouth[0].transform.position;
            pathMove.m_CurrentList = pathScript.m_PathSouth;
        }

        if (pathScript.m_east && m_FieldSystem.GetTileFrom(spawnPos.x + 1, spawnPos.y) == null)
        {
            copy.transform.position = pathScript.m_PathEast[0].transform.position;
            pathMove.m_CurrentList = pathScript.m_PathEast;
        }

        if (pathScript.m_west && m_FieldSystem.GetTileFrom(spawnPos.x - 1, spawnPos.y) == null)
        {
            copy.transform.position = pathScript.m_PathWest[0].transform.position;
            pathMove.m_CurrentList = pathScript.m_PathWest;
        }
        pathMove.m_rotationToNext = pathMove.m_CurrentList[pathMove.m_CurrentIndex].transform.position - pathMove.transform.position;
        pathMove.m_rotationPos = pathMove.transform.position;
        float rotate = Vector3.Angle(Vector3.back, pathMove.m_rotationToNext);
        Vector3 cross = Vector3.Cross(Vector3.back, pathMove.m_rotationToNext);
        if (cross.y < 0) rotate = -rotate;
        if (rotate != 0.0f)
        {
            pathMove.transform.Rotate(Vector3.up, rotate);
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
