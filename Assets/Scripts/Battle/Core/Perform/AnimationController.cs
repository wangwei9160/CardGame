using System.Collections;
using UnityEngine;

public class AnimationController : ManagerBase<AnimationController>
{
    public void Update()
    {
        BattleManager.Instance?.BaseBattlePlayer?.PerformModule.PlayNextAnimation();
    }

    public string artName;
    public Vector3 pos;

    public void ShowArtFunction(string _name, Vector3 transform)
    {
        artName = _name;
        pos = transform;
        StartCoroutine(ArtShow());
    }

    public IEnumerator ArtShow()
    {
        Vector3 position = pos;
        GameObject obj = ResourceUtil.GetArtShow(artName);
        Debug.Log(position + "__" + artName);
        GameObject prefab = Instantiate(obj, new Vector3(position.x, position.y + 0.3f, position.z), Quaternion.identity);
        SpriteRenderer spriteRenderer = prefab.GetComponent<SpriteRenderer>();
        float transparency = spriteRenderer.color.a;
        Animator animator = prefab.GetComponent<Animator>();
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5);
        prefab.GetComponent<AudioSource>().PlayOneShot(prefab.GetComponent<AudioSource>().clip);
        float duration = animator.GetCurrentAnimatorStateInfo(0).length;
        while (transparency > 0)
        {
            transparency -= 0.1f;
            spriteRenderer.color = new Color(1, 1, 1, transparency);
            yield return new WaitForSeconds(duration * 0.1f);
        }
        Destroy(prefab);
    }
}