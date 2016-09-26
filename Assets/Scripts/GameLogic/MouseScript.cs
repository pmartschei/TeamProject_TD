using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

public class MouseScript : MonoBehaviour
{
    private GameObject m_buildTowerHUD;
    private GameObject m_totemHUD;
    private GameObject m_upgradeTower;
    private GameObject m_checkTextField;

    // Use this for initialization
    public Camera m_Camera;
    public GameObject m_Tile;
    public GameObject m_FloorPlane;
    public FieldScript m_Field;

    private GameObject m_GhostTile;
    private GameObject m_parentTile;

    private GameObject m_Lightning;
    private GameObject m_LightningEnd;
    private LineRenderer m_CircleRender;

    private float m_BoltAnimationDurationLeft = 0.0f;

    public float m_BoltAnimationDuration = 0.5f;

    public float m_BoltDamage = 0.5f;

    public float m_BoltRadius = 2f;

    public float m_BoltCooldown = 60f;

    private float m_CurrentBoltCooldown = 0.0f;

    public MouseState m_MouseState = MouseState.BuildTile;

    public enum MouseState
    {
        BuildTile,
        Bolt,
        None
    }
    void Start()
    {
        m_Lightning = transform.Find("SimpleLightningBoltAnimatedPrefab").gameObject;
        m_LightningEnd = transform.Find("SimpleLightningBoltAnimatedPrefab").Find("LightningEnd").gameObject;
        m_CircleRender = GetComponent<LineRenderer>();
        GetComponent<CircleRender>().SetRadius(m_BoltRadius);
   
        m_buildTowerHUD = GameObject.Find("HUDCanvas").transform.FindChild("BuildTowerHUD").gameObject;
        m_totemHUD = GameObject.Find("HUDCanvas").transform.FindChild("TotemHUD").gameObject;
        m_upgradeTower = GameObject.Find("HUDCanvas").transform.Find("UpgradeTowerHUD").gameObject;
        m_checkTextField = GameObject.Find("HUDCanvas").transform.FindChild("CheckTextField").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0.0f) return;
        m_CurrentBoltCooldown = Math.Max(m_CurrentBoltCooldown-Time.deltaTime,0);
        if (m_BoltAnimationDurationLeft > 0.0f)
        {
            m_BoltAnimationDurationLeft -= Time.deltaTime;
            PlayBoldAnimation();
            if (m_BoltAnimationDurationLeft <= 0.0f)
            {
                m_Lightning.SetActive(false);
            }
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            if (m_Tile != null)
            {
                Destroy(m_Tile);
            }
            if (m_GhostTile != null)
            {
                Destroy(m_GhostTile);
            }
            m_Lightning.SetActive(false);
            m_CircleRender.enabled = false;
            m_MouseState = MouseState.None;
        }
        if (m_MouseState == MouseState.Bolt && m_CurrentBoltCooldown==0.0f)
        {
            Ray r = m_Camera.ScreenPointToRay(Input.mousePosition);//Ray von der Maus
            Vector3 hit = r.origin + Mathf.Abs(r.origin.y / r.direction.y + 0.5f) * r.direction;//Auf Y=0 Achse den hit suchen
            if (Input.GetMouseButton(0))
            {
                Collider[] colliders = Physics.OverlapSphere(hit, m_BoltRadius);
                foreach (Collider collider in colliders)
                {
                    EnemyScript es = collider.GetComponent<EnemyScript>();
                    if (es != null)
                    {
                        es.DoDamage(es.m_MaxHp*m_BoltDamage);
                        m_BoltAnimationDurationLeft = m_BoltAnimationDuration;
                        m_CurrentBoltCooldown = m_BoltCooldown;
                        m_Lightning.SetActive(true);
                        m_CircleRender.enabled = false;
                        m_MouseState = MouseState.None;
                    }
                }
            }
            transform.position = hit;
        }
        else if(m_MouseState==MouseState.BuildTile) {
            if (m_Tile != null)
            {
            m_upgradeTower.SetActive(false);
            m_totemHUD.SetActive(false);
            m_buildTowerHUD.SetActive(false);
            m_checkTextField.SetActive(false);
                Ray r = m_Camera.ScreenPointToRay(Input.mousePosition);//Ray von der Maus
                Vector3 hit = r.origin + Mathf.Abs(r.origin.y / r.direction.y) * r.direction;//Auf Y=0 Achse den hit suchen
                if (Input.GetMouseButtonDown(1))//Rechtsklick
                {
                    PathTileScript script = m_Tile.GetComponent<PathTileScript>();
                    m_Tile.transform.Rotate(Vector3.forward, 90);//Rotieren
                    if (script != null)
                    {
                        script.Rotate();//rotieren von den teilen
                    }
                    Destroy(m_GhostTile);
                    m_GhostTile = null;
                }
                if (m_GhostTile == null)
                {
                    m_GhostTile = (GameObject)Instantiate(m_Tile, new Vector3(-100, -100, -100), m_Tile.transform.rotation);
                    m_GhostTile.name = "GhostTile";
                    Renderer[] renderers = m_GhostTile.GetComponents<Renderer>();
                    foreach (Renderer renderer in renderers)
                    {
                        Material ghostMaterial = renderer.material;
                        ghostMaterial.color = new Color(1.0f, 1.0f, 1.0f, 0.25f);
                    }
                }

                bool xNegative;
                bool yNegative;
                xNegative = hit.x < 0.0f;
                yNegative = hit.z < 0.0f;
                int x = (int)(hit.x / m_Field.m_SizeX);
                int y = (int)(hit.z / m_Field.m_SizeY);
                if (hit.x < 0 || hit.z < 0)//negativ x und z)
                    return;
                float xMultiplier = 1.0f;
                float yMultiplier = 1.0f;
                if (x <= 0)//für negativ x nicht benötigt aktuell
                {
                    if (xNegative)
                        xMultiplier = -1.0f;
                }
                if (y <= 0)//für negativ y nicht benötigt aktuell
                {
                    if (yNegative)
                        yMultiplier = -1.0f;
                }
                //if Feld nen nachbar hat und es Im Feld existiert
                if (m_Field.HasNeighbour(x, y) && !m_Field.IsTile(x, y))
                {
                    //positionsberechnung
                    Vector3 pos = new Vector3(x * m_Field.m_SizeX + m_Field.m_SizeX / 2.0f * xMultiplier, 0.0f, y * m_Field.m_SizeY + m_Field.m_SizeY / 2.0f * yMultiplier);
                    m_GhostTile.transform.position = pos;
                    //Ghosttile verschieben

                    Renderer[] renderers = m_GhostTile.GetComponents<Renderer>();
                    if (checkValidStreet(x, y))
                    {
                        foreach (Renderer renderer in renderers)
                        {
                            Material ghostMaterial = renderer.material;
                            ghostMaterial.color = new Color(1.0f, 1.0f, 1.0f, 0.25f);
                        }
                        if (Input.GetMouseButtonDown(0))
                        {
                            GameObject rb = m_Tile;
                            m_Field.AddTileTo(rb, x, y);//zum feld hinzufügen

                            Transform t = rb.GetComponent<Transform>();
                            if (t != null)
                            {
                                t.position = pos;
                            }

                            m_MouseState = MouseState.None;
                            m_Tile = null;
                            GameObject.Find("DrawTileSystem").GetComponent<TileUI>().RemoveTile(m_parentTile);
                            GameObject.Destroy(m_GhostTile);
                            m_GhostTile = null;
                        }
                    }
                    else//if invalides Tile
                    {
                        foreach (Renderer renderer in renderers)
                        {
                            Material ghostMaterial = renderer.material;
                            ghostMaterial.color = new Color(1.0f, 0.0f, 0.0f, 0.25f);
                        }
                    }
                }
                else
                {
                    GameObject.Destroy(m_GhostTile);
                    m_GhostTile = null;
                }
            }
        }
    }

    private Vector3 GenerateRandomCirclePos(float v)
    {
        float t = 2 * (float)Math.PI * UnityEngine.Random.Range(0.0f, 1.0f);
        float u = UnityEngine.Random.Range(0.0f, 1.0f) + UnityEngine.Random.Range(0.0f, 1.0f);

        float r = u > 1 ? 2 - u : u;

        return new Vector3(r * (float)Math.Cos(t), r * (float)Math.Sin(t));
    }

    /// <summary>
    /// Checks if the Tile can be a Valid Street
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    bool checkValidStreet(int x, int y)
    {
        PathTileScript pt = m_Tile.GetComponent<PathTileScript>();
        GameObject southObject = m_Field.GetTileFrom(x, y - 1);
        GameObject northObject = m_Field.GetTileFrom(x, y + 1);
        GameObject eastObject = m_Field.GetTileFrom(x + 1, y);
        GameObject westObject = m_Field.GetTileFrom(x - 1, y);
        if (pt != null)
        {
            int resultSouth = checkStreet(pt, PathTileScript.SOUTH, southObject, PathTileScript.NORTH);
            int resultNorth = checkStreet(pt, PathTileScript.NORTH, northObject, PathTileScript.SOUTH);
            int resultEast = checkStreet(pt, PathTileScript.EAST, eastObject, PathTileScript.WEST);
            int resultWest = checkStreet(pt, PathTileScript.WEST, westObject, PathTileScript.EAST);
            if (resultEast <= 0 && resultWest <= 0 && resultSouth <= 0 && resultNorth <= 0)//wenn keiner connected ist
            {
                return false;
            }
            if (resultEast == -1 || resultNorth == -1 || resultSouth == -1 || resultWest == -1)//wenn einer invalid ist
            {
                return false;
            }
            if (pt.GetDirection(PathTileScript.EAST) && x + 1 >= m_Field.m_LevelWidth)//wenn osten true und letztes feld ist
            {
                return false;
            }
            if (pt.GetDirection(PathTileScript.WEST) && x - 1 < 0)//wenn westen true und letztes feld ist
            {
                return false;
            }
        }
        else
        {
            PathTileScript script;
            if (southObject != null)
            {
                script = southObject.GetComponent<PathTileScript>();
                if (script != null && script.m_north)
                {
                    return false;
                }
            }
            if (northObject != null)
            {
                script = northObject.GetComponent<PathTileScript>();
                if (script != null && script.m_south)
                {
                    return false;
                }
            }
            if (eastObject != null)
            {
                script = eastObject.GetComponent<PathTileScript>();
                if (script != null && script.m_west)
                {
                    return false;
                }
            }
            if (westObject != null)
            {
                script = westObject.GetComponent<PathTileScript>();
                if (script != null && script.m_east)
                {
                    return false;
                }
            }
        }
        return true;
    }
    /// <summary>
    /// Checks if the Street connects with another Street/Tile
    /// </summary>
    /// <param name="source"></param>
    /// <param name="sourceDirection"></param>
    /// <param name="target"></param>
    /// <param name="targetDirection"></param>
    /// <returns>
    /// -1 if invalid
    /// 0 if street not connected and everything fine
    /// 1 if street connected</returns>
    private int checkStreet(PathTileScript source, int sourceDirection, GameObject target, int targetDirection)
    {
        if (target != null)//testen ob Gameobject existiert
        {
            PathTileScript neighbourPT = target.GetComponent<PathTileScript>();
            bool dir = source.GetDirection(sourceDirection);
            if (neighbourPT != null)//hat Gameobject das Street script
            {
                bool neighbourDir = neighbourPT.GetDirection(targetDirection);
                if (neighbourDir != dir)//wenn die beiden Directions unterschiedlich sind (true!=false) ||(false!=true)
                {
                    return -1;
                }
                else
                {
                    if (neighbourDir && dir)//wenn beide gleich sind 
                    {
                        return 1;//connected
                    }
                    else//müssen beide false sein
                    {
                        return 0;//egal
                    }
                }
            }
            else//wenn keine street
            {
                if (dir)//wenn direction 
                {
                    return -1;//invalid
                }
                else
                {
                    return 0;//egal
                }
            }
        }
        return 0;//egal
    }

    private void PlayBoldAnimation()
    {
        Vector3 randomPos = GenerateRandomCirclePos(1.0f);
        m_LightningEnd.transform.localPosition = randomPos;
    }

    public void SetTile(GameObject gameObject)
    {
        if (m_Tile != null)
        {
            Destroy(m_Tile);
        }
        m_parentTile = gameObject;

        if (m_GhostTile != null)
        {
            Destroy(m_GhostTile);
        }
        m_CircleRender.enabled = false;
        m_Tile = Instantiate(gameObject);
        m_Tile.transform.rotation = Quaternion.Euler(-90, 0, 0);
        m_Tile.transform.position = new Vector3(900, 900, 900);
        m_MouseState = MouseState.BuildTile;
    }

    public void BoltActivate()
    {
        if (m_CurrentBoltCooldown == 0.0f)
        {
            if (m_GhostTile != null)
            {
                Destroy(m_GhostTile);
            }
            if (m_Tile != null)
            {
                Destroy(m_Tile);
            }
            m_MouseState = MouseState.Bolt;
            m_CircleRender.enabled = true;
        }
    }
}
