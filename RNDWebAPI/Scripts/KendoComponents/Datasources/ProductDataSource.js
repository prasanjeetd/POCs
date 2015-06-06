var dataSource = dataSource || {};

dataSource.productDataSource = function () {

    var productsDS = new kendo.data.DataSource({
        schema: {
            model: model.productModel()
        },
        batch: true,
        transport: {
            read: {
                url: "http://demos.telerik.com/kendo-ui/service/products",
                dataType: "jsonp"
            },
            update: {
                url: "http://demos.telerik.com/kendo-ui/service/products/update",
                dataType: "jsonp"
            },
            create: {
                url: "http://demos.telerik.com/kendo-ui/service/products/create",
                dataType: "jsonp"
            },
            destroy: {
                url: "http://demos.telerik.com/kendo-ui/service/products/Destroy",
                dataType: "jsonp"
            },
            parameterMap: function (options, operation) {
                if (operation !== "read" && options.models) {
                    return { models: kendo.stringify(options.models) };
                }
            }
        }
    });

    return productsDS;
}