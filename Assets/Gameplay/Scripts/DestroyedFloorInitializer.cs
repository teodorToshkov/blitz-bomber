using UnityEngine;
using System.Collections;

public class DestroyedFloorInitializer : MonoBehaviour
{
	public Sprite sprite;
	public Color color;

	[SerializeField]
	private SpriteRenderer frontSprite;
	[SerializeField]
	private SpriteRenderer backSprite;

	[SerializeField]
	private MeshRenderer frontMesh;
	[SerializeField]
	private MeshRenderer backMesh;

	// Use this for initialization
	void Start ()
	{
		frontSprite.sprite = sprite;
		backSprite.sprite = sprite;

		frontMesh.material.color = color;
		backMesh.material.color = color;
	}
}
