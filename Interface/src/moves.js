// Queries that makes moves for the two players
import axios from "axios";

export const moveBrick = async(t1, t2, board) => {
    const dto = {
        moveFromTile: t1,
        moveToTile: t2,
        board,
    };

    console.log(board);
    console.log("----------");

    try {
        var res = await axios.post("https://localhost:5001/api/Chess", dto);
        console.log(res.data);
    } catch (error) {
        console.log("error..");
        return null;
    }
    return res.data;
};

export const getTest = async() => {
    const data = await axios({
        method: "get",
        url: "https://localhost:5001/api/Chess",
    });
};