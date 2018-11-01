using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_4 : Enemy {

    private Vector3 p0, p1;
    private float timeStart;
    private float duration = 4;

    void Start()    {
        p0 = p1 = pos;

        InitMovement();
    }

    void InitMovement()    {
        p0 = p1;

        float widMinRad = bndCheck.camWeidth - bndCheck.radius;
        float hgtMindRad = bndCheck.camHeight - bndCheck.radius;

        p1.x = Random.Range(-widMinRad, widMinRad);
        p1.y = RandomRange(-hgtMindRad, hgtMindRad);

        timeStart = Time.time;
    }


}
