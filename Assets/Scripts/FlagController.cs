using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour, IResetable
{
    public Dictionary<string, bool> resetableFlags = new Dictionary<string, bool>();
    public Dictionary<string, bool> flags = new Dictionary<string, bool>();


    private void Awake()
    {
        ResetFlags();

        flags.Add("player_knows_about_wrench", false);
        flags.Add("player_knows_about_invasion", false);
        flags.Add("player_knows_about_alien_on_roof", false);
        flags.Add("player_knows_where_is_duct_tape", false);
        flags.Add("player_knows_NASA_number", false);
        flags.Add("player_knows_alien_language", false);
    }

    public void ResetFlags()
    {
        resetableFlags.Clear();

        //resetableFlags

        resetableFlags.Add("npc_tv_talked", false);
        resetableFlags.Add("tv_repaired", false);
        resetableFlags.Add("phone_repaired",false);

        resetableFlags.Add("fire_alarm", false);
    }


    public void SetFlag(string flagName, bool value, bool isResetable = false)
    {
        if (resetableFlags.ContainsKey(flagName))
        {
            resetableFlags[flagName] = value;
        }

        if (flags.ContainsKey(flagName))
        {
            flags[flagName] = value;
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

    public void ResetObject()
    {
        ResetFlags();
    }
}