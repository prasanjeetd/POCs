var GridviewViewModel =  {

    viewModel: kendo.observable({
        
        onSave: function (e) {
            console.log("save",e);
        },
        onEdit: function (e) {
            console.log("Edit", e);
            //var button = $(e.currentTarget).closest('tr').next().find(".k-grid-edit");
            //$(button).click();
        },
        onCancel: function (e) {
            console.log("cancel ", e);
            //e.preventDefault()
        },
        products: dataSource.productDataSource(),
    })

};