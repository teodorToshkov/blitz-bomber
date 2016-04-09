using UnityEngine;
using System.Collections;

/*! \brief This script makes the transparency of the material of a __GameObject__ to fade to 0
 */
public class FadeMaterial : MonoBehaviour
{
	public float startOpacity = 1;
	public float speed = 1;

	private Vector3 targetScale;
	private Material material;
	private Renderer render;
	private Color color;

	void Start ()
	{
		render = GetComponent<Renderer> ();
		material = render.material;
		color = material.color;
		color.a = startOpacity;
		material.color = color;
		render.material = material;
	}

	// Update is called once per frame
	void Update ()
	{
		color.a = Mathf.Lerp (color.a, 0, speed * GameManager.gamePlaySpeed * Time.deltaTime);
		material.color = color;
		render.material = material;
	}
}
