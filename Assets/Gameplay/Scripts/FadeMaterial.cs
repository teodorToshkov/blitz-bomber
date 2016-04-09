using UnityEngine;
using System.Collections;

/*! \brief This script makes the transparency of the material of a __GameObject__ to fade to 0
 */
public class FadeMaterial : MonoBehaviour
{
	public float startOpacity = 1;	//!< the initial opacity of the __GameObject__
	public float speed = 1;			//!< the rate at which we are going to fade the opacity of the material of the __GameObject__

	private Material material;		//!< stores the __Renderer__ of the __GameObject__
	private Renderer render;		//!< stores the __Renderer__ of the __GameObject__
	private Color color;			//!< stores the color we currently have

	//! We initialize our variables
	void Start ()
	{
		render = GetComponent<Renderer> ();
		material = render.material;
		color = material.color;
		color.a = startOpacity;
		material.color = color;
		render.material = material;
	}

	//! Lerps the alpha of the material towards 0
	void Update ()
	{
		color.a = Mathf.Lerp (color.a, 0, speed * GameManager.gamePlaySpeed * Time.deltaTime);
		material.color = color;
		render.material = material;
	}
}
