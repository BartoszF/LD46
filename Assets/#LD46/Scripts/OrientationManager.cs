using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OrientationManager : MonoBehaviour
{

    public OrientationEnum currentOrientation;


    public BuildableEntity buildableEntity;

    private Dictionary<OrientationEnum, OrientationData> orientationToSprites = new Dictionary<OrientationEnum, OrientationData>();

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Start is called before the first frame Ndate
    void Start()
    {
        foreach (var item in buildableEntity.orientationDataArr)
        {
            orientationToSprites.Add(item.orientation, item);
        }

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
    }

    public void rotateRight() {
        if (currentOrientation == OrientationEnum.N) {
            setOrientation(OrientationEnum.E);
        } else if (currentOrientation == OrientationEnum.E) {
            setOrientation(OrientationEnum.S);
        } else if (currentOrientation == OrientationEnum.S) {
            setOrientation(OrientationEnum.W);
        } else if (currentOrientation == OrientationEnum.W) {
            setOrientation(OrientationEnum.N);
        }
        // spriteRenderer.transform.rotation = new Quaternion(0,0,0,0);
    }

    public void rotateLeft() {
        if (currentOrientation == OrientationEnum.N) {
            setOrientation(OrientationEnum.W);
        } else if (currentOrientation == OrientationEnum.W) {
            setOrientation(OrientationEnum.S);
        } else if (currentOrientation == OrientationEnum.S) {
            setOrientation(OrientationEnum.E);
        } else if (currentOrientation == OrientationEnum.E) {
            setOrientation(OrientationEnum.N);
        }
        // spriteRenderer.transform.rotation = new Quaternion(0,0,0,0);
    }

    void setOrientation(OrientationEnum orientation) {
        spriteRenderer.sprite = orientationToSprites[orientation].spriteToRender;
        if (animator != null) {
            animator.runtimeAnimatorController = orientationToSprites[orientation].controller;
        }
        currentOrientation = orientation;
    }
}


public enum OrientationEnum {
    N,
    S,
    W, 
    E
}