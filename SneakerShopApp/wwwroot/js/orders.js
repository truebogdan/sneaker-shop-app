document.addEventListener("DOMContentLoaded", function (event) {
    const url = "https://" + window.location.host;
    const completedString = "completed";
    const inProgressString = " in progress";
    const openProductsModal = (products, orderId) => {
        $("#order-products-container").html('');
        $("#order-products-modal").modal('toggle');
        $("#modal-order-id").html(orderId);
        $.each(products, function (i, product) {
            $("#order-products-container").append("<hr><p>" + product.Description + " size " + product.Size +"</p>");
            $("#order-products-container").append("<p> Price:" + product.Price + " Ron </p>");
            $("#order-products-container").append(" <img  src="+ product.ImgUrl +"/>");
        });
    };

    $.ajax({
        url: url + "/Admin/GetAllOrders",
        type: 'GET',
        dataType: 'json',
        success: function (res) {
            $.each(res, function (i, order) {
                console.log(order);
                const orderStatus = order.isCompleted ? "completed" : "in progress";
                var orderCard = `
                    <div class="card">
                      <div class="card-header">
                        Order#`+ order.orderId + ` <span class="order-status order` + order.orderId + `"> Status: ` + orderStatus + `</span>
                      </div>
                      <div class="card-body">
                          <p> Total: `+ order.total + ` Ron</p>
                          <p class="order-customer"> Customer: `+ order.customer + ` </p>
                          <button data-order-id=` + order.orderId + ` class='btn btn-success  complete-button order-status'> Complete </button>
                          <p class="order-products-link" data-order=`+ i + ` data-order-id=` + order.orderId + ` > View ` + order.products.length + `  products </p>
                          <p>`+order.fullName+`</p> 
                          <p>`+order.address+`</p> 
                          <p>`+order.phone+`</p> 

                      </div>
                    </div>
                `
                if (order.isCompleted) {
                    orderCardWithoutButton = `
                    <div class="card completed-card">
                      <div class="card-header">
                        Order#`+ order.orderId + ` <span class="order-status order` + order.orderId + `"> Status: ` + orderStatus + `</span>
                      </div>
                      <div class="card-body">
                          <p> Total: `+ order.total + ` Ron</p>
                          <p class="order-customer"> Customer: `+ order.customer + ` </p>
                          <p class="order-products-link" data-order=`+ i + ` data-order-id=` + order.orderId + ` > View ` + order.products.length + `  products </p>
                          <p>`+ order.fullName + `</p> 
                          <p>`+ order.address + `</p> 
                          <p>`+ order.phone +`</p> 
                      </div>
                    </div>
                `
                    $("#orders-container").append(orderCardWithoutButton);
                }
                else {
                    $("#orders-container").append(orderCard);
                }
               
            });
            $(".order-products-link").click(function (event) {
                const orderIndex = event.target.getAttribute('data-order');
                const orderId = event.target.getAttribute('data-order-id');
                openProductsModal(res[orderIndex].products, orderId);


            });

            // Toggle completed cards

            $("#completedSwitch").on('change', function () {
                console.log("test");
                $(".completed-card").toggle();
            });


            $(".complete-button").click(function (event) {
                const orderId = event.target.getAttribute('data-order-id');

                //$(event.target).remove();
                //$(".order" + orderId).html("Status: completed");
                var formData = new FormData();
                formData.append("orderId", orderId);
                $.ajax({
                    url: url + "/Admin/CompleteOrder",
                    type: 'POST',
                    data: formData,
                    contentType: false,
                    processData: false,
                    success: function (res) {
                         $(event.target).remove();
                         $(".order" + orderId).html("Status: completed");
                    }
                });
            });
        }
    });
});