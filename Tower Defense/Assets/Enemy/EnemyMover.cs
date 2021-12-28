using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<WayPoint> Path = new List<WayPoint>();
    [SerializeField][Range(0,5)] float speed = 1f;

    Enemy enemy;
    // Start is called before the first frame update
    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
        
    }
    void Start()
    {
        enemy = GetComponent<Enemy>(); 
    }
    void FindPath()
    {
        Path.Clear();
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Path");
        foreach (GameObject i in waypoints)
        {
            WayPoint waypoint = i.GetComponent<WayPoint>();

            if (waypoint != null)
            {
                Path.Add(waypoint);
            }
        }
    }
    void ReturnToStart()
    {
        transform.position = Path[0].transform.position;
    }

    void FinishPath()
    {
        enemy.StilldGold();
        gameObject.SetActive(false);
    }
    IEnumerator FollowPath()
    {
        foreach (WayPoint waypoint in Path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while (travelPercent < 1)
            {
                travelPercent += (Time.deltaTime * speed);
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }

}
