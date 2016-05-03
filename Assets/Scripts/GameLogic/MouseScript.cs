using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MouseScript : MonoBehaviour
{

    // Use this for initialization
    public Camera m_Camera;
    public Camera m_Camera2;
    public GameObject m_Screen;
    public GameObject m_Tile;
    public GameObject m_FloorPlane;
    public FieldScript m_Field;
    private GameObject m_GhostTile;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))//Rechtsklick
        {
            PathTileScript script = m_Tile.GetComponent<PathTileScript>();
            if (script != null)
            {
                m_Tile.transform.Rotate(Vector3.forward, 90);//Rotieren
                script.Rotate();//rotieren von den teilen
                Destroy(m_GhostTile);
                m_GhostTile = null;
            }
            Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit1;
            Debug.Log(ray);
            if (Physics.Raycast(ray, out hit1))
            {
                Debug.Log(hit1.textureCoord);
                if (hit1.transform.gameObject == m_Screen)
                {
                    ray = m_Camera2.ViewportPointToRay(new Vector3(hit1.textureCoord.x, hit1.textureCoord.y, 0f));
                    if (Physics.Raycast(ray, out hit1))
                    {
                        Debug.Log(hit1.transform.gameObject);
                    }
                }
            }
        }
        if (m_GhostTile == null)
        {
            m_GhostTile = Instantiate(m_Tile);
            m_GhostTile.transform.position = new Vector3(-100, -100, -100);
            Material ghostMaterial = m_GhostTile.GetComponent<Renderer>().material;
            ghostMaterial.color = new Color(1.0f, 1.0f, 1.0f, 0.25f);
        }
        Mesh mesh = m_GhostTile.GetComponent<MeshFilter>().mesh;
        Vector3 size = mesh.bounds.size;//Mesh size
        //GameObject floorPlane = Instantiate(m_FloorPlane);
        //Mesh floorMesh = floorPlane.GetComponent<MeshFilter>().mesh;
        //float ratioX = size.x / floorMesh.bounds.size.x;
        //float ratioY = size.y / floorMesh.bounds.size.y;
        //floorPlane.transform.position = new Vector3(0, 0, 0);
        //floorPlane.transform.localScale = new Vector3(ratioX*5, 0, ratioY*5);
        Ray r = m_Camera.ScreenPointToRay(Input.mousePosition);//Ray von der Maus
        Vector3 hit = r.origin + Mathf.Abs(r.origin.y / r.direction.y) * r.direction;//Auf Y=0 Achse den hit suchen
        bool xNegative;
        bool yNegative;
        xNegative = hit.x < 0.0f;
        yNegative = hit.z < 0.0f;
        int x = (int)(hit.x / size.x);
        int y = (int)(hit.z / size.y);
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
            Vector3 pos = new Vector3(x * size.x + size.x / 2.0f * xMultiplier, -size.z / 2.0f, y * size.y + size.y / 2.0f * yMultiplier);
            m_GhostTile.transform.position = pos;
            //Ghosttile verschieben
            if (checkValidStreet(x, y))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject rb = Instantiate(m_Tile);
                    m_Field.AddTileTo(rb, x, y);//zum feld hinzufügen

                    Transform t = rb.GetComponent<Transform>();
                    if (t != null)
                    {
                        t.position = pos;
                    }
                    GameObject.Destroy(m_GhostTile);
                    m_GhostTile = null;
                }
            }
            else//if invalides Tile
            {
                Material ghostMaterial = m_GhostTile.GetComponent<Renderer>().material;
                ghostMaterial.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            }
        }
        else
        {
            GameObject.Destroy(m_GhostTile);
            m_GhostTile = null;
        }
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
        if (pt != null)
        {
            GameObject southObject = m_Field.GetTileFrom(x, y - 1);
            GameObject northObject = m_Field.GetTileFrom(x, y + 1);
            GameObject eastObject = m_Field.GetTileFrom(x + 1, y);
            GameObject westObject = m_Field.GetTileFrom(x - 1, y);
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
            //if (pt.GetDirection(PathTileScript.EAST) && x + 1 >= m_Field.m_LevelWidth)//wenn osten true und letztes feld ist
            //{
            //    return false;
            //}
            //if (pt.GetDirection(PathTileScript.WEST) && x - 1 > 0)//wenn westen true und letztes feld ist
            //{
            //    return false;
            //}
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

    public void SetTile(GameObject gameObject)
    {
        m_Tile = Instantiate(gameObject);
    }
}
