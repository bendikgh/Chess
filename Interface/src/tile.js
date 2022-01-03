import { board, setEvents, drag } from "./script.js";
// Parent class for all bricks
export class Tile {
    constructor(element, i, j) {
        this.element = element;
        element.style.display = "flex";
        element.style.justifyContent = "center";
        element.style.alignItems = "center";
        setEvents.bind(this)(element, i, j);
        if (!board[i][j]) {
            return;
        }
        const image = document.createElement("img");
        image.src = `./images/${board[i][j]}.svg`;
        image.alt = `${board[i][j]}`;
        if (board[i][j].charAt(0) === "W") {
            image.draggable = true;
            image.ondragstart = (ev) => drag(ev, i, j);
        } else {
            image.draggable = false;
        }
        this.element.appendChild(image);
    }

    getElement() {
        return this.element;
    }
}