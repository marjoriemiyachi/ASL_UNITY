using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    public enum PathType { Loop, ReverseWhenComplete }

    public Transform[] waypoints;
    public PathType pathType = PathType.Loop;

    private int index = 0;
    private int direction = 1; // 1 = frente, -1 = volta

    public Vector3 GetCurrentWaypoint()
    {
        if (waypoints == null || waypoints.Length == 0)
            return transform.position; 
        return waypoints[index].position;
    }

    public Vector3 GetNextWaypoint()
    {
        if (waypoints == null || waypoints.Length == 0)
            return transform.position; 

        index += direction;

        if (pathType == PathType.Loop)
        {
            index %= waypoints.Length;
        }
        else if (pathType == PathType.ReverseWhenComplete)
        {
            if (index >= waypoints.Length || index < 0)
            {
                direction *= -1;
                index += direction * 2;
            }
        }

        return waypoints[index].position;
    }

    private void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        Gizmos.color = Color.white;
        for (int i = 0; i < waypoints.Length - 1; i++)
            Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);

        if (pathType == PathType.Loop)
            Gizmos.DrawLine(waypoints[waypoints.Length - 1].position, waypoints[0].position);

        Gizmos.color = Color.red;
        foreach (Transform wp in waypoints)
            Gizmos.DrawSphere(wp.position, 0.2f);
    }
}