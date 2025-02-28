using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// UGUI矩形流光特效
/// </summary>
public class UIFlowingLight : RawImage
{
    [Tooltip("边框尺寸")]
    public float borderSize = 10;
    [Tooltip("流光移动速度")]
    public float speed = 0.3f;

    private void Update()
    {
        var uvr = uvRect;
        uvr.x -= Time.deltaTime * speed;
        if (uvr.x < -1) uvr.x += 1;
        uvRect = uvr;
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        Texture tex = mainTexture;
        vh.Clear();
        if (tex == null)
            return;
        var r = GetPixelAdjustedRect();
        //var scaleX = tex.width * tex.texelSize.x;
        //var scaleY = tex.height * tex.texelSize.y;
        var uvr = uvRect;

        //构造一个矩形外边框(Border)
        //外边框
        var outv = new Vector4(r.x, r.y, r.x + r.width, r.y + r.height);
        //内边框
        var inv = new Vector4(r.x + borderSize, r.y + borderSize, r.x + r.width - borderSize, r.y + r.height - borderSize);
        //内框周长
        var C = (inv.z + inv.w) * 2;
        //内/外框右上角uv值
        var uv_ru = inv.z / C;
        //内/外框右下角uv值
        var uv_rd = (inv.z + inv.w) / C;
        //内/外框左下角uv值
        var uv_ld = (inv.z + inv.w + inv.z) / C;

        //注意：UV的原点坐标在纹理左下角
        //外矩形-左下角, 0
        vh.AddVert(new Vector3(outv.x, outv.y), color, new Vector2((uv_ld + uvr.x) * uvr.width, 1));
        //外矩形-左上角, 1
        vh.AddVert(new Vector3(outv.x, outv.w), color, new Vector2(uvr.x * uvr.width, 1));
        //外矩形-右上角, 2
        vh.AddVert(new Vector3(outv.z, outv.w), color, new Vector2((uv_ru + uvr.x) * uvr.width, 1));
        //外矩形-右下角, 3
        vh.AddVert(new Vector3(outv.z, outv.y), color, new Vector2((uv_rd + uvr.x) * uvr.width, 1));

        //内矩形-左下角, 4
        vh.AddVert(new Vector3(inv.x, inv.y), color, new Vector2((uv_ld + uvr.x) * uvr.width, 0));
        //内矩形-左上角, 5
        vh.AddVert(new Vector3(inv.x, inv.w), color, new Vector2(uvr.x * uvr.width, 0));
        //内矩形-右上角, 6
        vh.AddVert(new Vector3(inv.z, inv.w), color, new Vector2((uv_ru + uvr.x) * uvr.width, 0));
        //内矩形-右下角, 7
        vh.AddVert(new Vector3(inv.z, inv.y), color, new Vector2((uv_rd + uvr.x) * uvr.width, 0));

        //内矩形-左上角, 8
        vh.AddVert(new Vector3(inv.x, inv.w), color, new Vector2((1 + uvr.x) * uvr.width, 0));
        //外矩形-左上角, 9
        vh.AddVert(new Vector3(outv.x, outv.w), color, new Vector2((1 + uvr.x) * uvr.width, 1));

        //上边框
        vh.AddTriangle(5, 1, 2);
        vh.AddTriangle(2, 6, 5);
        //右边框
        vh.AddTriangle(6, 2, 3);
        vh.AddTriangle(3, 7, 6);
        //下边框
        vh.AddTriangle(7, 3, 0);
        vh.AddTriangle(0, 4, 7);
        //左边框
        vh.AddTriangle(4, 0, 9);
        vh.AddTriangle(9, 8, 4);

    }
}