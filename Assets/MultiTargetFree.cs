using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class MultiTargetFree : MonoBehaviour
{
    /* The possible target objects */
    public Transform[] targets = new Transform[0];

    /* The paths currently being calculated or have been calculated */
    private Path[] lastPaths;

    /* Number of paths completed so far */
    private int numCompleted = 0;

    /* The calculated best path */
    private Path bestPath = null;

    public float[] pathLength;
    /* Searches for the closest target */
    void Start()
    {
        SearchClosest();
    }

    /* Call to update the bestPath with the closest path one of the targets.
     * It will take a few frames for this to be calculated, the bestPath variable will be null in the meantime
     */
    public void SearchClosest()
    {
        //If any paths are currently being calculated, cancel them to avoid wasting processing power
        if (lastPaths != null)
            for (int i = 0; i < lastPaths.Length; i++)
                lastPaths[i].Error();

        //Create a new lastPaths array if necessary (can reuse the old one?)
        if (lastPaths == null || lastPaths.Length != targets.Length) lastPaths = new Path[targets.Length];

        //Reset variables
        bestPath = null;
        numCompleted = 0;

        //Loop through the targets
        for (int i = 0; i < targets.Length; i++)
        {
            //Create a new path to the target
            ABPath p = ABPath.Construct(transform.position, targets[i].position, OnTestPathComplete);

            /* Before version 3.2
             * Path p = new Path (transform.position,targets[i].position, OnTestPathComplete);
             */

            lastPaths[i] = p;

            //Request the path to be calculated, might take a few frames
            //This will call OnTestPathComplete when completed
            AstarPath.StartPath(p);
        }
    }

    /* Called when each path completes calculation */
    public void OnTestPathComplete(Path p)
    {
        if (p.error)
        {
            Debug.LogWarning("One target could not be reached!\n" + p.errorLog);
        }

        //Make sure this path is not an old one
        for (int i = 0; i < lastPaths.Length; i++)
        {
            if (lastPaths[i] == p)
            {
                numCompleted++;

                if (numCompleted >= lastPaths.Length)
                {
                    CompleteSearchClosest();
                }
                return;
            }
        }
    }

    /* Called when all paths have completed calculation */
    public void CompleteSearchClosest()
    {
        //Find the shortest path
        Path shortest = null;
        float shortestLength = float.PositiveInfinity;

        //Loop through the paths
        for (int i = 0; i < lastPaths.Length; i++)
        {
            //Get the total length of the path, will return infinity if the path had an error
            float length = lastPaths[i].GetTotalLength();
            //pathLength[i] = lastPaths[i].GetTotalLength();
            if (shortest == null || length < shortestLength)
            {
                shortest = lastPaths[i];
                shortestLength = length;
            }
        }

        Debug.Log("Found a path which was " + shortestLength + " long");

        bestPath = shortest;
    }

    public void Update()
    {
        //Highlight the best path in the editor when it is found
        if (bestPath != null && bestPath.vectorPath != null)
        {
            for (int i = 0; i < bestPath.vectorPath.Count - 1; i++)
            {
                Debug.DrawLine(bestPath.vectorPath[i], bestPath.vectorPath[i + 1], Color.green);
            }

            /* Before version 3.2
             * for (int i=0;i<bestPath.vectorPath.Length-1;i++) {
             */
        }
    }
}
