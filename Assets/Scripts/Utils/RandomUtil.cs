using System.Collections.Generic;
using UnityEngine;

public class RandomUtil 
{
    public static int RandomInt(int mi , int mx)
    {
        return Random.Range(mi, mx);    // [mi , mx)
    }

    public static float RandomFloat(float range , bool neg = true)
    {
        if(neg) return RandomFloat(-range, range);
        else return RandomFloat(0, range);
    }

    public static float RandomFloat(float left , float right)
    {
        return Random.Range(left, right);
    }

    // 带限制的Get
    public static T GetRandomValueInList<T>(List<T> list , int mxCnt)
    {
        if(list == null || list.Count == 0)
        {
            throw new System.ArgumentNullException("list is null or empty");
        }
        int idx = RandomInt(0, Mathf.Min(list.Count , mxCnt));
        return list[idx];
    }

    // 从List内任意取出一个
    public static T GetRandomValueInList<T>(List<T> list)
    {
        if(list == null || list.Count == 0)
        {
            throw new System.ArgumentNullException("list is null or empty");
        }
        int idx = RandomInt(0, list.Count);
        return list[idx];
    }

}
