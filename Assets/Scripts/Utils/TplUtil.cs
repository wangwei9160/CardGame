using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class TplUtil 
{
    private static Dictionary<Type , object> singletons = new Dictionary<Type , object>();

    public static Dictionary<int, V> GetTplDic<T, V>() where T : BaseTpl<V>, new() where V : BaseTplInfo, new()
    {
        if (!singletons.ContainsKey(typeof(T)))
        {
            T tpl = new T();
            TextAsset jsonFile = Resources.Load<TextAsset>(tpl.TplPath);
            if (jsonFile == null)
            {
                return null;
            }
            string json = jsonFile.text;
            tpl.List = JsonConvert.DeserializeObject<List<V>>(json);
            foreach (V item in tpl.List)
            {
                //Debug.Log(string.Format("item-{0}-{1}", item.ID, item.Name));
                tpl.Dic.Add(item.ID, item);
            }
            singletons[typeof(T)] = tpl;
        }
        return (singletons[typeof(T)] as T).Dic;
    }



}
