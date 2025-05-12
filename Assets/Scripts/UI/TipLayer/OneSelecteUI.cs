using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OneSelecteUI : UIViewBase
{
    public override UILAYER Layer => UILAYER.M_TIP_LAYER;

    public new Camera camera;
    public Image select;
    
    public bool isSelect;
    public bool isForceSelect;

    SkillSelectorBase curParent;

    protected override void Start()
    {
        base.Start();
        select = transform.Find("select").GetComponent<Image>();
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        isSelect = false;
    }

    public override void Init(string _name, object[] args)
    {
        if(args[0] is SkillSelectorBase parent)
        {
            curParent = parent;
        }
        if(args[1] is bool isForce)
        {
            isForceSelect = isForce;
        }
    }

    private void Update()
    {
        OnSelect();
        if (select != null && !isSelect)
        {
            select.rectTransform.anchoredPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if(isForceSelect)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(select != null && !isSelect)
                {
                    EventCenter.Broadcast(EventDefine.ON_FOLLOWER_SKILL_SELECT_FINISH);
                }
            }
        }
    }

    public void OnSelect()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit2D = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit2D.collider != null)
        {
            BaseCharacter character = hit2D.collider.GetComponent<BaseCharacter>();
            curParent.UpdateSelector(character);
            isSelect = true;
            Vector3 pos = camera.WorldToScreenPoint(hit2D.transform.position);
            select.rectTransform.anchoredPosition = new Vector2(pos.x , pos.y);
        }
    }

}
