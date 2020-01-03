module.exports = {
    context: __dirname,
    mode: 'development',
    entry: {
        app: "./index.jsx",
        Products: "./products.jsx",
        Stores: "./store.jsx",
        Sales: "./sales.jsx"

    }
    ,
    output: {
        path: __dirname + "/dist",
        filename: "[name].bundle.js"
    }
    ,
    watch: true,
    resolve: {
        extensions: [".jsx", ".js"]
    },
    module: {
        rules: [
            {
                test: /\.(js|jsx)$/,
                exclude: /(node_models)/,
                use: {
                    loader: 'babel-loader',
                    options: {
                        presets: ["@babel/preset-env", "@babel/preset-react"]
                    }
                }
            }

        ]
    }
};