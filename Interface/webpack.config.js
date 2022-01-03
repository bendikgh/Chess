const path = require("path");

module.exports = {
    entry: path.resolve(__dirname, "./src/script.js"),
    mode: "development",
    module: {
        rules: [{
            test: /\.(js)$/,
            exclude: /node_modules/,
            use: ["babel-loader"],
        }, ],
    },
    resolve: {
        extensions: ["*", ".js"],
    },
    output: {
        path: path.resolve(__dirname, "./dist"),
        filename: "main.js",
    },
};