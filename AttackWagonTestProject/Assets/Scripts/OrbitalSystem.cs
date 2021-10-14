using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalSystem : MonoBehaviour
{
    public GameObject baseSphere;
    public List<GameObject> orbitingSpheres;
    public float speed = 1f;

    private Vector3 baseSphereCenter;
    [SerializeField]
    private float angle = 0f;

    // Start is called before the first frame update
    void Start()
    {
        baseSphereCenter = baseSphere.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Keep angle between 0 and 2PI radians
        angle = (angle + Time.deltaTime) % (2.0f * Mathf.PI);
        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);
        float z = 0;
        Vector3 distanceBetweenSpheres = orbitingSpheres[0].transform.position - baseSphereCenter;
        orbitingSpheres[0].transform.position = new Vector3(x,y,z) ; 
        
    }
}
