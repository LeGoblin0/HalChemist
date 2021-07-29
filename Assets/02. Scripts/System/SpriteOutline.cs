using UnityEngine;


//[ExecuteInEditMode]
public class SpriteOutline : MonoBehaviour
{
    public Color color = Color.white;

    [Range(0, 16)]
    public int outlineSize = 0;

    private SpriteRenderer spriteRenderer;
    private void Start()
    {

        if (Set)
        {
            Set = false;
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.material = GameSystem.instance.OutLineMaterial;
            UpdateOutline(true);
        }
    }
    bool Set = true;
    //void OnEnable()
    //{
    //    if (Set)
    //    {
    //        Set = false;
    //        spriteRenderer = GetComponent<SpriteRenderer>();
    //        //spriteRenderer.material = GameSystem.instance.OutLineMaterial;
    //    }

    //    UpdateOutline(true);
    //}

    void OnDisable()
    {
        if (Set)
        {
            Set = false;
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.material = GameSystem.instance.OutLineMaterial;
            UpdateOutline(true);
        }
        UpdateOutline(false);
    }

    void Update()
    {
        if (Set)
        {
            Set = false;
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.material = GameSystem.instance.OutLineMaterial;
            UpdateOutline(true);
        }
        UpdateOutline(true);
    }

    void UpdateOutline(bool outline)
    {
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(mpb);
        mpb.SetFloat("_Outline", outline ? 1f : 0);
        mpb.SetColor("_OutlineColor", color);
        mpb.SetFloat("_OutlineSize", outlineSize);
        spriteRenderer.SetPropertyBlock(mpb);
    }
}