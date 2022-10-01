using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    public Dictionary<string, bool> resetableFlags = new Dictionary<string, bool>();
    public Dictionary<string, bool> flags = new Dictionary<string, bool>();


    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        resetableFlags.Clear();
        flags.Clear();

        resetableFlags.Add("npc_tv_talked", false);
    }


    public void SetFlag(string flagName, bool value)
    {
        if (resetableFlags.ContainsKey(flagName))
        {
            resetableFlags[flagName] = value;
        }
        else
        {
            resetableFlags.Add(flagName, value);
        }
    }

    public bool GetFlag(string flagName)
    {
        if (resetableFlags.ContainsKey(flagName))
        {
            return resetableFlags[flagName];
        }
        else
        {
            if (flags.ContainsKey(flagName))
            {
                return flags[flagName];
            }
            else
            {
                flags.Add(flagName, false);
                return false;
            }
        }
    }
}