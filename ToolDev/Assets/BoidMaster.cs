using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidMaster : MonoBehaviour
{
    public GameObject boidPrefab;


    public List<Boid> boidList = new List<Boid>();
    public float boidAmount;
    public float SeparationRange = 0.5f;
    public float pushValue;
    public float cohesionValue = 10;
    public float SpeedLimiter = 100;

    void Start()
    {
        
    }

    void Update()
    {
       MoveBoids();
    }
    
    public void spawnBoids()
    {
        for (int i = 0; i < boidAmount; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(1, 150), Random.Range(1, 100), Random.Range(1, 100));
            Boid b = Instantiate(boidPrefab, randomPosition, Quaternion.identity).GetComponent<Boid>();
            boidList.Add(b);
            
        }
    }

    public void DeleteBoids()
    {

        

        for (int i = boidList.Count -1; i >= 0; i--)
        {
            //Boid removeBoid = boidList[0];
            //boidList.Remove(removeBoid);
            //Destroy(removeBoid);
            
            Boid removeBoid = boidList[i];
            boidList.Remove(removeBoid);
            Destroy(removeBoid.gameObject);
            
            Debug.Log("Destroyed");

        }
        

    }

    public void replaceBoids(GameObject newPrefab)
    {
        boidPrefab = newPrefab;
    }

    private void MoveBoids()
    {
        Vector3 v1, v2, v3, v4;

        foreach (Boid b in boidList)
        {
            v1 = Cohesion(b);
            v2 = Separation(b);
            v3 = Alignment(b);
            v4 = BindPosition(b);

            b.velocity += v1 + v2 + v3 + v4;
            limit_velocity(b);
         
            b.position += b.velocity * Time.deltaTime;
        }
    }

    private void limit_velocity(Boid boid)
    {

        if(boid.velocity.magnitude > SpeedLimiter)
        {
            boid.velocity = (boid.velocity / boid.velocity.magnitude) * SpeedLimiter;
        }

        boid.transform.position += boid.velocity * Time.deltaTime;

        //boid.transform.position = Vector3.Lerp(transform.position, boid.velocity, 10000 * Time.deltaTime);

        //boid.transform.rotation = Quaternion.LookRotation(boid.velocity);

    }

    public Vector3 Cohesion(Boid boid)
    {
        Vector3 PerceivedCentre = Vector3.zero;

        foreach (Boid b in boidList)
        {
            if (b != boid)
            {
                PerceivedCentre = PerceivedCentre + b.position;
            }
        }


        PerceivedCentre = PerceivedCentre / (boidList.Count - 1);

        return (PerceivedCentre - boid.position).normalized / cohesionValue;
    }

    public Vector3 Separation(Boid boid)
    {
        Vector3 seperatePosition = Vector3.zero;

        foreach (Boid b in boidList)
        {
            if (b != boid)
            {
                if (Vector3.Magnitude(b.position - boid.position) < SeparationRange)
                {
                    seperatePosition = seperatePosition - (b.position - boid.position) * 1.5f;
                }
            }
        }
        
        return seperatePosition;
    }

    public Vector3 Alignment(Boid boid)
    {
        Vector3 PerceivedVelocity = Vector3.zero;

        foreach (Boid b in boidList)
        {
            if (b != boid)
            {
                PerceivedVelocity = PerceivedVelocity + b.velocity;
            }
        }

        PerceivedVelocity = PerceivedVelocity / (boidList.Count - 1);


        return Vector3.Normalize(PerceivedVelocity - boid.velocity) / 8;

    }

    private Vector3 BindPosition(Boid b)
    {
        int Xmin, Xmax, Ymin, Ymax;

        Vector3 screenPush = Vector3.zero;

        if (b.position.x < 150)
        {
            screenPush.x = pushValue;
        }
        else if (b.position.x > 1000)
        {
            screenPush.x = -pushValue;
        }

        if (b.position.y < 0)
        {
            screenPush.y = pushValue;
        }
        else if (b.position.y > 600)
        {
            screenPush.y = -pushValue;
        }


        if (b.position.z < 0)
        {
            screenPush.z = pushValue;
        }
        else if (b.position.z > 1000)
        {
            screenPush.z = -pushValue;
        }


        return screenPush;
    }

    public void ChangeSpeed(float newSpeed)
    {
        SpeedLimiter = newSpeed;
    }
}