using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public Transform CameraTargetTR => cameraTargetTR;
	[SerializeField] private Transform cameraTargetTR = null;
	public List<Transform> TrackingTargets => trackingTargets;
	[SerializeField] private List<Transform> trackingTargets = null;
	private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
	[SerializeField] private Camera mainCamera;
	[SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera = null;
	private float minZoom = 10f;
	private float edgeOfTheScreenOffset = 5f;

	private void Start() {
		StartCoroutine(CameraTrackingTargetUpdate());
	}

	private IEnumerator CameraTrackingTargetUpdate() {
		while (true) {
			yield return waitForFixedUpdate;
			int targets = trackingTargets.Count;
			if (targets > 0) {
				Vector3 averagePosition = Vector3.zero;
				for (int i = 0; i < targets; i++) {
					averagePosition += trackingTargets[i].position;
				}
				averagePosition /= targets;
				cameraTargetTR.position = averagePosition;
			}
			//Update zoom level

			//Get max distance between players
			//Hack: Cheating by using just two players
			//float cameraHeight = mainCamera.orthographicSize * 2f;
			//float cameraWidth = cameraHeight * mainCamera.aspect;

			float playersCurrentMaxDistance = Math.Abs(trackingTargets[0].position.x - trackingTargets[1].position.x);
			float orthographicSizeFromDistance = playersCurrentMaxDistance / mainCamera.aspect / 2f;
			cinemachineVirtualCamera.m_Lens.OrthographicSize = Math.Max(minZoom, orthographicSizeFromDistance + edgeOfTheScreenOffset);
		}
	}
}