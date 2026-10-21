import { Ref, unref, watchEffect } from 'vue';
import { useTimeoutFn } from '/@/hooks/core/useTimeout';

export interface UseModalDragMoveContext {
  draggable: Ref<boolean>;
  destroyOnClose: Ref<boolean | undefined> | undefined;
  visible: Ref<boolean>;
}

export function useModalDragMove(context: UseModalDragMoveContext) {
  const getStyle = (dom: any, attr: any) => {
    return getComputedStyle(dom)[attr];
  };
  const drag = (wrap: any) => {
    if (!wrap) return;
    wrap.setAttribute('data-drag', unref(context.draggable));
    const dialogHeaderEl = wrap.querySelector('.ant-modal-header');
    const dragDom = wrap.querySelector('.ant-modal');

    if (!dialogHeaderEl || !dragDom || !unref(context.draggable)) return;

    dialogHeaderEl.style.cursor = 'move';

    dialogHeaderEl.onmousedown = (e: any) => {
      if (!e) return;
      // When the mouse is pressed, calculate the distance between the current element and the visible area
      const disX = e.clientX;
      const disY = e.clientY;
      const screenWidth = document.body.clientWidth; // current width of body
      const screenHeight = document.documentElement.clientHeight; // Visible area height (should be the body height, but it cannot be obtained in some environments)

      const dragDomWidth = dragDom.offsetWidth; // Dialog width
      const dragDomheight = dragDom.offsetHeight; // Dialog height

      const minDragDomLeft = dragDom.offsetLeft;

      const maxDragDomLeft = screenWidth - dragDom.offsetLeft - dragDomWidth;
      const minDragDomTop = dragDom.offsetTop;
      const maxDragDomTop = screenHeight - dragDom.offsetTop - dragDomheight;
      // The obtained value has px regular matching and replacement
      const domLeft = getStyle(dragDom, 'left');
      const domTop = getStyle(dragDom, 'top');
      let styL = +domLeft;
      let styT = +domTop;

      // Note that in IE, the value obtained for the first time is 50% of the component. After moving, the value is assigned to px.
      if (domLeft.includes('%')) {
        styL = +document.body.clientWidth * (+domLeft.replace(/%/g, '') / 100);
        styT = +document.body.clientHeight * (+domTop.replace(/%/g, '') / 100);
      } else {
        styL = +domLeft.replace(/px/g, '');
        styT = +domTop.replace(/px/g, '');
      }

      document.onmousemove = function (e) {
        // Calculate the distance moved through event delegation
        let left = e.clientX - disX;
        let top = e.clientY - disY;

        // Boundary processing
        if (-left > minDragDomLeft) {
          left = -minDragDomLeft;
        } else if (left > maxDragDomLeft) {
          left = maxDragDomLeft;
        }

        if (-top > minDragDomTop) {
          top = -minDragDomTop;
        } else if (top > maxDragDomTop) {
          top = maxDragDomTop;
        }

        // Move the current element
        dragDom.style.cssText += `;left:${left + styL}px;top:${top + styT}px;`;
      };

      document.onmouseup = () => {
        document.onmousemove = null;
        document.onmouseup = null;
      };
    };
  };

  const handleDrag = () => {
    const dragWraps = document.querySelectorAll('.ant-modal-wrap');
    for (const wrap of Array.from(dragWraps)) {
      if (!wrap) continue;
      const display = getStyle(wrap, 'display');
      const draggable = wrap.getAttribute('data-drag');
      if (display !== 'none') {
        // Drag position
        if (draggable === null || unref(context.destroyOnClose)) {
          drag(wrap);
        }
      }
    }
  };

  watchEffect(() => {
    if (!unref(context.visible) || !unref(context.draggable)) {
      return;
    }
    useTimeoutFn(() => {
      handleDrag();
    }, 30);
  });
}
