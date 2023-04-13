namespace YPPGUtilities
{
    namespace InputManagement
    {
        using System.Collections.Generic;
        using UnityEngine;
        using Service;
        using static UnityEngine.InputSystem.InputAction;

        /**
         *  <summary>
         *  Used to offer a gamepad alternative to dragging and dropping with a mouse by highlighting and unhighlighting certain GameObjects
         *  </summary>
         */
        public class GamepadDragAndDrop : MonoBehaviour
        {
            // The system can be toggled on or off statically
            public static bool IsUsable;

                        /**
             *  <summary>
             *  GameObjects (usually UI elements) that would be draggable with a mouse must inherit from this abstract class for a gamepad implementation
             *  </summary>            
             *  
             *  <example>
             *  An example behaviour might be activating/deactivating a gameobject with a "glow" animation that clearly illustrates the highlight
             *  </example>
             *  
             *  <see cref="DroppableGamepadGameObject"/>
             */
            public abstract class DraggableGamepadGameObject : MonoBehaviour
            {
                // Implements the desired behaviour when the draggable object was selected
                public abstract void OnGamepadDragSelection();

                // Implements the desired behaviour when the object was highlighted / is being "hovered" over
                public abstract void OnHighlight();
                
                // Implements the desired behaviour when the object was unhighlighted / stopped being "hovered" over
                public abstract void OnUnhighlight();
            }

            /**
             *  <summary>
             *  GameObjects (usually UI elements) that would accept items to be dropped on with a mouse must inherit from this abstract class for a gamepad implementation
             *  </summary>
             * 
             *  <example>
             *  An example behaviour might be activating/deactivating a gameobject with a "glow" animation that clearly illustrates the highlight
             *  </example>
             *  
             *  <see cref="DraggableGamepadGameObject"/>
             */
            public abstract class DroppableGamepadGameObject : MonoBehaviour
            {
                // Implements the desired behaviour when an item has been dragged to a droppable location (drag & drop)
                // Also provides access to the DraggableGamepadGameObject that was dragged onto the DroppableGamepadGameObject
                public abstract void OnGamepadDropSelection(DraggableGamepadGameObject draggableGameObject);

                // Implements the desired behaviour when the object was highlighted / is being "hovered" over
                public abstract void OnHighlight();

                // Implements the desired behaviour when the object was unhighlighted / stopped being "hovered" over
                public abstract void OnUnhighlight();
            }

            // Both, the draggable and droppable objects can be added and removed dynamically using these lists 
            public List<DraggableGamepadGameObject> draggableGameObjects;
            public List<DroppableGamepadGameObject> droppableGameObjects;

            // Unity's New (!) Input Manager needs the three Actions "DragAndDropNavigation", "DragAndDropSelection", and "DragAndDropAbortion" in the "UI" section
            private System.Action<CallbackContext> dragAndDropNavigation; 
            private System.Action<CallbackContext> dragAndDropSelection;
            private System.Action<CallbackContext> dragAndDropAbortion;

            // The draggable object is saved when it has been selected 
            private DraggableGamepadGameObject selectedDraggable;

            // Determines the state of currently either waiting for a draggable or droppable selection
            private bool inDroppableSelection;

            // Manages the current index of the item that is being highlighted
            private int selectionIndex;

            private void Awake()
            {
                // The binding needs to be a Vector2
                dragAndDropNavigation = ctx => ChangeNavigation(ctx.ReadValue<Vector2>());

                dragAndDropSelection = _ => SelectCurrent();
                dragAndDropAbortion = _ => Abort();

                // Adds all actions to the inputMaster (make sure to rename the inputMaster here as well if you renamed it in the InputManagement class!)
                InputService.Instance.inputMaster.UI.DragAndDropNavigation.performed += dragAndDropNavigation;
                InputService.Instance.inputMaster.UI.Selection.performed += dragAndDropSelection;
                InputService.Instance.inputMaster.UI.Abortion.performed += dragAndDropAbortion;

                // Creates the lists at Awake(). Do NOT try to manipulate them during Awake() or even earlier!
                draggableGameObjects = new List<DraggableGamepadGameObject>();
                droppableGameObjects = new List<DroppableGamepadGameObject>();
            }

            // Disposes of the input actions
            private void OnDisable()
            {
                InputService.Instance.inputMaster.UI.DragAndDropNavigation.performed -= dragAndDropNavigation;
                InputService.Instance.inputMaster.UI.Selection.performed -= dragAndDropSelection;
                InputService.Instance.inputMaster.UI.Abortion.performed -= dragAndDropAbortion;
            }

            /**
             *  <summary>
             *  Unhighlights all draggable and droppable objects and then highlights the first draggable object if it exists
             *  </summary>
             *  
             *  <see cref="ResetAll"/>
             *  <see cref="HighlightFirstDraggable"/>
             */
            public void UpdateDragAndDrop()
            {
                ResetAll();
                HighlightFirstDraggable();
            }

            /**
             *  <summary>
             *  Unhighlights all draggable and dropble objects and resets the management variables
             *  </summary>
             */
            public void ResetAll()
            {
                inDroppableSelection = false;
                selectionIndex = 0;
                draggableGameObjects.ForEach(draggableGameObject => draggableGameObject.OnUnhighlight());
                droppableGameObjects.ForEach(droppableGameObjects => droppableGameObjects.OnUnhighlight());
            }

            /**
             *  <summary>
             *  Highlights the first draggable object if it exists and resets the management variables
             *  </summary>
             */  
            public void HighlightFirstDraggable()
            {
                if (draggableGameObjects.Count != 0)
                {
                    inDroppableSelection = false;
                    selectionIndex = 0;
                    draggableGameObjects[0].OnHighlight();
                }
            }

            // Stops the droppable object selection and returns to the dragable object selection
            private void Abort()
            {
                // The system will abort the selection if the entire system isn't usable!
                if (IsUsable && inDroppableSelection)
                {
                    // Unhighlights the current droppable object
                    if (droppableGameObjects.Count > 0)
                        droppableGameObjects[selectionIndex].OnUnhighlight();

                    // Resets the management variables
                    inDroppableSelection = false;
                    selectionIndex = 0;

                    // Highlights the first draggable object if it exists
                    if (draggableGameObjects.Count > 0)
                        draggableGameObjects[0].OnHighlight();
                }
            }

            // Changes the currently selected draggable or droppable object depending on the direction
            private void ChangeNavigation(Vector2 direction)
            {
                // The system will not change the navigation if it isn't usable!
                if (IsUsable)
                {
                    if (inDroppableSelection)
                    {
                        // Unhighlights the current droppable object
                        droppableGameObjects[selectionIndex].OnUnhighlight();

                        // Increments the index if should be moved to the right
                        if (direction.x > 0f)
                        {
                            if (selectionIndex == droppableGameObjects.Count - 1)
                            {
                                selectionIndex = 0;
                            }
                            else
                            {
                                selectionIndex++;
                            }
                        }
                        // Decrements the index if should be moved to the left
                        else if (direction.x <= 0f)
                        {
                            if (selectionIndex == 0)
                            {
                                selectionIndex = droppableGameObjects.Count - 1;
                            }
                            else
                            {
                                selectionIndex--;
                            }
                        }

                        // Highlights the current droppable object
                        droppableGameObjects[selectionIndex].OnHighlight();
                    }
                    else
                    {
                        // Unhighlights the current draggable object
                        draggableGameObjects[selectionIndex].OnUnhighlight();

                        // Increments the index if should be moved to the right
                        if (direction.x > 0f)
                        {
                            if (selectionIndex == draggableGameObjects.Count - 1)
                            {
                                selectionIndex = 0;
                            }
                            else
                            {
                                selectionIndex++;
                            }
                        }
                        // Decrements the index if should be moved to the left
                        else if (direction.x <= 0f)
                        {
                            if (selectionIndex == 0)
                            {
                                selectionIndex = draggableGameObjects.Count - 1;
                            }
                            else
                            {
                                selectionIndex--;
                            }
                        }

                        // Highlights the current draggable object
                        draggableGameObjects[selectionIndex].OnHighlight();
                    }
                }
            }

            // Triggers the selection behaviour, resets the management variables and then highlights the first draggable/droppable
            private void SelectCurrent()
            {
                // Will not select the current object if the system isn't usable!
                if (IsUsable)
                {
                    if (inDroppableSelection)
                    {
                        droppableGameObjects[selectionIndex].OnGamepadDropSelection(selectedDraggable);
                        inDroppableSelection = false;
                        selectionIndex = 0;
                        draggableGameObjects[0].OnHighlight();
                    }
                    else
                    {
                        selectedDraggable = draggableGameObjects[selectionIndex];
                        draggableGameObjects[selectionIndex].OnGamepadDragSelection();
                        inDroppableSelection = true;
                        selectionIndex = 0;
                        droppableGameObjects[0].OnHighlight();
                    }
                }
            }
        }
    }
}
