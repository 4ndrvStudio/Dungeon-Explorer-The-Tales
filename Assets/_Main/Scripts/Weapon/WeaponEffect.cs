using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEffect : MonoBehaviour
{
     private const int NUM_VERTICES = 12;

    [SerializeField]
    [Tooltip("The empty game object located at the tip of the blade")]
    private GameObject _tip = null;

    [SerializeField]
    [Tooltip("The empty game object located at the base of the blade")]
    private GameObject _base = null;

    public GameObject _trailMesh;

    [SerializeField]
    [Tooltip("The number of frame that the trail should be rendered for")]
    private int _trailFrameLength = 3;

       [SerializeField]
    [Tooltip("The amount of force applied to each side of a slice")]
    private Mesh _mesh;
    private Vector3[] _vertices;
    private int[] _triangles;
    private int _frameCount;
    private Vector3 _previousTipPosition;
    private Vector3 _previousBasePosition;


     void Start()
    {

        _mesh = new Mesh();

        _trailMesh.GetComponent<MeshFilter>().mesh = _mesh;

        _vertices = new Vector3[_trailFrameLength * NUM_VERTICES];
        _triangles = new int[_vertices.Length];

        //Set starting position for tip and base
         _previousTipPosition =  _tip.transform.position;
      
        _previousBasePosition = _base.transform.position;

    }
    
    void LateUpdate()
    {
        //Reset the frame count one we reach the frame length
        if(_frameCount == (_trailFrameLength * NUM_VERTICES))
        {
            _frameCount = 0;
        }
        //Draw first triangle vertices for back and front
        _vertices[_frameCount] = _base.transform.localPosition;
        _vertices[_frameCount + 1] =  _tip.transform.localPosition;
        _vertices[_frameCount + 2] = _previousTipPosition;
        _vertices[_frameCount + 3] = _base.transform.localPosition;
        _vertices[_frameCount + 4] = _previousTipPosition;
        _vertices[_frameCount + 5] =  _tip.transform.localPosition;

        //Draw fill in triangle vertices
        _vertices[_frameCount + 6] = _previousTipPosition;
        _vertices[_frameCount + 7] = _base.transform.localPosition;
        _vertices[_frameCount + 8] = _previousBasePosition;
        _vertices[_frameCount + 9] = _previousTipPosition;
        _vertices[_frameCount + 10] = _previousBasePosition;
        _vertices[_frameCount + 11] = _base.transform.localPosition;

        //Set triangles
        _triangles[_frameCount] = _frameCount;
        _triangles[_frameCount + 1] = _frameCount + 1;
        _triangles[_frameCount + 2] = _frameCount + 2;
        _triangles[_frameCount + 3] = _frameCount + 3;
        _triangles[_frameCount + 4] = _frameCount + 4;
        _triangles[_frameCount + 5] = _frameCount + 5;
        _triangles[_frameCount + 6] = _frameCount + 6;
        _triangles[_frameCount + 7] = _frameCount + 7;
        _triangles[_frameCount + 8] = _frameCount + 8;
        _triangles[_frameCount + 9] = _frameCount + 9;
        _triangles[_frameCount + 10] = _frameCount + 10;
        _triangles[_frameCount + 11] = _frameCount + 11;

        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;

        //Track the previous base and tip positions for the next frame
        _previousTipPosition =  _tip.transform.position;
        _previousBasePosition = _base.transform.position;
        _frameCount += NUM_VERTICES;
    }




}
