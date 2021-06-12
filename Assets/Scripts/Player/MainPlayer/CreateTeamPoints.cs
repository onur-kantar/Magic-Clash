using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTeamPoints : MonoBehaviour
{
    public List<TeammatePoint> teammatePoints;

    void Awake()
    {
        Create();
    }
    void Create()
    {
        teammatePoints = new List<TeammatePoint>();
        int i = 2;
        for (int x = -i; x <= i; x++)
        {
            for (int z = -i; z <= i; z++)
            {
                TeammatePoint newTeammatePoint = new TeammatePoint();
                newTeammatePoint.Point = new Vector3(x, 0, z);
                newTeammatePoint.id = Mathf.Abs(x) > Mathf.Abs(z) ? Mathf.Abs(x) : Mathf.Abs(z);
                //GameObject go = Instantiate(Player, transform.position + newTeammatePoint.Point, Quaternion.identity);
                //go.transform.parent = transform;
                //ewTeammatePoint.teamMate = go;
                teammatePoints.Add(newTeammatePoint);
            }
        }
        teammatePoints.Sort(delegate (TeammatePoint x, TeammatePoint y)
        {
            return x.id.CompareTo(y.id);
        });

        List<Vector3> sequentialTeamPoints = new List<Vector3>();
        foreach (TeammatePoint teammatePoint in teammatePoints)
        {
            if (!sequentialTeamPoints.Contains(teammatePoint.Point))
            {
                sequentialTeamPoints.Add(teammatePoint.Point);
                if (!sequentialTeamPoints.Contains(-teammatePoint.Point))
                {
                    sequentialTeamPoints.Add(-teammatePoint.Point);
                }
            }
        }
        for (int j = 0; j < sequentialTeamPoints.Count; j++)
        {
            teammatePoints[j].Point = sequentialTeamPoints[j];
        }
    }
}
