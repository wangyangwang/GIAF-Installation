using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateCube : MonoBehaviour {

	public List<HyperCube> myCubes;

	Object prefadCube;
	List<GameObject> allLoaded;
 	List<Vector3> rotateSpeed;
	int rotatingCubeIndex = 0;

	public int cubeNumber;
	public float generationFieldRange;
	public float[] sizeRange;
	
	// Use this for initialization
	void Start () {
		allLoaded = new List<GameObject>();
		rotateSpeed = new List<Vector3>();
		prefadCube = Resources.Load("Cube_Wireframe");

		myCubes = new List<HyperCube>();

		for(int i=0; i<cubeNumber; i++){
			HyperCube temp = new HyperCube(new Vector3(Random.Range(-generationFieldRange,generationFieldRange),Random.Range(-generationFieldRange,generationFieldRange),Random.Range(-generationFieldRange,generationFieldRange)),Random.Range(0.3f,0.9f),prefadCube,Random.Range(sizeRange[0],sizeRange[1]),3);
			temp.instantParent("number "+ i);
			temp.generate();
			myCubes.Add(temp);
		}

	}

	
	// Update is called once per frame
	void Update () {


		for(int i=0; i<myCubes.Count-1;i++){
			myCubes[i].rotate();
		}



	}
	

	public class HyperCube {
		public Vector3 position;
		public GameObject me;
		public GameObject[] children;
		public Vector3 rotationDirection;
		public float decayRate;
		public int cubesNumber;
		public Object prefabObject;
		public float scaleMultiplier;
		public float[] rotateSpeeds;

		
		public HyperCube(Vector3 _pos, float _decayRate, Object _prefabObject, float _scale, int _cubesN) {
			cubesNumber = _cubesN;
			prefabObject = _prefabObject;
			position = _pos;
			scaleMultiplier = _scale;
			decayRate = _decayRate;
			rotateSpeeds = new float[cubesNumber];
			children = new GameObject[cubesNumber];

			for(int i=0; i<cubesNumber-1;i++){
				rotateSpeeds[i] = Random.Range(1.0f,5.0f);
			}
			rotationDirection = new Vector3(Random.Range(-0.1f,0.1f),Random.Range(-0.1f,0.1f),Random.Range(-0.1f,0.1f));

		}

		public void instantParent(string name){
			me = new GameObject(name);
		}
		
		public void generate(){
			float scale = 1.0f;
			for(int i=0;i<cubesNumber;i++){
				GameObject newobj = (GameObject)Instantiate(prefabObject,position,Quaternion.identity);
				newobj.transform.localScale = new Vector3(scale * scaleMultiplier, scale * scaleMultiplier, scale * scaleMultiplier);
				scale *= decayRate;
				newobj.transform.parent = me.transform;
				children[i] = newobj;
			}
		}

		public void rotate(){
			for(int i=0; i< children.Length - 1; i++){
				children[i].transform.RotateAround(position, rotationDirection ,Time.deltaTime * rotateSpeeds[i]);
			}
		}
	}
}
