using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using Object = UnityEngine.Object;

namespace UnityEditor.UI
{
    static internal class LayoutsMenuOptions
    {
		#region Unity Builder properties  - Do not change unless UI Source (Editor\MenuOptions) changes
		private const string kUILayerName = "UI";
		private const float kWidth = 160f;
		private const float kThickHeight = 30f;
		private const float kThinHeight = 20f;
		private const string kStandardSpritePath = "UI/Skin/UISprite.psd";
		private const string kBackgroundSpriteResourcePath = "UI/Skin/Background.psd";
		private const string kInputFieldBackgroundPath = "UI/Skin/InputFieldBackground.psd";
		private const string kKnobPath = "UI/Skin/Knob.psd";
		private const string kCheckmarkPath = "UI/Skin/Checkmark.psd";

		private static Vector2 s_ThickGUIElementSize = new Vector2(kWidth, kThickHeight);
		private static Vector2 s_ThinGUIElementSize = new Vector2(kWidth, kThinHeight);
		private static Vector2 s_ImageGUIElementSize = new Vector2(100f, 100f);
		private static Color s_DefaultSelectableColor = new Color(1f, 1f, 1f, 1f);
		private static Color s_TextColor = new Color(50f / 255f, 50f / 255f, 50f / 255f, 1f);
		#endregion
		#region Unity Builder methods - Do not change unless UI Source (Editor\MenuOptions) changes
		private static void SetPositionVisibleinSceneView(RectTransform canvasRTransform, RectTransform itemTransform)
		{
			// Find the best scene view
			SceneView sceneView = SceneView.lastActiveSceneView;
			if (sceneView == null && SceneView.sceneViews.Count > 0)
				sceneView = SceneView.sceneViews[0] as SceneView;

			// Couldn't find a SceneView. Don't set position.
			if (sceneView == null || sceneView.camera == null)
				return;

			// Create world space Plane from canvas position.
			Vector2 localPlanePosition;
			Camera camera = sceneView.camera;
			Vector3 position = Vector3.zero;
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRTransform, new Vector2(camera.pixelWidth / 2, camera.pixelHeight / 2), camera, out localPlanePosition))
			{
				// Adjust for canvas pivot
				localPlanePosition.x = localPlanePosition.x + canvasRTransform.sizeDelta.x * canvasRTransform.pivot.x;
				localPlanePosition.y = localPlanePosition.y + canvasRTransform.sizeDelta.y * canvasRTransform.pivot.y;

				localPlanePosition.x = Mathf.Clamp(localPlanePosition.x, 0, canvasRTransform.sizeDelta.x);
				localPlanePosition.y = Mathf.Clamp(localPlanePosition.y, 0, canvasRTransform.sizeDelta.y);

				// Adjust for anchoring
				position.x = localPlanePosition.x - canvasRTransform.sizeDelta.x * itemTransform.anchorMin.x;
				position.y = localPlanePosition.y - canvasRTransform.sizeDelta.y * itemTransform.anchorMin.y;

				Vector3 minLocalPosition;
				minLocalPosition.x = canvasRTransform.sizeDelta.x * (0 - canvasRTransform.pivot.x) + itemTransform.sizeDelta.x * itemTransform.pivot.x;
				minLocalPosition.y = canvasRTransform.sizeDelta.y * (0 - canvasRTransform.pivot.y) + itemTransform.sizeDelta.y * itemTransform.pivot.y;

				Vector3 maxLocalPosition;
				maxLocalPosition.x = canvasRTransform.sizeDelta.x * (1 - canvasRTransform.pivot.x) - itemTransform.sizeDelta.x * itemTransform.pivot.x;
				maxLocalPosition.y = canvasRTransform.sizeDelta.y * (1 - canvasRTransform.pivot.y) - itemTransform.sizeDelta.y * itemTransform.pivot.y;

				position.x = Mathf.Clamp(position.x, minLocalPosition.x, maxLocalPosition.x);
				position.y = Mathf.Clamp(position.y, minLocalPosition.y, maxLocalPosition.y);
			}

