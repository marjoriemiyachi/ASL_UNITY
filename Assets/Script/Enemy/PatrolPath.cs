using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    public enum PathType { Loop, ReverseWhenComplete }

    public Transform[] waypoints;// array de cordenadas que irão definir os pontos
    public PathType pathType = PathType.Loop;

    private int index = 0;//qual waypoint está
    private int direction = 1; // 1 = frente, -1 = volta

    public Vector3 GetCurrentWaypoint() //volta a posição atual
    {
        if (waypoints == null || waypoints.Length == 0)
            return transform.position; 
        return waypoints[index].position;// usa o index como referencia

    }

    public Vector3 GetNextWaypoint()// avança para o próximo ponto
    {
        if (waypoints == null || waypoints.Length == 0)
            return transform.position; 

        index += direction;

        if (pathType == PathType.Loop)// a para b para c para d para e
        {
            index %= waypoints.Length;// % é referente ao loop
        }
        else if (pathType == PathType.ReverseWhenComplete)// faz o caminho reverso de a para b para c para d
        {
            if (index >= waypoints.Length || index < 0)//o index vale Zero então ele deve ser maior igual a Zero para movimentar OU deve ser menor que zero 
                //direção pode ser entre 1 e -1
            {
                direction *= -1;
                index += direction * 2;
            }
        }

        return waypoints[index].position;// retorna a posição atual
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