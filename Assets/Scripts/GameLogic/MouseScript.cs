using UnityEngine;
using System.Collections;

public class MouseScript : MonoBehaviour
{

    // Use this for initialization
    public Camera m_Camera;
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
        if (m_GhostTile == null)
        {
            m_GhostTile = Instantiate(m_Tile);
            m_GhostTile.transform.position = new Vector3(-100, -100, -100);
            Material ghostMaterial = m_GhostTile.GetComponent<Renderer>().material;
            ghostMaterial.color = new Color(1.0f, 0.0f, 0.0f, 0.25f);
        }
        Mesh mesh = m_GhostTile.GetComponent<MeshFilter>().mesh;
        Vector3 size = mesh.bounds.size;
        //GameObject floorPlane = Instantiate(m_FloorPlane);
        //Mesh floorMesh = floorPlane.GetComponent<MeshFilter>().mesh;
        //float ratioX = size.x / floorMesh.bounds.size.x;
        //float ratioY = size.y / floorMesh.bounds.size.y;
        //floorPlane.transform.position = new Vector3(0, 0, 0);
        //floorPlane.transform.localScale = new Vector3(ratioX*5, 0, ratioY*5);
        Ray r = m_Camera.ScreenPointToRay(Input.mousePosition);
        Vector3 hit = r.origin + Mathf.Abs(r.origin.y / r.direction.y) * r.direction;
        bool xNegative;
        bool yNegative;
        xNegative = hit.x < 0.0f;
        yNegative = hit.z < 0.0f;
        int x = (int)(hit.x / size.x);
        int y = (int)(hit.z / size.y);
        if (hit.x < 0 || hit.z < 0)
            return;
        float xMultiplier = 1.0f;
        float yMultiplier = 1.0f;
        if (x <= 0)
        {
            if (xNegative)
                xMultiplier = -1.0f;
        }
        if (y <= 0)
        {
            if (yNegative)
                yMultiplier = -1.0f;
        }
        if (m_Field.HasNeighbour(x, y) && !m_Field.IsTile(x,y) && checkValidStreet(x, y))
        {
            Vector3 pos = new Vector3(x * size.x + size.x / 2.0f * xMultiplier, -size.z / 2.0f, y * size.y + size.y / 2.0f * yMultiplier);
            m_GhostTile.transform.position = pos;
            if (Input.GetMouseButtonDown(0))
            {
                GameObject rb = Instantiate(m_Tile);
                m_Field.AddTileTo(rb, x, y);

                Transform t = rb.GetComponent<Transform>();
                //Material m = rb.GetComponent<Renderer>().material;
                //if (x == 0 || y == 0)
                //{
                //    m.color = Color.white;
                //}
                //else
                //{
                //    m.color = new Color((255 / (float)x) / 255, (255 / (float)y) / 255, 0.0f, 1.0f);
                //}
                if (t != null)
                {

                    t.position = pos;
                }
                GameObject.Destroy(m_GhostTile);
                m_GhostTile = null;
            }
        }
        else
        {
            GameObject.Destroy(m_GhostTile);
            m_GhostTile = null;
        }
    }

    bool checkValidStreet(int x, int y)
    {
        return true;
    }
}
