using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

	public Card card;

	public TextMeshProUGUI cardName;
	public TextMeshProUGUI type;
	public TextMeshProUGUI abilities;

	public TextMeshProUGUI mana;
	public TextMeshProUGUI attack;
	public TextMeshProUGUI health;

	public Image cardArt;
    public Image cardImage;

    private Canvas _canvas;
    private RectTransform _canvasRectTransform;
    private Vector3 startDragPos;

    // Use this for initialization
    void Start () {
			cardName.text  = card.cardName;
			type.text      = card.type;
			abilities.text = card.abilities;

			mana.text   = card.manaCost.ToString();
			attack.text = card.attackPower.ToString();
			health.text = card.health.ToString();

			cardArt.sprite = card.cardArt;
		}

		/**
		 *  @function DealDamage
		 *  @desc     Deal damage to this card. Destroy if out of health.
		 */
		public void DealDamage(int damageDealt){
			this.card.health = this.card.health - damageDealt;

			if(this.card.health <= 0){
				Destroy(gameObject); // Destroy card if out of health
			}

			// Update health display
			health.text = this.card.health.ToString();
		}

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_canvas == null)
        {
            _canvas = FindInParents<Canvas>(gameObject);
            _canvasRectTransform = _canvas.GetComponent<RectTransform>();

        }

        if (_canvas == null)
            return;

        //make it so we can see if we are over the board
        startDragPos = transform.position;
        SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData data)
    {
        SetDraggedPosition(data);
    }

    private void SetDraggedPosition(PointerEventData data)
    {

        var rt = this.GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_canvasRectTransform, data.position, data.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos;
            //rt.rotation = m_DraggingPlane.rotation;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);

        //check if over board area

        bool setPlacement = false;

        if (raycastResults.Count > 0)
        {
            foreach (var result in raycastResults)
            {
								// Ignore result if it is the card we are dragging
								if (result.gameObject == gameObject) {
                  continue;
                }

								//check if pawn here
								var cardComponent = result.gameObject.GetComponent<CardView>();
								if (cardComponent != null) {
									cardComponent.DealDamage(this.card.attackPower);
								}

								// check if we can drop card here
                var boardPlacement = result.gameObject.GetComponent<PawnBoardVisual>();
                if (boardPlacement != null)
                {

                    if (!CheckGameRules() || !boardPlacement.isPlayerBoard)
                    {
                        break;
                    }

                    transform.SetParent(boardPlacement.PawnArea);
                    setPlacement = true;
                    break;
                }


            }
        }

        if (setPlacement == false)
        {
            transform.position = startDragPos;

            //quick placement reset
            transform.SetParent(transform.parent);
        }


    }

    bool CheckGameRules()
    {
        //check mana eventually

        //check if valid from hand, etc
        return true;
    }


    public static T FindInParents<T>(GameObject go) where T : Component
    {
        if (go == null) return null;
        var comp = go.GetComponent<T>();

        if (comp != null)
            return comp;

        Transform t = go.transform.parent;
        while (t != null && comp == null)
        {
            comp = t.gameObject.GetComponent<T>();
            t = t.parent;
        }
        return comp;
    }

}
