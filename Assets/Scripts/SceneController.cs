using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    public ImageSynthesis synth;
    public GameObject prefabs;
    public GameObject created;
    public GameObject planer;
    public GameObject lighter;
    public int max = 1;
    public int frameCount;
    private Object[] textures;
    public int trainingImages;
    public int valImages;
    public bool save = false;
    public bool grayscale = false;

    public float minLightIntensity = 0.5f;
    public float maxLightIntensity = 2.0f;
    public float minVariationFactor = 0.9f; //1.0 => no variation
    public float maxVariationFactor = 1.1f; //...
    public float speedFactor= 2.0f; //1.0 => normal speed
    private float variationFactor;



    void Start()
    {
        created = new GameObject();
        textures = Resources.LoadAll("Textures", typeof(Texture2D));


    }

    // Update is called once per frame
    void Update() {
        if (frameCount < trainingImages + valImages) {
            if (frameCount % 1 == 0) {
                //GenerateRandom();

                Generatefoot();

                Debug.Log($"FrameCount: {frameCount}");
            }
            frameCount++;
            if (save) {
                if (frameCount < trainingImages) {
                    string filename = $"{frameCount.ToString().PadLeft(5, '0')}";
                    synth.Save(filename, 225, 225, "captures/train", 2);
                }
                else if (frameCount < trainingImages + valImages) {
                    int valFrameCount = frameCount - trainingImages;
                    string filename = $"{valFrameCount.ToString().PadLeft(5, '0')}";
                    synth.Save(filename, 225, 225, "captures/val", 2);
                }

            }
        }
    }

    void Generatefoot(){

    	if(created != null){
    		Destroy(created); 
    	}
    	GameObject prefab = prefabs;
    	float newX,newY,newZ,newRotx,newRoty,newRotz,newRotx1,newRoty1,newRotz1;

    	newX = Random.Range(2.0f,-2.0f);
    	newY = Random.Range(2.0f,-2.0f);
    	newZ = Random.Range(2.0f,-2.0f);
    	Vector3 newPos = new Vector3(newX,newY,newZ);
        

    	newRotx = Random.Range(-100f,-80f);
    	newRoty = Random.Range(-80f,+80f);
    	newRotz = Random.Range(-30f,+30f);
        newRotx1 = Random.Range(-10f,-20f);
        newRoty1 = Random.Range(-20f,+20f);
        newRotz1 = Random.Range(-10f,+10f);

    	var newObj = Instantiate(prefab,newPos,Quaternion.identity);
    	created = newObj;
    	newObj.transform.Rotate(newRotx, newRoty,newRotz);
    	newObj.SetActive(true);
    	float sx = Random.Range(.7f,1.5f);
    	Vector3 newscale = new Vector3(sx,sx,sx);
    	newObj.transform.localScale = newscale;
    	//Vector3
          // Slightly randomize the effect every deltaTime to create a flicker effect
        variationFactor = Random.Range(minVariationFactor, maxVariationFactor);
    	//planer.Component.GetComponent<Renderer>().material = 
        //lighter.transform.Rotate(newRotx1, newRoty1,newRotz1);
        //lighter.transform.Position(newX, newY,newZ);
        gameObject.GetComponent<Light>().intensity = minLightIntensity+ Mathf.PingPong(Time.time * speedFactor, maxLightIntensity) * variationFactor;
    	Texture2D texture = (Texture2D)textures[Random.Range(0, textures.Length)];
    	planer.GetComponent<Renderer>().material.mainTexture = texture;
    	
        synth.OnSceneChange(grayscale);
    }

}
