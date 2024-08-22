import path from 'path';
import ESLintPlugin from 'eslint-webpack-plugin';

const config = {
  plugins: [
    new ESLintPlugin({
      extensions: ['ts', 'tsx'],
      files: ['src/**/*.ts', 'src/**/*.tsx'],
    }),
  ],
  entry: './src/index.tsx',
  resolve: {
    extensions: ['.tsx', '.ts', '.js', '.jsx'],
  },
  output: {
    path: path.resolve(__dirname, 'dist'),
    filename: 'bundle.js',
  },
  module: {
    rules: [
      {
        test: /\.(ts|tsx)$/,
        exclude: /node_modules/,
        use: 'ts-loader',
      },
    ],
  },
  devServer: {
    headers: {
      'Access-Control-Allow-Origin': '*',
      'Access-Control-Allow-Headers':
        'Origin, Authorization, X-Requested-With, Content-Type, Accept',
    },
    static: {
      directory: path.join(__dirname, 'dist'),
    },
    compress: true,
    port: 8081, // You can change the port number if needed
    hot: true,
    proxy: {
      '/Login': {
        target: 'https://localhost:7152',
        secure: false,
      }, // Your API server address
      '/WeatherForecast': 'https://localhost:7152',
    },
  },
};

export default config;
