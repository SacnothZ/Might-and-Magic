using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class WaterShapeController : MonoBehaviour
{
    [Header("Spring Settings")]
    [SerializeField] private float springStiffness = 0.1f;
    [SerializeField] private float dampening = 0.03f;
    [SerializeField] private float spread = 0.006f;

    [Header("Wave Settings")]
    [SerializeField][Range(1, 100)] private int wavesCount = 6;     // Range creates slider on unity

    [Header("References")]
    [SerializeField] private SpriteShapeController spriteShapeController;
    [SerializeField] private GameObject wavePointPrefab;
    [SerializeField] private GameObject wavePointsContainer;

    private readonly List<WaterSpring> springs = new();

    private void Awake()
    {
        if (spriteShapeController==null)
        {
            spriteShapeController=GetComponent<SpriteShapeController>();
        }
    }
    

    private IEnumerator Start()
    {
        yield return null;  
        GenerateWater();
    }

    private void FixedUpdate()
    {
        if (springs.Count==0)
            return;

        foreach (WaterSpring spring in springs)
        {
            spring.WaveSpringUpdate(springStiffness, dampening);
        }

        UpdateSprings();

        foreach (WaterSpring spring in springs)
        {
            spring.WavePointUpdate();
        }

        spriteShapeController.RefreshSpriteShape();
    }

    private void GenerateWater()
    {
        if (spriteShapeController == null)
        {
            
            return;
        }

        Spline waterSpline = spriteShapeController.spline;

        ClearOldSprings();

        RemoveOldWavePoints(waterSpline);

        CreateWavePoints(waterSpline);

        CreateSprings(waterSpline);

        spriteShapeController.RefreshSpriteShape();
    }

    private void ClearOldSprings()
    {
        springs.Clear();

        if (wavePointsContainer == null)
            return;

        for (int i = wavePointsContainer.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(wavePointsContainer.transform.GetChild(i).gameObject);
        }
    }

    private void RemoveOldWavePoints(Spline waterSpline)
    {
        
        while (waterSpline.GetPointCount() > 4) // Leave only the 4 corner points
        {
            waterSpline.RemovePointAt(2);
        }
    }

    private void CreateWavePoints(Spline waterSpline)
    {
        Vector3 topLeft = waterSpline.GetPosition(1);
        Vector3 topRight = waterSpline.GetPosition(2);

        float width=topRight.x-topLeft.x;
        float spacing=width/(wavesCount + 1);

        for (int i=wavesCount; i>0; i--)
        {
            int insertIndex=2;
            float xPos=topLeft.x+spacing*i;

            Vector3 newPoint = new Vector3(xPos,topLeft.y,topLeft.z);

            waterSpline.InsertPointAt(insertIndex, newPoint);
            waterSpline.SetHeight(insertIndex, 0.1f);
            waterSpline.SetCorner(insertIndex, false);
            waterSpline.SetTangentMode(insertIndex, ShapeTangentMode.Continuous);
        }
    }

    private void CreateSprings(Spline waterSpline)
    {
        for (int i=0; i<=wavesCount+1; i++)
        {
            int splineIndex = i + 1;
            Smoothen(waterSpline, splineIndex);

            GameObject wavePoint = Instantiate(wavePointPrefab,wavePointsContainer.transform);

            wavePoint.transform.localPosition=waterSpline.GetPosition(splineIndex);

            WaterSpring spring=wavePoint.GetComponent<WaterSpring>();

            spring.Init(spriteShapeController);
            springs.Add(spring);
        }
    }

    private void UpdateSprings()
    {
        int count = springs.Count;

        float[] leftDeltas = new float[count];
        float[] rightDeltas = new float[count];

        for (int i=0; i<count; i++)
        {
            if (i>0)
            {
                leftDeltas[i] = spread*(springs[i].height-springs[i-1].height);

                springs[i-1].velocity += leftDeltas[i];
            }

            if (i<count-1)
            {
                rightDeltas[i] =spread * (springs[i].height-springs[i+1].height);

                springs[i+1].velocity += rightDeltas[i];
            }
        }
    }

    private void Smoothen(Spline waterSpline, int index)
    {
        Vector3 position = waterSpline.GetPosition(index);
        Vector3 positionPrev = position;
        Vector3 positionNext = position;

        if (index>1)
        {
            positionPrev = waterSpline.GetPosition(index-1);
        }

        if (index<waterSpline.GetPointCount() -2)
        {
            positionNext = waterSpline.GetPosition(index+1);
        }

        Vector3 forward = transform.forward;

        float scale = Mathf.Min((positionNext - position).magnitude,(positionPrev - position).magnitude)*0.33f;

        Vector3 leftTangent =(positionPrev-position).normalized * scale;
        Vector3 rightTangent =(positionNext-position).normalized * scale;

        SplineUtility.CalculateTangents(position,positionPrev,positionNext,forward,scale,out rightTangent,out leftTangent);

        waterSpline.SetLeftTangent(index, leftTangent);
        waterSpline.SetRightTangent(index, rightTangent);
    }

    public void Splash(int index, float speed)
    {
        if (index>=0 && index<springs.Count)
        {
            springs[index].velocity+= speed;
        }
    }
}