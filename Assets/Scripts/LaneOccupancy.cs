using System.Collections.Generic;

public static class LaneOccupancy
{
    private static HashSet<string> blocked = new HashSet<string>();

    public static void BlockLane(int lane, float z)
    {
        blocked.Add(Key(lane, z));
    }

    public static bool IsBlocked(int lane, float z)
    {
        return blocked.Contains(Key(lane, z));
    }

    private static string Key(int lane, float z)
    {
        return lane + "_" + UnityEngine.Mathf.RoundToInt(z);
    }

    public static void Clear()
    {
        blocked.Clear();
    }
}