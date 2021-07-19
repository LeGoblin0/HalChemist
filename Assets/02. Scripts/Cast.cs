using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cast : MapSyS
{
    [Range(-1,1)]
    public int Flip = 1;
    public int ItemCode = 0;
    public int[] ItemNum;
    public override void SpeO()
    {
        base.SpeO();
        if (SaveOn) return;

        int aniNum = GameSystem.instance.MapSSS(ObjCode);
        for (int i = 0; i < ItemNum[aniNum]; i++)
        {
            Invoke("MakeItem", Random.Range(0, .7f));
        }

        HHIITime = .2f;
    }
    float HHIITime = 0;
    private void Update()
    {
        if (HHIITime > 0)
        {
            ImgRander.material.SetFloat("_HitCol", 0.55f);
            HHIITime -= Time.deltaTime;
        }
        else
        {
            ImgRander.material.SetFloat("_HitCol", 0);
        }
    }

    void MakeItem()
    {
        Transform item = Instantiate(GameSystem.instance.ItemPre[ItemCode]);
        item.position = transform.position + new Vector3(0, 0, -.001f);
        if (item.GetComponent<Rigidbody2D>() != null) item.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(1f, 4f) * Flip, Random.Range(1f, 3f));
    }
}
