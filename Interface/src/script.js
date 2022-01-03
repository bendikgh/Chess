// Imports
import { Tile } from "./tile.js";
import { moveBrick, getTest } from "./moves.js";
import { legalMovesStart, initialBoard } from "./data.js";

// Variables
let boardElement = document.getElementById("board");
export let board = initialBoard;
let legalMoves = legalMovesStart;

// Functions
export const drawBoard = () => {
    boardElement.innerHTML = "";
    for (let i = 0; i < 8; i++) {
        for (let j = 0; j < 8; j++) {
            let element = document.createElement("div");
            element.style.setProperty("border", "1px solid black");
            if (j % 2 == 0 && i % 2 == 0) {
                element.style.backgroundColor = "green";
            } else if (j % 2 != 0 && i % 2 != 0) {
                element.style.backgroundColor = "green";
            } else {
                element.style.backgroundColor = "white";
            }
            const tile = new Tile(element, i, j);
            boardElement.appendChild(tile.getElement());
        }
    }
};

export function setEvents(element, i, j) {
    element.ondragover = (ev) => ev.preventDefault();
    element.ondrop = (ev) => drop(ev, i, j);
}

const writeMove = (team, move) => {
    const str = `${team} moved from ${JSON.stringify(
    move.moveFromTile
  )} to ${JSON.stringify(move.moveToTile)}`;

    write(str);
};

const writeWin = (team) => {
    const str = `Chessmate! ${team} is the winner!`;
    write(str);
};

const write = (str) => {
    const div = document.createElement("div");
    const text = document.getElementsByClassName("text-box")[0];
    div.textContent = str;
    const lst = [div, ...text.children[0].children];
    text.children[0].innerHTML = "";
    for (let e of lst) {
        e.style.marginBottom = "20px";
        text.children[0].appendChild(e);
    }
};

const icludesTile = (lst, tile) => {
    for (let t of lst) {
        if (t.i === tile.i && t.j === tile.j) {
            return true;
        }
    }
    return false;
};

// Draggable
const drop = async(ev, i, j) => {
    ev.preventDefault();
    let data = JSON.parse(ev.dataTransfer.getData("text"));

    const oldBoard = JSON.parse(JSON.stringify(board));

    const pos = "{" + data.i + ", " + data.j + "}";

    if (!legalMoves[pos]) return;

    if (icludesTile(legalMoves[pos], { i, j })) {
        changeBoard(data, { i, j });
    } else return;

    const res = await moveBrick(data, { i, j }, oldBoard);

    if (res["winner"]) {
        const element = document.getElementById("bricks-text");
        const body = document.getElementById("board");
        element.style.setProperty("--color", "red");
        body.style.setProperty("--borderSize", "4px");
        body.style.setProperty("--color", "red");
        board = res["board"];
        legalMoves = res["moves"];
        return writeWin(res.winner);
    }

    board = res["board"];
    legalMoves = res["moves"];

    drawBoard();
    updateBricks();

    writeMove("W", { moveFromTile: data, moveToTile: { i, j } });
};

export const drag = (ev, i, j) => {
    ev.dataTransfer.setData("text", JSON.stringify({ i, j }));
    ev.target.parentElement.style.backgroundColor = "orange";
};

// Bruk denne for å endre brettet
const changeBoard = (prePos, newPos) => {
    const brick = board[prePos.i][prePos.j];
    board[prePos.i][prePos.j] = "";
    board[newPos.i][newPos.j] = brick;
    drawBoard();
};

document.addEventListener("DOMContentLoaded", function() {
    boardElement = document.getElementById("board");
    drawBoard();
});

const updateBricks = () => {
    let countWhite = 0;
    let countBlack = 0;
    for (let lst of board) {
        for (let tile of lst) {
            if (tile.charAt(0) === "W") countWhite++;
            else if (tile.charAt(0) === "B") countBlack++;
        }
    }
    const element = document.getElementById("bricks-text");
    element.textContent = `White: ${16 - countBlack} | Black: ${16 - countWhite}`;
};