using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using ToolBox.Pools;


public static class TimedFunction
{

    public delegate bool OnFunction(); //Return success
    public delegate void OnFunction1(); //Return nothing
    public delegate T OnFunction2<T>(); //Return any value

    public static event OnFunction onExecuteCommand;
    public static event OnFunction1 onExecuteCommand1;

    public static void StartBool(OnFunction _function, float _time = 1f)
    {
        GameObject go = new GameObject();
        go.name = _function.Method.Name;
        var timedfunction = go.AddComponent<TimedFunctionScript>();
        timedfunction.function = _function;
        timedfunction.time = _time;

    }

    public static void Start(OnFunction1 _function, float _time = 1f)
    {
        GameObject go = new GameObject();
        go.name = _function.Method.Name;
        var timedfunction = go.AddComponent<TimedFunctionScript>();
        timedfunction.function1 = _function;
        timedfunction.time = _time;

    }

    /// <summary>
    /// Determine the 'T' type. Do not use it to wait return! Use async/ienumerator!
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_function"></param>
    /// <param name="_time"></param>
    public static void Start<T>(OnFunction2<T> _function, float _time = 1f)
    {
        //GameObject go = new GameObject();
        //go.name = _function.Method.Name;
        //var timedfunction = go.AddComponent<TimedFunctionScript>();
        //timedfunction.function2 = _function;
        //timedfunction.time = _time;

    }
  
}

public static class FactoryUtilities
{

    public static void Shuffle<T>(this IList<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }


    public static string AttemptRetrieveString(this StringStringDictionary dictionary, string key, string defaultValue = "")
    {
        dictionary.TryGetValue(key, out string z);

        if (string.IsNullOrEmpty(z))
        {
            return defaultValue;
        }

        return z;
    }


    public static bool AttemptRetrieveBool(this StringStringDictionary dictionary, string key, bool defaultValue = false)
    {
        dictionary.TryGetValue(key, out string z);

        if (bool.TryParse(z, out bool result))
        {
            return result;
        }
        else
        {
            return defaultValue;
        }
    }

    public static float AttemptRetrieveFloat(this StringStringDictionary dictionary, string key, float defaultValue = 0f)
    {
        dictionary.TryGetValue(key, out string z);

        if (float.TryParse(z, out float result))
        {
            return result;
        }
        else
        {
            return defaultValue;
        }
    }

    public static float AttemptRetrieveInt(this StringStringDictionary dictionary, string key, int defaultValue = 0)
    {
        dictionary.TryGetValue(key, out string z);

        if (int.TryParse(z, out int result))
        {
            return result;
        }
        else
        {
            return defaultValue;
        }
    }



