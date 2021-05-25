using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperBtn : MonoBehaviour
{
	[SerializeField] GameObject btn1;
	[SerializeField] GameObject btn2;
	[SerializeField] GameObject jmp1;
	[SerializeField] GameObject jmp2;
	[SerializeField] float offset = 1f;
	[SerializeField] GameObject playerClone;
	[SerializeField] float jumpHeight = 5f;
	float time = 0f;

	void Start() {/*only here to have an enablable script*/}

	void Update()
	{
		time += Time.deltaTime;
		// le bouton peut etre active et on saute dessus
		if (btn1.activeSelf && btn1.GetComponent<ButtonColision>().GetCollideState()) {
			btn1.SetActive(false); // on cache le bouton
			btn2.SetActive(true); // pour monte qu'il est active
			time = 0f;
		} else if (time >= 0.5f && btn2.activeSelf && !btn2.GetComponent<ButtonColision>().GetCollideState()) {
			btn1.SetActive(true);
			btn2.SetActive(false);
			if (isPlayerCloneOnJumper())
				StartCoroutine(activateJumper());
		}
	}

	IEnumerator activateJumper()
	{
		jmp2.SetActive(true);
		jmp1.SetActive(false);

		yield return new WaitForSeconds(1f);
		Rigidbody2D playerRb = playerClone.GetComponent<Rigidbody2D>();
		playerRb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);

		yield return new WaitForSeconds(0.25f);
		jmp1.SetActive(true);
		jmp2.SetActive(false);
	}

	bool isPlayerCloneOnJumper()
	{
		MirorLevelMoveClone2 _MirorLevelMoveCloneScript = playerClone.GetComponent<MirorLevelMoveClone2>();
		Vector3 playerClonePos = playerClone.transform.position;
		Vector3 jumperPos = jmp1.transform.position;

		if (_MirorLevelMoveCloneScript.isGrounded() && playerClonePos.x >= jumperPos.x - offset && playerClonePos.x <= jumperPos.x + offset)
			return true;

		return false;
	}
}