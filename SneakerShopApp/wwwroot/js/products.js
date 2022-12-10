document.addEventListener("DOMContentLoaded", function (event) {
    const url = "https://" + window.location.host;


    $.ajax({
        url: url + "/Admin/GetAllProducts",
        type: 'GET',
        dataType: 'json', 
        success: function (res) {
            $.each(res, function (i, product) {
                var productCard =   `
                    <div class="card">
                      <div class="card-header">
                        `+ product.description +`
                      </div>
                      <div class="card-body">
                          <img class="slim-card" src=`+ product.imgUrl +` / style="width:50px">
                          <p>`+ product.longDescription +`</p>
                          <button class="btn btn-danger delete-product" data-guid=`+ product.guid + ` >Delete</button>
                          <input data-guid=`+ product.guid + ` value=`+ product.price +` class="card-price input-price" type=number />
                          <p class="card-price"> Ron </p> 
                      </div>
                    </div>
                `;
                $("#products-container").append(productCard)
            });

            // Delete the product
            $(".delete-product").click(function (event) {
                const guid = event.target.getAttribute('data-guid');
                var formData = new FormData();
                formData.append("guid", guid);
                $.ajax({
                    url: url + "/Admin/DeleteProduct",
                    type: 'POST',
                    data: formData,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        $(event.target).parent().parent().remove();

                    },
                    error: function () {

                    }
                })
            });
 
            // Update the price
            $(".input-price").focusout(function (event) {
                const guid = event.target.getAttribute('data-guid');
                const newValue = $(event.target).val();
                var formData = new FormData();
                formData.append("guid", guid);
                formData.append("price", newValue);
                $.ajax({
                    url: url + "/Admin/UpdateProductPrice",
                    type: 'POST',
                    data: formData,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        console.log("Price changed");

                    },
                    error: function () {

                    }
                })
            });
;
        }
    });


    $("#create-btn").click(function () {
        var product = {
            Brand: $("#brand").val(),
            Description: $("#description").val(),
            LongDescription: $("#long-description").val(),
            Price: $("#price").val()
        }

        var images = $("#images").prop("files");

        var formData = new FormData();
        $.each(images, function (i, file) {
            formData.append('files', file);
        });

        formData.append("Brand", product.Brand);
        formData.append("Description", product.Description);
        formData.append("LongDescription", product.LongDescription);
        formData.append("Price", product.Price);

        $.ajax({
            url: url + "/Admin/CreateProduct",
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function (data) {
                var productCard = `
                    <div class="card">
                      <div class="card-header">
                        `+ product.Description + `
                      </div>
                      <div class="card-body">
                          <img class="slim-card" src=`+ data[1] + ` / style="width:50px">
                          <p>`+ product.LongDescription + `</p>
                          <button class="btn btn-danger delete-product" data-guid=`+ data[0] + ` >Delete</button>
                          <input data-guid=`+ data[0] + ` value=` + product.Price +` class="card-price input-price" type=number />
                          <p class="card-price"> Ron </p> 
                      </div>
                    </div>
                `;
                $("#products-container").append(productCard)
                $('#exampleModal').modal('toggle');
                $("#brand").val("");
                $("#description").val("");
                $("#long-description").val("");
                $("#price").val("");
                $("#images").val("");
            },
            error: function () {

            }
        })
    });
});