using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSBulletController : MonoBehaviour
{
    private float _bulletSpeed = 20;
    private Color _paintColor;
    private float _destroyAfterTime = 3;
    private float _radius = 1;
    private float _strength = 1;
    private float _hardness = 1;
    private Vector3 _previousPosition = Vector3.zero;

    private void Start()
    {

    }

    private void Update()
    {
        Vector3 pos = transform.position + transform.forward * _bulletSpeed * Time.deltaTime;
        _previousPosition = transform.position;
        transform.position = pos;

        RaycastHit hit;

        if (Physics.Raycast(pos, (pos - _previousPosition).normalized, out hit))
        {
            if (Vector3.Distance(transform.position, hit.point) > Vector3.Distance(pos, _previousPosition))
                return;
            transform.position = hit.point;
            Paintable p = hit.collider.GetComponent<Paintable>();
            if (p != null)
            {
                PaintManager.instance.paint(p, hit.point, _radius, _hardness, _strength, _paintColor);
                Destroy(gameObject);
            }
        }
    }

    public void SetBulletData(Color paintColor, float bulletSpeed, float destroyAfterTime, float radius, float strength, float hardness)
    {
        _paintColor = paintColor;
        _bulletSpeed = bulletSpeed;
        _destroyAfterTime = destroyAfterTime;
        _radius = radius;
        _strength = strength;
        _hardness = hardness;
        Destroy(gameObject, _destroyAfterTime);
        paintColor += new Color(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), paintColor.a);
        GetComponent<MeshRenderer>().material.color = paintColor;
    }
}
