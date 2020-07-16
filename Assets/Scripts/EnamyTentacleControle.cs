using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnamyTentacleControle : MonoBehaviour
{
    [HideInInspector]
    public Vector3 v1;
    [HideInInspector]
    public Vector3 v2;
    public Vector3 direction;
    public LineRenderer lr;
    public Transform start;
    public Transform target;
    public float a, c;
    private Vector3[]curve;
    public int numPoints=3;
    [Range(0.2f,0.8f)]
    public float targetMoveThresholdeModifier=0.6f;
    private Vector3 optimalPosition;
    [HideInInspector]
    public float speed;
    bool isMoving;
    float startTime;
    float moveTime;
    Vector3 oldPosition;
    public AnimationCurve yMovmentperStep;
    public float moveheight;
    Vector3 moveStart,moveEnd;

    private void Start()
    {
        target.position = v2;
        start.position = v1;
        optimalPosition = target.position- start.position ;
        
        isMoving = false;
        CalculateCurve();
        oldPosition = transform.position;
    }

    private Vector3 CalculateMidPoint()
    {
        
        float b = (-start.position+ target.position).magnitude;
        if (b < a + c)
        {
            /*Vector3 vVector = (target.localPosition - start.localPosition).normalized;
            Vector3 uVector = Vector3.up;
            Vector3 nhilf1 = new Vector3(
                vVector.y * uVector.z - vVector.z * uVector.y,
                vVector.z * uVector.x - vVector.x * uVector.z,
                vVector.x * uVector.y - vVector.y * uVector.x
            );
            nhilf1 = new Vector3(
                vVector.y * nhilf1.z - vVector.z * nhilf1.y,
                vVector.z * nhilf1.x - vVector.x * nhilf1.z,
                vVector.x * nhilf1.y - vVector.y * nhilf1.x
            );
            Vector3 nhilf2;

            float s = 0.5f * (a + b + c);
            float hc = (2/c) * Mathf.Sqrt(s*(s-a)*(s-b)*(s-c));

            nhilf2 = start.localPosition + vVector * Mathf.Sqrt(c * c - hc * hc);
            Vector3 mid = nhilf2 + nhilf1.normalized * hc;
            Debug.DrawLine(target.position, start.position);
            Debug.DrawLine(target.position, mid + mid.normalized * hc);
            Debug.DrawLine(start.position, mid + mid.normalized * hc);
            */
            float d = (target.position - start.position).magnitude;
            Vector3 schnittkreisMittelpunkt = start.position + ((a * a - c * c) / (2 * d * d) + 0.5f) * (target.position - start.position);
            float schnittkreisRadius = Mathf.Sqrt(a*a-Mathf.Pow((schnittkreisMittelpunkt-start.position).magnitude, 2));

            //(1 / 2 * d) * Mathf.Sqrt(2 * d * d - Mathf.Pow((a * a - c * c),2)  - d * d * d * d);
            Vector3 s = (target.position-start.position).normalized;
            Vector3 stuetzvektor = new Vector3(s.y*direction.z -s.z*direction.y, s.z * direction.x - s.x * direction.z, s.x * direction.y - s.y * direction.x);

            Vector3 m1 = schnittkreisMittelpunkt + stuetzvektor.normalized * schnittkreisRadius;
            Vector3 m2 = schnittkreisMittelpunkt - stuetzvektor.normalized * schnittkreisRadius;
            return m1.y < m2.y ? m2 : m1;
        }
        else
            return start.position + (target.position - start.position).normalized * 0.5f;
    }

    private void CalculateCurve()
    {
        curve = BezierCurve.DrawCurve(start,target,CalculateMidPoint(),numPoints);
        lr.positionCount = numPoints;
        lr.SetPositions(curve);
       
    }

    private void Update()
    {
        if ((transform.position.x != oldPosition.x|| transform.position.z != oldPosition.z))
        {
            Vector3 v = (oldPosition - transform.position);
            target.position += new Vector3(v.x,0,v.z);
            
        }

        moveTime = speed;
        if (!isMoving&&((target.position - start.position).magnitude > ((a + c) * targetMoveThresholdeModifier)))
        {
            startTime = Time.time;
            isMoving = true;
            
            moveStart = oldPosition;
            moveEnd = new Vector3(start.position.x+optimalPosition.x,target.position.y,start.position.z+optimalPosition.z);

            target.GetComponent<Rigidbody>().useGravity = false;
        }
        
        if (isMoving)
        {
            if (Time.time - startTime < moveTime)
            {

                float lerpValue = (Time.time - startTime) / moveTime;
                target.position = Vector3.Lerp(moveStart, moveEnd, lerpValue);
                target.position = new Vector3(target.position.x, target.position.y + yMovmentperStep.Evaluate(lerpValue * yMovmentperStep[yMovmentperStep.length - 1].time) * moveheight, target.position.z);

            }
            else
            {
                isMoving = false;
                target.position = moveEnd;
                target.GetComponent<Rigidbody>().useGravity = true;
            }
        }
      
        CalculateCurve();
        oldPosition = transform.position;
    }

   
}