			itemTransform.anchoredPosition = position;
			itemTransform.localRotation = Quaternion.identity;
			itemTransform.localScale = Vector3.one;
		}

		private static GameObject CreateUIElementRoot(string name, MenuCommand menuCommand, Vector2 size)
		{
			GameObject parent = menuCommand.context as GameObject;
			if (parent == null || parent.GetComponentInParent<Canvas>() == null)
			{
				parent = GetOrCreateCanvasGameObject();
			}
			GameObject child = new GameObject(name);

			Undo.RegisterCreatedObjectUndo(child, "Create " + name);
			Undo.SetTransformParent(child.transform, parent.transform, "Parent " + child.name);
			GameObjectUtility.SetParentAndAlign(child, parent);

			RectTransform rectTransform = child.AddComponent<RectTransform>();
			rectTransform.sizeDelta = size;
			if (parent != menuCommand.context) // not a context click, so center in sceneview
			{
				SetPositionVisibleinSceneView(parent.GetComponent<RectTransform>(), rectTransform);
			}
			Selection.activeGameObject = child;
			return child;
		}

		static GameObject CreateUIObject(string name, GameObject parent)
		{
			GameObject go = new GameObject(name);
			go.AddComponent<RectTransform>();
			GameObjectUtility.SetParentAndAlign(go, parent);
			return go;
		}

		static public void AddCanvas(MenuCommand menuCommand)
		{
			var go = CreateNewUI();
			GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
			if (go.transform.parent as RectTransform)
			{
				RectTransform rect = go.transform as RectTransform;
				rect.anchorMin = Vector2.zero;
				rect.anchorMax = Vector2.one;
				rect.anchoredPosition = Vector2.zero;
				rect.sizeDelta = Vector2.zero;
			}
			Selection.activeGameObject = go;
		}

		static public GameObject CreateNewUI()
		{
			// Root for the UI
			var root = new GameObject("Canvas");
			root.layer = LayerMask.NameToLayer(kUILayerName);
			Canvas canvas = root.AddComponent<Canvas>();
			canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			root.AddComponent<CanvasScaler>();
			root.AddComponent<GraphicRaycaster>();
			Undo.RegisterCreatedObjectUndo(root, "Create " + root.name);

			// if there is no event system add one...
			CreateEventSystem(false);
			return root;
		}

		public static void CreateEventSystem(MenuCommand menuCommand)
		{
			GameObject parent = menuCommand.context as GameObject;
			CreateEventSystem(true, parent);
		}

		private static void CreateEventSystem(bool select)
		{
			CreateEventSystem(select, null);
		}

		private static void CreateEventSystem(bool select, GameObject parent)
		{
			var esys = Object.FindObjectOfType<EventSystem>();
			if (esys == null)
			{
				var eventSystem = new GameObject("EventSystem");
				GameObjectUtility.SetParentAndAlign(eventSystem, parent);
				esys = eventSystem.AddComponent<EventSystem>();
				eventSystem.AddComponent<StandaloneInputModule>();

				Undo.RegisterCreatedObjectUndo(eventSystem, "Create " + eventSystem.name);
			}

			if (select && esys != null)
			{
				Selection.activeGameObject = esys.gameObject;
			}
		}

		// Helper function that returns a Canvas GameObject; preferably a parent of the selection, or other existing Canvas.
		static public GameObject GetOrCreateCanvasGameObject()
		{
			GameObject selectedGo = Selection.activeGameObject;

			// Try to find a gameobject that is the selected GO or one if its parents.
			Canvas canvas = (selectedGo != null) ? selectedGo.GetComponentInParent<Canvas>() : null;
			if (canvas != null && canvas.gameObject.activeInHierarchy)
				return canvas.gameObject;

			// No canvas in selection or its parents? Then use just any canvas..
			canvas = Object.FindObjectOfType(typeof(Canvas)) as Canvas;
			if (canvas != null && canvas.gameObject.activeInHierarchy)
				return canvas.gameObject;

			// No canvas in the scene at all? Then create a new one.
			//return ExtensionMenuOptions.CreateNewUI();

			return null;
		}

		private static void SetDefaultColorTransitionValues(Selectable slider)
		{
			ColorBlock colors = slider.colors;
			colors.highlightedColor = new Color(0.882f, 0.882f, 0.882f);
			colors.pressedColor = new Color(0.698f, 0.698f, 0.698f);
			colors.disabledColor = new Color(0.521f, 0.521f, 0.521f);
		}

		private static void SetDefaultTextValues(Text lbl)
		{
			// Set text values we want across UI elements in default controls.
			// Don't set values which are the same as the default values for the Text component,
			// since there's no point in that, and it's good to keep them as consistent as possible.
			lbl.color = s_TextColor;
		}
		#endregion

		#region Scroll Snap controls
		[MenuItem("GameObject/UI/Extensions/Horizontal Scroll Snap", false)]
		static public void AddHorizontalScrollSnap(MenuCommand menuCommand)
		{
			GameObject horizontalScrollSnapRoot = CreateUIElementRoot("Horizontal Scroll Snap", menuCommand, s_ThickGUIElementSize);

			GameObject childContent = CreateUIObject("Content", horizontalScrollSnapRoot);

			GameObject childPage01 = CreateUIObject("Page_01", childContent);

			GameObject childText = CreateUIObject("Text", childPage01);

			// Set RectTransform to stretch
			RectTransform rectTransformScrollSnapRoot = horizontalScrollSnapRoot.GetComponent<RectTransform>();
			rectTransformScrollSnapRoot.anchorMin = new Vector2(0.5f, 0.5f);
			rectTransformScrollSnapRoot.anchorMax = new Vector2(0.5f, 0.5f);
			rectTransformScrollSnapRoot.anchoredPosition = Vector2.zero;
			rectTransformScrollSnapRoot.sizeDelta = new Vector2(300f, 150f);


			Image image = horizontalScrollSnapRoot.AddComponent<Image>();
			image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>(kBackgroundSpriteResourcePath);
			image.type = Image.Type.Sliced;
			image.color = new Color(1f, 1f, 1f, 0.392f);

			ScrollRect sr = horizontalScrollSnapRoot.AddComponent<ScrollRect>();
			sr.vertical = false;
			sr.horizontal = true;
			horizontalScrollSnapRoot.AddComponent<HorizontalScrollSnap>();

			//Setup Content container
			RectTransform rectTransformContent = childContent.GetComponent<RectTransform>();
			rectTransformContent.anchorMin = Vector2.zero;
			rectTransformContent.anchorMax = new Vector2(1f, 1f);
			rectTransformContent.sizeDelta = Vector2.zero;

			sr.content = rectTransformContent;

			//Setup 1st Child
			Image pageImage = childPage01.AddComponent<Image>();
			pageImage.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>(kStandardSpritePath);
			pageImage.type = Image.Type.Sliced;
			pageImage.color = s_DefaultSelectableColor;

			RectTransform rectTransformPage01 = childPage01.GetComponent<RectTransform>();
			rectTransformPage01.anchorMin = new Vector2(0f, 0.5f);
			rectTransformPage01.anchorMax = new Vector2(0f, 0.5f);
			rectTransformPage01.pivot = new Vector2(0f, 0.5f);

			//Setup Text on Page01
			Text text = childText.AddComponent<Text>();
			text.text = "Page_01";
			text.alignment = TextAnchor.MiddleCenter;
			text.color = new Color(0.196f, 0.196f, 0.196f);

			//Setup Text 1st Child
			RectTransform rectTransformPage01Text = childText.GetComponent<RectTransform>();
			rectTransformPage01Text.anchorMin = new Vector2(0.5f, 0.5f);
			rectTransformPage01Text.anchorMax = new Vector2(0.5f, 0.5f);
			rectTransformPage01Text.pivot = new Vector2(0.5f, 0.5f);


			//Need to add example child components like in the Asset (SJ)
			Selection.activeGameObject = horizontalScrollSnapRoot;
		}

        #endregion
    }
}