    public static double ConvertToUnixTimestamp(this DateTime date)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        TimeSpan diff = date.ToUniversalTime() - origin;
        return Math.Floor(diff.TotalSeconds);
    }

    public static string MoneyString(this int money)
    {
        return money < 0 ? $"-$ {Mathf.Abs(money)}" : $"+$ {Mathf.Abs(money)}";
    }

    public static void EnableGameobject(this GameObject go, bool active)
    {
        if (go.activeSelf != active) go.SetActive(active);
    }
    public static bool IsParentOf(this GameObject origin, GameObject parent)
    {
        Transform t = origin.transform;
        if (t == parent.transform) return true;

        while (t.parent != null)
        {
            t = t.parent;

            if (t == parent.transform) return true;
        }


        return false;
    }

    public static Vector3 RandomizedOffset(this Vector3 pos, Vector3 range)
    {
        Vector3 pos1 = pos;
        pos1.x += UnityEngine.Random.Range(-range.x, range.x);
        pos1.y += UnityEngine.Random.Range(-range.y, range.y);
        pos1.z += UnityEngine.Random.Range(-range.z, range.z);


        return pos1;
    }

    public static Vector3Int RoundVector3Int(this Vector3 pos)
    {
        Vector3Int pos1 = new Vector3Int();
        pos1.x = Mathf.RoundToInt(pos1.x);
        pos1.y = Mathf.RoundToInt(pos1.y);
        pos1.z = Mathf.RoundToInt(pos1.z);

        return pos1;
    }

    public static Vector3Int FloorVector3Int(this Vector3 pos)
    {
        Vector3Int pos1 = new Vector3Int();
        pos1.x = Mathf.FloorToInt(pos1.x);
        pos1.y = Mathf.FloorToInt(pos1.y);
        pos1.z = Mathf.FloorToInt(pos1.z);

        return pos1;
    }

    public static void DestroyAndClearList<T>(this List<T> list) where T : Component
    {
        foreach (var wall in list)
        {
            if (wall == null) continue;
            UnityEngine.Object.Destroy(wall.gameObject);
        }

        list.Clear();
    }



    public static void DestroyAndClearList_1(this List<GameObject> list)
    {
        foreach (var wall in list)
        {
            if (wall == null) continue;
            UnityEngine.Object.Destroy(wall.gameObject);
        }

        list.Clear();
    }

    public static void ReleasePoolObject(this List<GameObject> list)
    {
        foreach (var wall in list)
        {
            if (wall == null) continue;
            wall.Release();
        }

        list.Clear();
    }

    public static void ReleasePoolObject<T>(this List<T> list) where T : Component
    {
        foreach (var wall in list)
        {
            if (wall == null) continue;
            wall.gameObject.Release();
        }

        list.Clear();
    }

    public static List<T> DuplicateList<T>(this List<T> list)
    {
        var newList = new List<T>();
        newList.AddRange(list);

        return newList;
    }

    public static string Capitalize(this string input)
    {
        if (String.IsNullOrEmpty(input))
            throw new ArgumentException("ARGH!");
        return input.First().ToString().ToUpper() + String.Join("", input.ToLower().Skip(1));
    }


    /// <summary>
    /// Check if direction is only either Left, Right, Down, Up, Forward or Back.
    /// </summary>
    /// <param name="v3"></param>
    /// <returns></returns>
    public static bool OnlySingleDirection(this Vector3 v3)
    {
        if (Mathf.Abs(v3.x) > 0.1f)
        {
            if (Mathf.Abs(v3.y) <= float.Epsilon && Mathf.Abs(v3.z) <= float.Epsilon)
            {
                return true;
            }
        }
        if (Mathf.Abs(v3.y) > 0.1f)
        {
            if (Mathf.Abs(v3.x) <= float.Epsilon && Mathf.Abs(v3.z) <= float.Epsilon)
            {
                return true;
            }
        }
        if (Mathf.Abs(v3.z) > 0.1f)
        {
            if (Mathf.Abs(v3.x) <= float.Epsilon && Mathf.Abs(v3.y) <= float.Epsilon)
            {
                return true;
            }
        }

        return false;
    }

    public static T Clone<T>(this T source)
    {
        var serialized = JsonConvert.SerializeObject(source, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        return JsonConvert.DeserializeObject<T>(serialized);
    }

    public static bool Contains(this LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

    public static T GetComponentInParent<T>(this GameObject origin)
    {
        Transform t = origin.transform;

        while (t.parent != null)
        {
            t = t.parent;

            T component = t.gameObject.GetComponent<T>();

            if (component != null) return component;
        }


        return default(T);
    }

    public static Transform[] GetAllParents(this GameObject origin)
    {
        List<Transform> parents = new List<Transform>();
        Transform t = origin.transform;

        while (t.parent != null)
        {
            t = t.parent;

            parents.Add(t);
        }


        return parents.ToArray();
    }


    public static T GetComponentThenParent<T>(this GameObject origin)
    {
        T component = origin.GetComponent<T>();

        if (component != null) return component;
        else component = origin.GetComponentInParent<T>();

        if (component != null) return component;

        return default(T);
    }



    public static T GetComponentThenChild<T>(this GameObject origin)
    {
        T component = origin.GetComponent<T>();

        if (component != null) return component;
        else component = origin.GetComponentInChildren<T>();

        if (component != null) return component;

        return default(T);
    }


    public static List<GameObject> AllChilds(this GameObject root)
    {
        List<GameObject> result = new List<GameObject>();
        if (root.transform.childCount > 0)
        {
            foreach (Transform VARIABLE in root.transform)
            {
                Searcher(result, VARIABLE.gameObject);
            }
        }
        return result;
    }
    private static void Searcher(List<GameObject> list, GameObject root)
    {
        list.Add(root);
        if (root.transform.childCount > 0)
        {
            foreach (Transform VARIABLE in root.transform)
            {
                Searcher(list, VARIABLE.gameObject);
            }
        }
    }

    public static void DisableObjectTimer(this GameObject gameObject, float time = 5f)
    {
        var deactivator = gameObject.GetComponent<TimedObjectDeactivator>();
        if (deactivator == null) deactivator = gameObject.AddComponent<TimedObjectDeactivator>();
        deactivator.allowRestart = true;
        deactivator.Timer = time;
    }
}
