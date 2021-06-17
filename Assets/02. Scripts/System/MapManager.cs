using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    // Start is called before the first frame update

    Transform MiniMapTr;
    Transform PlyMiniMapTr;

    void Start()
    {
        if (EnemySetPre == null) EnemySetPre = new GameObject("Enemy").transform;
        EnemySetPre.transform.parent = transform;
        EnemySetPre.gameObject.SetActive(false);
        if (MiniMapTr != null) MiniMapTr = transform.parent.GetComponent<MMini>().MMM;
        if (PlyMiniMapTr != null) PlyMiniMapTr = transform.parent.GetComponent<MMini>().MMMp;
    }
    public Transform EnemySetPre;
    [HideInInspector]
    public Transform EEE;

    public bool XLock;
    public bool YLock;
    public void MakeEEE()
    {
        EEE = Instantiate(EnemySetPre);
        EEE.position = transform.position;
        EEE.gameObject.SetActive(true);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (MiniMapTr == null)
            {
                MiniMapTr = transform.parent.GetComponent<MMini>().MMM;
            //Debug.Log(MiniMapTr);
            }
            if (PlyMiniMapTr == null)
            {
                PlyMiniMapTr = transform.parent.GetComponent<MMini>().MMMp;
            //Debug.Log(PlyMiniMapTr);
            }

            //Debug.Log(transform.parent.GetComponent<MMini>().MMM);
            PlyMiniMapTr.position = MiniMapTr.GetChild(transform.GetSiblingIndex()).position;
        }
    }
}
