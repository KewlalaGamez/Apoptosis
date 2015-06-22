﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AcidicArea : BaseArea {

    public float damagePerSecond;
    public float pendingConvertDelayed = 5.0f;

	public override void Awake() {
        base.Awake();

    }

	public override void Start () {
        base.Start();

	}
	
	public override void Update () {
        base.Update();

	}

	public override void FixedUpdate() {
        base.FixedUpdate();

    }

	public override void LateUpdate() {
        base.LateUpdate();

    }

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Unit") {
            BaseCell enterCell = collider.gameObject.GetComponent<BaseCell>();
            if (!enterCell) return;

            switch (enterCell.celltype) {
                case CellType.STEM_CELL: {
                    StemCell stemCell = enterCell.GetComponent<StemCell>();
                    StopCoroutine("ReadyToConvert");
                    StartCoroutine(ReadyToConvert(pendingConvertDelayed, stemCell));
                    break;
                }
                    
                case CellType.HEAT_CELL:
                    break;
                case CellType.COLD_CELL:
                    break;
                case CellType.HEAT_CELL_TIRE2:
                    break;
                case CellType.COLD_CELL_TIRE2:
                    break;
                case CellType.ACIDIC_CELL:
                    break;
                case CellType.ALKALI_CELL:
                    break;
                case CellType.CANCER_CELL:
                    break;
                default:
                    break;
            }
        }
    }


    void OnTriggerStay(Collider collider) {

    }

    void OnTriggerExit(Collider collider) {
        if (collider.gameObject.tag == "Unit") {
            BaseCell enterCell = collider.gameObject.GetComponent<BaseCell>();
            if (!enterCell) return;

            switch (enterCell.celltype) {
                case CellType.STEM_CELL: {
                    StemCell stemCell = collider.gameObject.GetComponent<StemCell>();
                    if (stemCell) {
                        stemCell.isInAcidic = false;
                    }
                    break;
                }
                    
                case CellType.HEAT_CELL:
                    break;
                case CellType.COLD_CELL:
                    break;
                case CellType.HEAT_CELL_TIRE2:
                    break;
                case CellType.COLD_CELL_TIRE2:
                    break;
                case CellType.ACIDIC_CELL:
                    break;
                case CellType.ALKALI_CELL:
                    break;
                case CellType.CANCER_CELL:
                    break;
                default:
                    break;
            }
        }
    }

    // Coroutine function
    IEnumerator ReadyToConvert(float delayed, StemCell stemCell) {
        yield return new WaitForSeconds(delayed);
        // to toggle on the pending converting
        if (stemCell)
            stemCell.isInAcidic = true;
    }
}