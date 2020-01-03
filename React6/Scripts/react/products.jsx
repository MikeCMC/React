  
import React, { Component } from 'react';
import ReactDOM from 'react-dom';

import ProductTable from './Components/product/ProductTable';


const app = document.getElementById('products');
ReactDOM.render(<ProductTable />, app);