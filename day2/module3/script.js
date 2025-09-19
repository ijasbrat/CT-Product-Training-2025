function makeDraggable(el, container = document.body) {
  let isDragging = false, offsetX, offsetY;

  el.addEventListener("mousedown", e => {
    isDragging = true;
    offsetX = e.offsetX;
    offsetY = e.offsetY;
    el.style.cursor = "grabbing";
  });

  document.addEventListener("mousemove", e => {
    if (isDragging) {
      const rect = container.getBoundingClientRect();

      let x = e.clientX - rect.left - offsetX;
      let y = e.clientY - rect.top - offsetY;

      x = Math.max(0, Math.min(x, rect.width - el.offsetWidth));
      y = Math.max(0, Math.min(y, rect.height - el.offsetHeight));

      el.style.left = x + "px";
      el.style.top = y + "px";
    }
  });

  document.addEventListener("mouseup", () => {
    isDragging = false;
    el.style.cursor = "grab";
  });
}

makeDraggable(document.getElementById("drag1"), document.body);  
makeDraggable(document.getElementById("drag2"), document.getElementById("dropzone"));
