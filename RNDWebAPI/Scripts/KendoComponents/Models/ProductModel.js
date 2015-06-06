var model = model || {};

model.productModel = function () {

    var productModel = kendo.data.Model.define({
        id: "ProductID", // the identifier of the model
        fields: {
            "ProductName": {
                type: "string"
            },
            "UnitPrice": {
                type: "number"
            }
        }
    });

    return productModel;
}