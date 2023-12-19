const path = require("path");
const ESLintPlugin = require("eslint-webpack-plugin");

module.exports = {
  plugins: [
    new ESLintPlugin({
      extensions: ["ts", "tsx"],
      files: ["src/**/*.ts", "src/**/*.tsx"],
    }),
  ],
  entry: "./src/index.tsx",
  resolve: {
    extensions: [".tsx", ".ts", ".js", ".jsx"],
  },
  output: {
    path: path.resolve(__dirname, "dist"),
    filename: "bundle.js",
  },
  module: {
    rules: [
      {
        test: /\.(ts|tsx)$/,
        exclude: /node_modules/,
        use: "ts-loader",
      },
    ],
  },
  devServer: {
    static: {
      directory: path.join(__dirname, "dist"),
    },
    compress: true,
    port: 3000, // You can change the port number if needed
    hot: true,
    proxy: {
      "/Login": "http://localhost:5262",
      "/WeatherForecast": "http://localhost:5262",
    },
  },
};
