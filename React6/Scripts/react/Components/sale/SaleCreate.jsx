import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import { Modal, Button, Form } from 'semantic-ui-react';

export default class SaleCreate extends Component {
    constructor(props) {
        super(props);
        this.state = {
            Success: { Data: '' },

            productId: '',
            storeId: '',
            customerId: '',
            dateSold: '',

            CustomerDropdownList: [],
            ProductDropdownList: [],
            StoresDropdownList: [],

            Sucess: [],
            errors: {}
        };

        this.onCreateSubmit = this.onCreateSubmit.bind(this);
        this.onClose = this.onClose.bind(this);
        this.onChange = this.onChange.bind(this);

        this.CustomersDropdown = this.CustomersDropdown.bind(this);
        this.ProductsDropdown = this.ProductsDropdown.bind(this);
        this.StoresDropdown = this.StoresDropdown.bind(this);
    }

    componentDidMount() {
        this.CustomersDropdown();
        this.ProductsDropdown();
        this.StoresDropdown();
    }

    validateForm() {

        let errors = {}

        let formIsValid = true
        if (!this.state.customerId) {
            formIsValid = false;
            errors['CustomerId'] = '*Please select the Customer.';
        }

        if (!this.state.productId) {
            formIsValid = false;
            errors['ProductId'] = '*Please select the Product.'
        }

        if (!this.state.storeId) {
            formIsValid = false;
            errors['StoreId'] = '*Please select the Store.'
        }

        if (!this.state.dateSold) {
            formIsValid = false;
            errors['DateSold'] = '*Please provide the sale date.'
        }

        this.setState({
            errors: errors
        });
        return formIsValid
    }

    onCreateSubmit(e) {
        e.preventDefault();
        if (this.validateForm()) {
            let data = {
                'customerId': this.state.customerId,
                'productId': this.state.ProductId,
                'storeId': this.state.StoreId,
                'dateSold': this.state.dateSold
            };

            $.ajax({
                url: "/Sales/CreateSale",
                type: "POST",
                data: data,
                success: function (data) {
                    this.setState({ Success: data })

                    window.location.reload()
                }.bind(this)
            });
        }
    }

    onClose() {
        this.setState({ showDeleteModal: false });
        window.location.reload()
    }

    onChange(e) {
        console.log(e.target.value)
        this.setState({ [e.target.name]: e.target.value });
    }

    CustomersDropdown() {
        $.ajax({
            url: "/Sales/GetCustomers",
            type: "GET",
            success: function (data) {
                this.setState({ CustomerDropdownList: data })
            }.bind(this)
        });
    }

    ProductsDropdown() {
        $.ajax({
            url: "/Sales/GetProducts",
            type: "GET",
            success: function (data) {
                this.setState({ ProductDropdownList: data })
            }.bind(this)
        });
    }

    StoresDropdown() {
        $.ajax({
            url: "/Sales/GetStores",
            type: "GET",
            success: function (data) {
                this.setState({ StoresDropdownList: data })
            }.bind(this)
        });
    }

    render() {
        let CustomerDataList = [{ id: '', customerName: 'Select Customer' }].concat(this.state.CustomerDropdownList)
        let ProductDataList = [{ id: '', productName: 'Select Product' }].concat(this.state.ProductDropdownList)
        let StoreDataList = [{ id: '', storeName: 'Select Store' }].concat(this.state.StoresDropdownList)



        return (
            <React.Fragment>
                <Modal open={this.props.showCreateModel} onClose={this.props.onClose} size='small'>
                    <Modal.Header> Create Sales </Modal.Header>
                    <Modal.Content>
                        <Form>
                            <Form.Field>
                                <label>Customer Name</label>
                                <select name="CustomerId" onChange={this.onChange} value={this.state.customerId}>
                                    {CustomerDataList.map((Cust) => <option key={Cust.id} value={Cust.id}>{Cust.customerName}</option>)}
                                </select>
                                <div style={{ color: 'red' }}>
                                    {this.state.errors.customerId}
                                </div>
                            </Form.Field>
                            <Form.Field>
                                <label>Product Name</label>
                                <select name="ProductId" onChange={this.onChange} value={this.state.productId}>
                                    {ProductDataList.map((Prod) => <option key={Prod.id} value={Prod.id}>{Prod.productName}</option>)}
                                </select>
                                <div style={{ color: 'red' }}>
                                    {this.state.errors.productId}
                                </div>
                            </Form.Field>
                            <Form.Field>
                                <label>Store Name</label>
                                <select name="StoreId" onChange={this.onChange} value={this.state.storeId}>
                                    {StoreDataList.map((Str) => <option key={Str.id} value={Str.id}>{Str.storeName}</option>)}
                                </select>
                                <div style={{ color: 'red' }}>
                                    {this.state.errors.storeId}
                                </div>
                            </Form.Field>
                            <Form.Field>
                                <label>Date Sold</label>
                                <input type="text" name="DateSold" placeholder='YYYY/MM/DD' onChange={this.onChange} />
                                <div style={{ color: 'red' }}>
                                    {this.state.errors.dateSold}
                                </div>
                            </Form.Field>
                        </Form>
                    </Modal.Content>
                    <Modal.Actions>
                        <Button onClick={this.props.onClose} secondary >Cancel
                        </Button>
                        <Button onClick={this.onCreateSubmit} className="ui green button">Create
                        <i className="check icon"></i>
                        </Button>
                    </Modal.Actions>
                </Modal>
            </React.Fragment>
        )
    }
}